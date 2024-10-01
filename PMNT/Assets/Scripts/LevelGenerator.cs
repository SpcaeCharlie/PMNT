using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int[,] generatorarray;

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        if (generatorarray == null)
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
{0,0,0,0,0,0,5,0,0,0,4,0,0,0}};
        }


        removecurrentlevel();
        Generator();

    }

    // Update is called once per frame
    void Update()
    {

    }


    void Generator()
    {

        int Levelx = generatorarray.GetLength(1);
        int Levely = generatorarray.GetLength(0);

        GameObject generatedlevel = new GameObject("generated Level");
        GameObject tile = new GameObject();
        tile.AddComponent<SpriteRenderer>();
        tile.GetComponent<SpriteRenderer>().sprite = sprites[1];
        tile.transform.position = new Vector3(0, 0, 0);
        Debug.Log(Levely);
        Debug.Log(Levelx);

        int test = 0;

        for (int i = 0; i < Levelx; i++)
        {
            for (int j = 0; j < Levely; j++)
            {

                bool north= false;
                bool south = false;
                bool east = false;
                bool west = false;
                int tilenum = generatorarray[j, i];
                Vector3 rotation= new Vector3(0,0,0);

                    if (tilenum == 1 || tilenum == 3 || tilenum == 2 || tilenum == 4 || tilenum == 7)
                    {
                    if (i - 1 >= 0 && isrotateable(generatorarray[j, i -1])) { north = true;}
                    if (i + 1 < Levelx && isrotateable(generatorarray[j,i + 1])) { south= true; }
                    if (j - 1 >= 0 && isrotateable(generatorarray[j - 1,i])) {west = true;}
                    if (j + 1 < Levely && isrotateable(generatorarray[j +1, i])){east= true; }
                }
                    if (tilenum == 1 || tilenum == 3)
                    {
                        if (south == true && east == true && north == false && west == false)
                    {
                        rotation = new Vector3(0,0,0);
                    }
                    else if(east == true && north == true && west == false && south == false)
                    {
                        rotation = new Vector3(0, 0, 270);
                    }
                    else if(north == true && west == true && south == false && east == false)
                    {
                        rotation = new Vector3(0, 0, 180);
                    }
                    else if(west == true && south == true && east == false && north == false)
                    {
                        rotation = new Vector3(0, 0, 90);
                    }


                    else if (south == true && east == true && isrotateable(generatorarray[j+1,i+1]) == false ) 
                        {
                        rotation = new Vector3(0, 0, 0);
                        }
                        else if (east == true && north == true && isrotateable(generatorarray[j+1, i-1]) == false)
                        {
                        rotation = new Vector3(0, 0, 270);
                        }
                        else if (north == true && west == true && isrotateable(generatorarray[j-1,i-1]) == false)
                        {
                        rotation = new Vector3(0, 0, 180);
                        }
                        else if (west == true && south == true && isrotateable(generatorarray[j-1, i+1]) == false)
                        {
                        rotation = new Vector3(0, 0, 90);
                        test += 1;
                        }
                }



                GameObject temp = Instantiate(tile, new Vector3(0.5f * i, -0.5f * j, 0), Quaternion.Euler(rotation), generatedlevel.transform);
                    temp.GetComponent<SpriteRenderer>().sprite = sprites[tilenum];
              }
        }






        Debug.Log(test);

    }
    void removecurrentlevel()
        {

            Destroy(GameObject.Find("Level"));
        }


    bool isrotateable(int tiletype)
    {
        return tiletype == 1 || tiletype == 2 || tiletype == 3 || tiletype == 4 || tiletype == 7;
    }


}
