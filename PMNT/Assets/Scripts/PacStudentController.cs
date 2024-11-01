using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{

    private Vector3 lastInput;
    public Tweener tweener;
    private Vector3 currentInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = new Vector3(0f, 0.5f,0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            lastInput = new Vector3(0f, -0.5f,0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = new Vector3(0.5f, 0f,0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = new Vector3(-0.5f, 0f,0);
        }

        if (!tweener.TweenExists(transform))
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position+lastInput, 0.2f);
            if (hitColliders[0].gameObject.tag == "Walkable")
            {
                currentInput = lastInput;
                Vector3 endpos = new Vector3(hitColliders[0].gameObject.transform.position.x, hitColliders[0].gameObject.transform.position.y, -1f);
                tweener.AddTween(transform, transform.position, endpos, 0.5f);
            }
            if (hitColliders[0].gameObject.tag != "Walkable")
            {
                Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll(transform.position + currentInput, 0.2f);
                if (hitColliders2[0].gameObject.tag == "Walkable")
                {

                    Vector3 endpos2 = new Vector3(hitColliders2[0].gameObject.transform.position.x, hitColliders2[0].gameObject.transform.position.y, -1f);

                    tweener.AddTween(transform, transform.position, endpos2, 0.5f);

                }
            }
        }

       
    }
}
