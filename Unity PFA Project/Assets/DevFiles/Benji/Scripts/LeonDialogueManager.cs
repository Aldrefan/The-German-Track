using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeonDialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();

    [HideInInspector]
    public string npcName;
    string playerName = "Léon";

    Sprite npcSprite;
    Sprite leonSprite;

    GameObject DialogWidget;



    void Start()
    {
        DialogWidget = this.transform.GetChild(0).gameObject;
        InitPlayerSide();
        Enable(DialogWidget.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitDialogWidget()
    {
        Enable(DialogWidget.transform,true);
        if (npcName != null)
        {
            npcSprite = Resources.Load("Resources/Sprites" + npcName) as Sprite;
        }
        if(npcSprite != null)
        {
            this.transform.Find("CharacterPict").GetChild(0).GetComponent<Image>().sprite = npcSprite;
            this.transform.Find("CharacterName").GetChild(0).GetComponent<Text>().text = npcName;
        }
    }

    void Enable(Transform transform,bool state)
    {
        foreach (Transform child in transform)
        {
            if (child.name != "ChoicePanel")
            {
                child.gameObject.SetActive(state);

                Enable(child, state);
            }
        }

    }

    void InitPlayerSide()
    {
        if (playerName != null)
        {
            leonSprite = Resources.Load("Resources/Sprites" + playerName) as Sprite;
        }
        if (leonSprite != null)
        {
            this.transform.Find("CharacLeft").GetChild(0).Find("CharacterPict").GetChild(0).GetComponent<Image>().sprite = leonSprite;
        }
    }

    


}

