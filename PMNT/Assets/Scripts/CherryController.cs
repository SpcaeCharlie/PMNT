using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CherryController : MonoBehaviour
{

    public GameObject cherry;
    public Tweener tweener;
    private Camera mainCamera;
    private List<GameObject> templist;

    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        templist = new List<GameObject>();
        {

           InvokeRepeating(nameof(cherryplacer), 0f, 10f);
        }

        
    }
private void cherryplacer()
        {

        for (int i= 0; i < templist.Count; i++)
        {
            if (!tweener.TweenExists(templist[i].transform))
            {
                GameObject tempd = templist[i];
                templist.RemoveAt(i);
                Destroy(tempd);
            }
        }

           GameObject temp = Instantiate(cherry, transform.position, Quaternion.identity);
        temp.SetActive(true);
        templist.Add(temp);
        float ypos = Random.Range(1f, -1f);
        int xpos = Random.Range(0, 2);
        Debug.Log(xpos);
        if (xpos ==0 )
        {
            Vector3 leftScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 rightScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.nearClipPlane));
            tweener.AddTween(temp.transform, new Vector3(leftScreenEdge.x - 5, ypos, -1), new Vector3(rightScreenEdge.x+5, ypos, -1 ), 8) ;
        }
        if (xpos ==1 ) {
            Vector3 leftScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 rightScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.nearClipPlane));
            tweener.AddTween(temp.transform, new Vector3(rightScreenEdge.x + 5, ypos, -1), new Vector3(leftScreenEdge.x - 5, ypos, -1), 8);
        }
        


    }
    // Update is called once per frame
    void Update()
    {
        if (templist.Count > 0)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(templist[0].transform.position, 2f);
            if (hitColliders.Length > 0)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                   // Debug.Log(hitColliders[i].gameObject.tag);
                    if (hitColliders[i].gameObject.tag == "test")
                    {
                        Debug.Log("hit");
                        int temp = int.Parse(score.text);
                        score.text = (temp + 300).ToString();
                        templist[0].SetActive(false);
                    }
                }
            }
        }
    }
}
