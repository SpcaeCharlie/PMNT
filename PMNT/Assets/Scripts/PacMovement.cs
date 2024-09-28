using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{
    public GameObject pac;

    private Animator pacanim;

    float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        pacanim = pac.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pac.transform.position.x >=-3.75f && pac.transform.position.y >4.75f)
        {
            pac.transform.position += Vector3.down * speed * Time.deltaTime;
            pacanim.Play("PacDown");
        }
        
        if (pac.transform.position.x > -6.19f && pac.transform.position.y <= 4.75f)
        {
            pac.transform.position += Vector3.left * speed * Time.deltaTime;
            pacanim.Play("PacLeft");
        }

        if (pac.transform.position.x <= -6.19 && pac.transform.position.y < 6.69)
        {
            pac.transform.position += Vector3.up * speed * Time.deltaTime;
            pacanim.Play("PacUp");
        }
        if (pac.transform.position.x < -3.75 && pac.transform.position.y >= 6.69)
        {
           pac.transform.position += Vector3.right * speed * Time.deltaTime;
            pacanim.Play("PacRight");
        }

   
    }
}
