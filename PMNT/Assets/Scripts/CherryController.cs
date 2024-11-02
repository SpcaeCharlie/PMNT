using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{

    public GameObject cherry;
    public Tweener tweener;
    private Camera mainCamera;
    private List<GameObject> templist; 
    
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
        float ypos = Random.Range(6.75f, -6.75f);
        int xpos = Random.Range(0, 2);
        Debug.Log(xpos);
        if (xpos ==0 )
        {
            Vector3 leftScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 rightScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.nearClipPlane));
            tweener.AddTween(temp.transform, new Vector3(leftScreenEdge.x - 5, ypos, -1), new Vector3(rightScreenEdge.x+5, ypos, -1 ), 5) ;
        }
        if (xpos ==1 ) {
            Vector3 leftScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 rightScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.nearClipPlane));
            tweener.AddTween(temp.transform, new Vector3(rightScreenEdge.x + 5, ypos, -1), new Vector3(leftScreenEdge.x - 5, ypos, -1), 5);
        }
        


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
