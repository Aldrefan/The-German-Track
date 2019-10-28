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

public class SentencePatern
{
    public int index;
    public string speaker;
    public string fullSentence;
    public int indexOutput;

    public SentencePatern(int newIndex, string newSpeaker, string newFullSentence, int newIndexOutput)
    {
        index = newIndex;
        speaker = newSpeaker;
        fullSentence = newFullSentence;
        indexOutput = newIndexOutput;
    }
}
