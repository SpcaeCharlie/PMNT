using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationcycler : MonoBehaviour
{

    private Animator animator;
    private int i = 2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(animationloop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator animationloop()
    {
        while (true)
        {

            yield return new WaitForSeconds(3);
            animator.SetInteger("Direction", i);
            i++;
            if (i > 10)
            {
                i = 1;
            }
        }
    }

}
