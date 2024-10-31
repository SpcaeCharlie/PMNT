using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int[,] generatorarray;

    public Sprite[] sprites;

    public RuntimeAnimatorController pelletanim;

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

        Camera.main.orthographicSize = generatorarray.GetLength(1);



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
        tile.transform.position = new Vector3(-6.75f, 7.25f, 0);
        tile.AddComponent<BoxCollider2D>();
        tile.tag = "Walkable";

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

                //corner rotations
                if (tilenum == 1 || tilenum == 3)
                {
                    //covers if the corner is on the border 
                    if (IsOnBorder(i, j, Levelx, Levely))
                    {
                        
                        if (north && !south && !east && !west) {
                            Debug.Log("north");
                            if (j == 0)
                            {
                                west = true;
                            }
                            if (j == Levely - 1)
                            {
                                east = true;
                            }
                        }

                        if (south && !north && !east && !west)
                        {
                            Debug.Log("south");
                            if (j == 0)
                            {
                                west = true;
                            }
                            if (j == Levely - 1)
                            {
                                east = true;
                            }
                        }

                        if (east && !west && !north && !south)
                        {
                            if (i == 0) 
                            {
                                north = true;
                            }
                            if (i == Levelx - 1)
                            {
                                south = true;
                            }
                        }

                        if (west == true && east == false && north == false && south == false)
                        {
                            if (i == 0) 
                            {
                                north = true;
                            }
                            if (i == Levelx - 1)
                            {
                                south = true;
                            }
                        }
                    }
                    
                    //covers when the corner connects to only two other tiles
                        if (south == true && east == true && north == false && west == false)
                        {
                            rotation = new Vector3(0, 0, 0);
                        }
                        else if (east == true && north == true && west == false && south == false)
                        {
                            rotation = new Vector3(0, 0, 270);
                        }
                        else if (north == true && west == true && south == false && east == false)
                        {
                            rotation = new Vector3(0, 0, 180);
                        }
                        else if (west == true && south == true && east == false && north == false)
                        {
                            rotation = new Vector3(0, 0, 90);
                        }

                        //covers if the corner is adjacent more than two valid tiles
                        else if (south == true && east == true && isrotateable(generatorarray[j + 1, i + 1]) == false)
                        {
                            rotation = new Vector3(0, 0, 0);
                        }
                        else if (east == true && north == true && isrotateable(generatorarray[j + 1, i - 1]) == false)
                        {
                            rotation = new Vector3(0, 0, 270);
                        }
                        else if (north == true && west == true && isrotateable(generatorarray[j - 1, i - 1]) == false)
                        {
                            rotation = new Vector3(0, 0, 180);
                        }
                        else if (west == true && south == true && isrotateable(generatorarray[j - 1, i + 1]) == false)
                        {
                            rotation = new Vector3(0, 0, 90);
                            test += 1;
                        }
                    
                }

                //covers straight peices 
                if (tilenum == 2 || tilenum == 4)
                {
                    if (IsOnBorder(i, j, Levelx, Levely))
                    {
                        if(j ==0)
                        {
                            west = true;
                        }
                        if (j == Levely-1)
                        {
                            east = true;
                        }
                        if (i ==0)
                        {
                            north = true;
                        }
                        if (i==Levelx-1)
                        {
                            south = true;
                        }
                    }

                        if (north == true && south == true)
                    {
                        rotation = new Vector3(0, 0, 0);
                    }
                    else if(east == true && west == true)
                    {
                        rotation = new Vector3(0, 0, 90);
                    }
                    
                }


                GameObject temp = Instantiate(tile, new Vector3((0.5f *i)-6.75f, (-0.5f * j)+7.25f, 0), Quaternion.Euler(rotation), generatedlevel.transform);
                    temp.GetComponent<SpriteRenderer>().sprite = sprites[tilenum];
                if (tilenum == 6)
                {
                   Animator animator = temp.AddComponent<Animator>();
                    animator.runtimeAnimatorController = pelletanim;
                }
                if (tilenum == 7 || tilenum == 1 || tilenum == 2 || tilenum == 3 || tilenum == 4)
                {
                    temp.tag = "Unwalkable";
                }
            }
        }

        Instantiate(generatedlevel, new Vector3(0, 0.5f, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
        Instantiate(generatedlevel, new Vector3(0, 0.5f, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
        Instantiate(generatedlevel, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 180, 0)));





    }
    void removecurrentlevel()
        {

            Destroy(GameObject.Find("Level"));
        }

    bool isrotateable(int tiletype)
    {
        return tiletype == 1 || tiletype == 2 || tiletype == 3 || tiletype == 4 || tiletype == 7;
    }

    bool IsOnBorder(int x, int y, int levelWidth, int levelHeight)
    {
        return (x == 0 || x == levelWidth - 1 || y == 0 || y == levelHeight - 1);
    }


}
