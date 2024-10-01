using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int[,] generatorarray;

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        if(generatorarray == null)
        {
            generatorarray = new int[,] 
{ {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
{2,5,5,5,5,5,5,5,5,5,5,5,5,4},
{2,5,3,4,4,3,5,3,4,4,4,3,5,4},
{2,6,4,0,0,4,5,4,0,0,0,4,5,4},
{2,5,3,4,4,3,5,3,4,4,4,3,5,3},
{2,5,5,5,5,5,5,5,5,5,5,5,5,5},
{2,5,3,4,4,3,5,3,3,5,3,4,4,4},
{2,5,3,4,4,3,5,4,4,5,3,4,4,3},
{2,5,5,5,5,5,5,4,4,5,5,5,5,4},
{1,2,2,2,2,1,5,4,3,4,4,3,0,4},
{0,0,0,0,0,2,5,4,3,4,4,3,0,3},
{0,0,0,0,0,2,5,4,4,0,0,0,0,0},
{0,0,0,0,0,2,5,4,4,0,3,4,4,0},
{2,2,2,2,2,1,5,3,3,0,4,0,0,0},
{0,0,0,0,0,0,5,0,0,0,4,0,0,0}} ;
        }


        removecurrentlevel();
        Generator();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Generator() {

        int Levelx = generatorarray.GetLength(1);
        int Levely = generatorarray.GetLength(0);

        GameObject generatedlevel = new GameObject("generated Level");
        GameObject tile = new GameObject();
        tile.AddComponent<SpriteRenderer>();
        tile.GetComponent<SpriteRenderer>().sprite = sprites[1];
        tile.transform.position = new Vector3(0, 0, 0);

        for (int i = 0; i < Levelx; i++)
        {
            for (int j = 0; j < Levely; j++)
            {
                GameObject temp = Instantiate(tile, new Vector3(0.5f*i, -0.5f*j, 0), Quaternion.identity, generatedlevel.transform);
                temp.GetComponent<SpriteRenderer>().sprite = sprites[generatorarray[j, i]];
            }
        }
    
    



        
    }


void removecurrentlevel()
        {

            Destroy(GameObject.Find("Level"));
        }





}