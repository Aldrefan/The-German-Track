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
    int dialogLine = 0;
    GameObject player;
    GameObject leftPanel;
    GameObject rightPanel;
    Transform carnet;
    public int dialogIndex;
    public int transitionQuote;
    public int negativeQuote;
    string stringEventTrigger;
    public bool haveEvent;
    public enum eventEnum
    {
        laissezPasser,
        hopitalOpen,
        policeOpen,
        pibPhoneUnlocked,
        numberMarvinMeyer,
        endDialogWilliamScott,
        callClaraUnlocked,
        numberClaraGrey
    }

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
        public List<eventEnum> eventGivenList; //dans l'ordre inverse des priorités
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
        public bool endDialog;
    }

    [System.Serializable]
    public class ArrayOfQuotes
    {
        public string quote;
        public string characterName;
        public bool isInLeftSide;
        public List<int> newStickerIndexList;
        public List<eventEnum> eventTrigger;
        public Sprite spriteCharacter;
    }
    
    string currentLine = "";
    float dialogDelay = 0.01f;
    public Quote allDialogs;
    public bool quoteFinished;
    IEnumerator show;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        carnet = GameObject.Find("CarnetUI").transform;
        DialogCanvas = GameObject.Find("Ken_Dial_Book_FlCanvas");
        leftPanel = DialogCanvas.GetComponent<Ken_Canvas_Infos>().leftPanel;
        rightPanel = DialogCanvas.GetComponent<Ken_Canvas_Infos>().rightPanel;
    }

    public void ChangeDialog(int newDialog)
    {
        player.GetComponent<Interactions>().isInDialog = true;
        if(newDialog == transitionQuote)
        {
            player.GetComponent<Interactions>().QuitCinematicMode();
        }
        if(allDialogs.listOfDialogs[dialogIndex].canAskQuestions)
        {
            carnet.GetComponent<Animator>().SetBool("InDialog", true);
            //player.GetComponent<Interactions>().canOpenCarnet = true;// Initial
        }
        else 
        {
            carnet.GetComponent<Animator>().SetBool("ClickOn", true);
            //player.GetComponent<Interactions>().canOpenCarnet = false;// Initial
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
        if(allDialogs.listOfDialogs[dialogIndex].canAskQuestions)
        {
            carnet.GetComponent<Animator>().SetBool("InDialog", true);
            //player.GetComponent<Interactions>().canOpenCarnet = true;// Initial
        }
        else 
        {
            carnet.GetComponent<Animator>().SetBool("ClickOn", true);
            //player.GetComponent<Interactions>().canOpenCarnet = false;// Initial
        }
        if(GameObject.Find("BlackBands").GetComponent<Animator>().GetBool("Cinematic"))
        {
        }
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
            if(allDialogs.listOfDialogs[dialogIndex].endDialog)
            {
                EndDialog();
            }
            else ChangeDialog(transitionQuote);
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
        player.GetComponent<Interactions>().EndDialog();
        rightPanel.SetActive(false);
        leftPanel.SetActive(false);
        dialogLine = 0;
    }

    public void Response(int stickerIndex)
    {
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
    }

    public IEnumerator ShowText(GameObject panel)
    {
        quoteFinished = false;
        for(int i = 0; i < allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote.Length; i++)
        {
            currentLine = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote.Substring(0,i);
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
        panel.transform.GetChild(1).GetComponent<Text>().text = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote;
        DialogSecondPhase();
    }

    void DialogSecondPhase()
    {
        if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Count > 0)
        {
            //JsonSave save = SaveGameManager.GetCurrentSave();
            for(int i = 0; i < allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Count; i++)
            {
                if(/*save.memoryStickers.Contains(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]) */player.GetComponent<PlayerMemory>().allStickers.Contains(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]))
                {
                }
                else 
                {
                    player.GetComponent<PlayerMemory>().KeepInMemory(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                    player.GetComponent<PlayerMemory>().allStickers.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                    //save.memoryStickers.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
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
                if(player.GetComponent<EventsCheck>().eventsList.Contains(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i].ToString()))
                {
                }
                else 
                {
                    player.GetComponent<EventsCheck>().eventsList.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i].ToString());
                    player.GetComponent<EventsCheck>().CheckEvents(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i].ToString());
                }
            }
            //allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.RemoveRange(0, allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.Count);
        }
        dialogLine++;
    }
}