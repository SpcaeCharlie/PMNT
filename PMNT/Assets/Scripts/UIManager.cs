using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Image image;
    private RectTransform imagetransform;
    private RectTransform title;
    private RectTransform subtitle;
    public GameObject inka;
    public GameObject Clyda;
    public GameObject Pinka;
    public GameObject Blinka;
    public GameObject Pac;



    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.transform.GetChild(0).GetComponent<Image>();
        imagetransform = image.gameObject.GetComponent<RectTransform>();
        imagetransform.sizeDelta = new Vector2(Screen.width, Screen.height);


        title = gameObject.transform.GetChild(1).GetComponent<RectTransform>();
        title.sizeDelta = new Vector2(Screen.width, Screen.height / 5);
        title.anchoredPosition = new Vector2(0, Screen.height/2.6f);
        title.GetComponent<Text>().fontSize = Screen.height / 20;
        subtitle = gameObject.transform.GetChild(2).GetComponent<RectTransform>();
        subtitle.sizeDelta = new Vector2(Screen.width, Screen.height / 5);
        subtitle.anchoredPosition = new Vector2(0, Screen.height / 3f);
        subtitle.GetComponent<Text>().fontSize = Screen.height / 28;

        inka.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        inka.GetComponent<RectTransform>().anchoredPosition = new Vector2((Screen.height/10), -(Screen.height/10));
        Clyda.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        Clyda.GetComponent<RectTransform>().anchoredPosition = new Vector2((Screen.height / 10), (Screen.height / 10));
        Blinka.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        Blinka.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(Screen.height / 10), (Screen.height / 10));
        Pinka.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        Pinka.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(Screen.height / 10), -(Screen.height / 10));
        Pac.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        Pac.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height/10);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1Load()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
