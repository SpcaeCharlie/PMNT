using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{

    private int lastInput;
    public Tweener tweener;
    private int currentInput;
    private List<Vector3> directions = new List<Vector3>(); // Use List instead of array
    private int collisionchecker = 0;
    public Animator animator;
    public AudioSource audio;
    public AudioClip walk;
    public AudioClip pelletwalk;
    public AudioClip death;
    public AudioClip Collision;
    public Sprite pellet;
    public Sprite blank;
    public Sprite powerpellet;

   

    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        directions.Add(new Vector3(0f, 0.5f, 0));
        directions.Add(new Vector3(0f, -0.5f, 0));
        directions.Add(new Vector3(0.5f, 0f, 0));
        directions.Add(new Vector3(-0.5f, 0f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = 0;
        }
        if (Input.GetKey(KeyCode.S))
        {
            lastInput = 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = 3;
        }

        if (!tweener.TweenExists(transform))
        {
            
           
        
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + directions[lastInput], 0.2f);
            if (hitColliders[0].gameObject.tag == "Walkable")
            {
                currentInput = lastInput;
                Vector3 endpos = new Vector3(hitColliders[0].gameObject.transform.position.x, hitColliders[0].gameObject.transform.position.y, -1f);
                tweener.AddTween(transform, transform.position, endpos, 0.5f);
                animator.SetInteger("Direction", lastInput);
                animator.speed = 1;
                if (hitColliders[0].gameObject.GetComponent<SpriteRenderer>().sprite == blank )
                {
                    audio.clip = walk;
                    Debug.Log("problem1");
                    audio.loop = true;
                    audio.Play();
                    collisionchecker = 0;
                    audio.Play();
                }
                if (hitColliders[0].gameObject.GetComponent<SpriteRenderer>().sprite == pellet || hitColliders[0].gameObject.GetComponent<SpriteRenderer>().sprite == powerpellet)
                {
                    audio.clip = pelletwalk;
                    audio.loop = true;
                    audio.Play();
                    collisionchecker = 0;
                    audio.Play();
                }
                //particle.Play();
                particle.Emit(20);

                
            }
            if (hitColliders[0].gameObject.tag != "Walkable")
            {
                Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll(transform.position + directions[currentInput], 0.2f);
                if (hitColliders2[0].gameObject.tag == "Walkable")
                {
                    collisionchecker = 0;

                    Vector3 endpos2 = new Vector3(hitColliders2[0].gameObject.transform.position.x, hitColliders2[0].gameObject.transform.position.y, -1f);

                    tweener.AddTween(transform, transform.position, endpos2, 0.5f);
                    animator.SetInteger("Direction", currentInput);
                    if (hitColliders2[0].gameObject.GetComponent<SpriteRenderer>().sprite == blank)
                    {
                        audio.clip = walk;
                       
                        audio.Play();
                        collisionchecker = 0;
                        Debug.Log("problem2");
                        audio.Play();
                    }
                    if (hitColliders2[0].gameObject.GetComponent<SpriteRenderer>().sprite == pellet || hitColliders2[0].gameObject.GetComponent<SpriteRenderer>().sprite == powerpellet)
                    {
                        audio.clip = pelletwalk;
                        audio.Play();
                        collisionchecker = 0;
                    }
                    //particle.Play();
                    particle.Emit(20);
                }
                if (hitColliders2[0].gameObject.tag !="Walkable" && collisionchecker ==0)
                {
                    animator.speed = 0;
                    audio.loop = false;
                    audio.clip = Collision;
                    audio.Play();
                    collisionchecker += 1;
                if (hitColliders2[0].gameObject.tag !="Walkable")
                {
                    animator.speed = 0;
                    audio.Stop();
                }
            }
        }
        if (tweener.TweenExists(transform))
        {

        }

       
    }
}
