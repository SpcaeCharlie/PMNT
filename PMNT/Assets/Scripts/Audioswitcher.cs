using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioswitcher : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip intro;
    public AudioClip ghostnormal;
    public AudioClip ghostscared;
    public AudioClip ghostdead;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(introtransition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private IEnumerator introtransition()
    {
        yield return new WaitForSeconds(6f);
        audioSource.clip = ghostnormal;
        audioSource.Play();
    }

}
