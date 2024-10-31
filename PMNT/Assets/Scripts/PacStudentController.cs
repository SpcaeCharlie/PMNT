using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(0, 0), 0.2f);
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log("Found object: " + hitCollider.gameObject.transform.position);
            }
        }
    }
}
