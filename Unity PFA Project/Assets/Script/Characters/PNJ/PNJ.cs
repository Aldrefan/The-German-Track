using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PNJ : MonoBehaviour
{
    public string PNJName;
    public Sprite characterSprite;
    public Sprite kennethSprite;
    GameObject DialogCanvas;
    public int dialogLine = 0;
    GameObject player;
    GameObject leftPanel;
    GameObject rightPanel;
    GameObject questionPanel;
    Transform carnet;
    public int dialogIndex;
    public int transitionQuote;
    public int negativeQuote;
    string stringEventTrigger;
    public bool haveEvent;

    public List<int> stickerAlreadyGivenList;

    public StickerRedirection stickerRedirection;
    [System.Serializable]
    public class StickerRedirection
    {
        public List<int> stickerGivenList;
        public List<int> redirectionList;
    }

    public EventRedirection eventRedirection;
    [System.Serializable]
    public class EventRedirection
    {
        public List<string> eventGivenList; //dans l'ordre inverse des priorités
        public List<int> redirectionEventList;
    }

    [System.Serializable]
    public class Quote
    {
        public List<DialogCapacity> listOfDialogs;
    }

    [System.Serializable]
    public class DialogCapacity
    {
        public List<ArrayOfQuotes> dialog;
        public bool canAskQuestions;
        public bool hasChoices;
        public bool endDialog;
        public List<TheGermanTrack.Button> buttonsList;
    }

    [System.Serializable]
    public class ArrayOfQuotes
    {
        public string quote;
        public string characterName;
        public bool isInLeftSide;
        public List<int> newStickerIndexList;
        public List<string> eventTrigger;
        public Sprite spriteCharacter;
    }
    
    string currentLine = "";
    float dialogDelay = 0.01f;
    public Quote allDialogs;
    public bool quoteFinished;
    IEnumerator show;
    public List<int> stickersAlreadyGiven;
    string actualQuote;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.Find("CarnetUI"))
        {
            carnet = GameObject.Find("CarnetUI").transform;
        }
        if (GameObject.Find("Ken_Dial_Book_FlCanvas") != null)
        {
            DialogCanvas = GameObject.Find("Ken_Dial_Book_FlCanvas");
            leftPanel = DialogCanvas.GetComponent<Ken_Canvas_Infos>().leftPanel;
            rightPanel = DialogCanvas.GetComponent<Ken_Canvas_Infos>().rightPanel;
            questionPanel = DialogCanvas.GetComponent<Ken_Canvas_Infos>().questionPanel;
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        
    }

    public void ChangeDialog(int newDialog)
    {
        player.GetComponent<Interactions>().isInDialog = true;
        if(newDialog == transitionQuote)
        {
            player.GetComponent<Interactions>().QuitCinematicMode();
        }

        if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth")
        {
            if(allDialogs.listOfDialogs[newDialog].canAskQuestions)
            {
                carnet.GetComponent<Animator>().SetBool("InDialog", true);
                //player.GetComponent<Interactions>().canOpenCarnet = true;// Initial
                DialogCanvas.transform.GetChild(3).gameObject.SetActive(true);
            }
            else 
            {
                carnet.GetComponent<Animator>().SetBool("ClickOn", true);
                carnet.GetComponent<Animator>().SetBool("InDialog", false);
                //player.GetComponent<Interactions>().canOpenCarnet = false;// Initial
                DialogCanvas.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        
        dialogLine = 0;
        dialogIndex = newDialog;
        Startdialogue();
    }

    public void ResponseEvent()
    {
        haveEvent = false;
        for(int i = eventRedirection.eventGivenList.Count - 1; i >= 0; i--)
        {
            if(player.GetComponent<EventsCheck>().eventsList.Contains(eventRedirection.eventGivenList[i].ToString()))
            {
                ChangeDialog(eventRedirection.redirectionEventList[i]);
                haveEvent = true;
                break;
            }
        }
    }

    public void Startdialogue()
    {
        if(GetComponent<Animator>())
        {
            GetComponent<Animator>().SetBool("Talk", true);
        }
        if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth")
        {
            /*if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.transform.position.x < transform.position.x && GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.GetComponent<Interactions>().state == Interactions.State.InDialog)
            {
                transform.GetComponent<SpriteRenderer>().flipX = false;
            }
            else transform.GetComponent<SpriteRenderer>().flipX = true;*/
            if(allDialogs.listOfDialogs[dialogIndex].canAskQuestions)
            {
                carnet.GetComponent<Animator>().SetBool("InDialog", true);
                //player.GetComponent<Interactions>().canOpenCarnet = true;// Initial
            }
            else 
            {
                carnet.GetComponent<Animator>().SetBool("InDialog", false);
                //player.GetComponent<Interactions>().canOpenCarnet = false;// Initial
            }
        }
        if(GameObject.Find("BlackBands").GetComponent<Animator>().GetBool("Cinematic"))
        {}
        else GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", true);

        if(dialogLine < allDialogs.listOfDialogs[dialogIndex].dialog.Count)
        {
            if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].isInLeftSide)
            {
                leftPanel.SetActive(true);
                rightPanel.SetActive(false);
                leftPanel.transform.GetChild(2).GetComponent<Text>().text = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName;
                show = ShowText(leftPanel);
                StartCoroutine(show);
                //leftPanel.transform.GetChild(0).GetComponent<Text>().text = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote;
                if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName == PNJName)
                {
                    leftPanel.transform.GetChild(4).GetComponent<Image>().sprite = characterSprite;
                }
                else leftPanel.transform.GetChild(4).GetComponent<Image>().sprite = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].spriteCharacter;
                if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName == "Kenneth")
                {
                    leftPanel.transform.GetChild(4).GetComponent<Image>().sprite = kennethSprite;
                }
            }
            else 
            {
                rightPanel.SetActive(true);
                leftPanel.SetActive(false);
                rightPanel.transform.GetChild(2).GetComponent<Text>().text = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName;
                show = ShowText(rightPanel);
                StartCoroutine(show);
                //rightPanel.transform.GetChild(0).GetComponent<Text>().text = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote;
                if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName == PNJName)
                {
                    rightPanel.transform.GetChild(4).GetComponent<Image>().sprite = characterSprite;
                }
                else rightPanel.transform.GetChild(4).GetComponent<Image>().sprite = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].spriteCharacter;
                if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName == "Kenneth")
                {
                    rightPanel.transform.GetChild(4).GetComponent<Image>().sprite = kennethSprite;
                }
            }
        }
        else
        {
            if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth")
            {
                if(allDialogs.listOfDialogs[dialogIndex].endDialog)
                {
                    EndDialog();
                    //player.GetComponent<Interactions>().PNJContact = null;
                }
                else ChangeDialog(transitionQuote);
            }
            if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Leon")
            {
                if(allDialogs.listOfDialogs[dialogIndex].hasChoices)
                {
                    questionPanel.SetActive(true);
                    rightPanel.SetActive(false);
                    leftPanel.SetActive(false);
                    questionPanel.GetComponent<QuestionInterface>().InstantiateQuestions(allDialogs.listOfDialogs[dialogIndex].buttonsList);
                }
                else if(allDialogs.listOfDialogs[dialogIndex].endDialog)
                {
                    EndDialog();
                }
                else ChangeDialog(1);
            }
        }
    }

    public void EndDialog()
    {
        if(GetComponent<Animator>())
        {
            GetComponent<Animator>().SetBool("Talk", false); // Temporaire (A changer le plus vite possible)
        }
        /*if(!allDialogs.listOfDialogs[dialogIndex].canAskQuestions)
        {
            allDialogs.listOfDialogs[dialogIndex].canAskQuestions = true;
        }*/
        if(transform.parent && transform.parent.GetComponent<Phone>())
        {
            transform.parent.GetComponent<BoxCollider2D>().enabled = true;
        }
        player.GetComponent<Interactions>().EndDialog();
        rightPanel.SetActive(false);
        leftPanel.SetActive(false);
        dialogLine = 0;
    }

    public void Response(int stickerIndex)
    {
        StickersGivenToPNJ.SGTP.AddStickerInAList(PNJName, stickerIndex);
        if(!stickerAlreadyGivenList.Contains(stickerIndex))
        {
            stickerAlreadyGivenList.Add(stickerIndex);
        }
        for(int i = 0; i < stickerRedirection.stickerGivenList.Count; i++)
        {
            if(stickerIndex == stickerRedirection.stickerGivenList[i])
            {
                ChangeDialog(stickerRedirection.redirectionList[i]);
                break;
            }
        }
        if(stickerRedirection.stickerGivenList.Contains(stickerIndex))
        {}
        else ChangeDialog(negativeQuote);
        if(stickerAlreadyGivenList.Contains(stickerIndex))
        {}
        else stickerAlreadyGivenList.Add(stickerIndex);
    }

    public IEnumerator ShowText(GameObject panel)
    {
        quoteFinished = false;
        //Debug.Log(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote);
        actualQuote = LanguageManager.Instance.GetDialog(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote);

        panel.transform.GetChild(1).GetComponent<Text>().text = actualQuote;
        actualQuote = panel.transform.GetChild(1).GetComponent<Text>().text;
        //int length = 0;
        //if(actualQuote.Contains("<b>"))
        //{
            //length = actualQuote.Length - 6;
        //}
        //else length = actualQuote.Length;
        for(int i = 0; i < actualQuote.Length/*length*/ + 1; i++)
        {
            /*if(actualQuote.Substring(i, i + 2) == "<b>")
            {
                Debug.Log("It's a '<b>' !");
                i += 2;
            }
            if(actualQuote.Substring(i, i + 3) == "</b>")
            {
                Debug.Log("It's a '</b>' !");
                i += 3;
            }*/
            currentLine = actualQuote.Substring(0,i);
            panel.transform.GetChild(1).GetComponent<Text>().text = currentLine;
            yield return new WaitForSeconds(dialogDelay);
        }
        quoteFinished = true;
        DialogSecondPhase();
    }

    public void FullQuote(GameObject panel)
    {
        StopCoroutine(show);
        quoteFinished = true;
        panel.transform.GetChild(1).GetComponent<Text>().text = LanguageManager.Instance.GetDialog(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote);
        DialogSecondPhase();
    }

    void DialogSecondPhase()
    {
        if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Count > 0)
        {
            for(int i = 0; i < allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Count; i++)
            {
                if(player.GetComponent<PlayerMemory>().allStickers.Contains(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]))
                {
                }
                else 
                {
                    player.GetComponent<PlayerMemory>().KeepInMemory(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                    player.GetComponent<PlayerMemory>().allStickers.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                }
            }
            allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.RemoveRange(0, allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Count);
        }

        //add or not event
        if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.Count > 0)
        {
            //JsonSave save = SaveGameManager.GetCurrentSave();
            for(int i = 0; i < allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.Count; i++)
            {
                if(player.GetComponent<EventsCheck>().eventsList.Contains(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i]))
                {}
                else 
                {
                    player.GetComponent<EventsCheck>().eventsList.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i]);
                    player.GetComponent<EventsCheck>().CheckEvents(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i]);
                }
            }
            //allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.RemoveRange(0, allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.Count);
        }
        dialogLine++;
    }
}