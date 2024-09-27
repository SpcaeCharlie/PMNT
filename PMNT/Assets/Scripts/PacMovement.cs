using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{
    public GameObject pac;

    float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pac.transform.position.x >=-3.75f && pac.transform.position.y >4.75f)
        {
            pac.transform.position += Vector3.down * speed * Time.deltaTime;
        }
       // if (pac.transform.position.x >= -3.75 && pac.transform.position.y >= 6.7)
        //{
       //     pac.transform.position += Vector3.down * speed * Time.deltaTime;
        //}
       // if (pac.transform.position.x >= -3.75 && pac.transform.position.y >= 6.7)
        //{
          //  pac.transform.position += Vector3.down * speed * Time.deltaTime;
       // }
       // if (pac.transform.position.x >= -3.75 && pac.transform.position.y >= 6.7)
        //{
         //   pac.transform.position += Vector3.down * speed * Time.deltaTime;
        //}

   
    }
}
