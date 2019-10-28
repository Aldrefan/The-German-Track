using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogData : MonoBehaviour
{

    public string characName;

    Sprite characSprite;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        if (characName != null)
        {
            characSprite = Resources.Load("Resources/Sprites" + characName) as Sprite;
        }
        if(characSprite != null)
        {
            this.transform.Find("CharacterPict").GetChild(0).GetComponent<Image>().sprite = characSprite;
            this.transform.Find("CharacterName").GetChild(0).GetComponent<Text>().text = characName;
        }
    }
}

