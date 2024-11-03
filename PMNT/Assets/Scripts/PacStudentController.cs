using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PacStudentController : MonoBehaviour
{
    private int playerscore=0;
    private GameObject currentpos;
    private int lastInput;
    public Tweener tweener;
    private int currentInput;
    private List<Vector3> directions = new List<Vector3>(); // Use List instead of array
    private int collisionchecker = 0;
    public Animator animator;
    public AudioSource audio;
    public AudioSource mainAudio;
    public AudioClip scared;
    public AudioClip walk;
    public AudioClip pelletwalk;
    public AudioClip death;
    public AudioClip Collision;
    public Sprite pellet;
    public Sprite blank;
    public Sprite powerpellet;
    public ParticleSystem collisionparticle;
    public ParticleSystem particle;

    public TextMeshProUGUI score;

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
        score.text = playerscore.ToString();
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
            if (currentpos != null)
            {
                if (currentpos.GetComponent<SpriteRenderer>().sprite == pellet)
                {
                    currentpos.GetComponent<SpriteRenderer>().sprite = blank;
                    playerscore += 10;
                    currentpos = null;
                }
                else if (currentpos.tag == "powerpellet")
                {
                    StartCoroutine(scaredstate());
                    //mainAudio.clip = normalstate;
                    currentpos.GetComponent<Animator>().enabled = false;
                    currentpos.GetComponent<SpriteRenderer>().sprite = blank;
                    currentpos = null;
                }
            }


            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + directions[lastInput], 0.2f);
            if (hitColliders.Length <=0)
            {
                
                transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            }
            else if (hitColliders[0].gameObject.tag == "Walkable" || hitColliders[0].gameObject.tag == "powerpellet")
            {
                currentInput = lastInput;
                Vector3 endpos = new Vector3(hitColliders[0].gameObject.transform.position.x, hitColliders[0].gameObject.transform.position.y, -1f);
                tweener.AddTween(transform, transform.position, endpos, 0.5f);
                animator.SetInteger("Direction", lastInput);
                animator.speed = 1;
                if (hitColliders[0].gameObject.GetComponent<SpriteRenderer>().sprite == blank)
                {
                    audio.clip = walk;

                    audio.loop = true;
                    audio.Play();
                    collisionchecker = 0;
                    audio.Play();
                }
                if (hitColliders[0].gameObject.GetComponent<SpriteRenderer>().sprite == pellet || hitColliders[0].gameObject.tag == "powerpellet")
                {
                    audio.clip = pelletwalk;
                    audio.loop = true;
                    audio.Play();
                    collisionchecker = 0;
                    audio.Play();
                    currentpos = hitColliders[0].gameObject;
                }
                //particle.Play();
                particle.Emit(20);


            }
            else if (hitColliders[0].gameObject.tag != "Walkable")
            {
                Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll(transform.position + directions[currentInput], 0.2f);
                if (hitColliders2[0].gameObject.tag == "Walkable" || hitColliders2[0].gameObject.tag == "powerpellet") 
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
                       
                        audio.Play();
                    }
                    if (hitColliders2[0].gameObject.GetComponent<SpriteRenderer>().sprite == pellet || hitColliders2[0].gameObject.tag == "powerpellet")
                    {
                        audio.clip = pelletwalk;
                        audio.Play();
                        collisionchecker = 0;
                        currentpos = hitColliders2[0].gameObject;
                    }
                    //particle.Play();
                    particle.Emit(20);
                }
                if (hitColliders2[0].gameObject.tag != "Walkable" && collisionchecker == 0)
                {
                    animator.speed = 0;
                    audio.loop = false;
                    audio.clip = Collision;
                    audio.Play();
                    collisionchecker += 1;
                    Transform pos = hitColliders2[0].gameObject.transform;
                    collisionparticle.transform.position = new Vector3(pos.position.x - 0.3f, pos.position.y, -1);
                    collisionparticle.Emit(20);
                }
            }
            if (tweener.TweenExists(transform))
            {

            }
            

        }
    }

    public Animator Clyde;
    public Animator Inky;
    public Animator Pinky;
    public Animator Blinky;
    public TextMeshProUGUI timer;
    public AudioClip normalstate;
    private float countdown = 10f;

    private IEnumerator scaredstate()
    {
        
        mainAudio.clip = scared;
        mainAudio.Play();
        Clyde.SetInteger("Direction", 5);
        Inky.SetInteger("Direction", 5);
        Pinky.SetInteger("Direction", 5);
        Blinky.SetInteger("Direction", 5);
        while (countdown >=0 )
        {
            timer.text = countdown.ToString("F0");
            yield return new WaitForSeconds(1f);
            countdown--;
            if (countdown == 3)
            {
                Clyde.SetInteger("Direction", 10);
               Inky.SetInteger("Direction", 10);
                Pinky.SetInteger("Direction", 10);
                Blinky.SetInteger("Direction", 10);
            }
        }
        Clyde.SetInteger("Direction", 1);
        Inky.SetInteger("Direction", 1);
        Pinky.SetInteger("Direction", 1);
        Blinky.SetInteger("Direction", 1);
        countdown = 10f;
        mainAudio.clip = normalstate;
        mainAudio.Play();
    }

}
