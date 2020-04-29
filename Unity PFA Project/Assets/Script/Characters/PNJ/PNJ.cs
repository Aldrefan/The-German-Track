using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PNJ : MonoBehaviour
{
    public string PNJName;
    GameObject dialogCanvas;
    public int dialogLine = 0;
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
        public List<int> stickerGivenList = new List<int>();
        public List<int> redirectionList = new List<int>();
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
        public Quote()
        {
            listOfDialogs = new List<DialogCapacity>();
        }
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
        public bool isInLeftSide;
        public List<int> newStickerIndexList;
        public List<string> eventTrigger;
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
        if (CanvasManager.CManager != null)
        {

            carnet = CanvasManager.CManager.GetCanvas("CarnetUI").transform;
            //DialogCanvas = ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<Interactions>().dialAndBookCanvas;
            dialogCanvas = CanvasManager.CManager.GetCanvas("Dialogue");
            leftPanel = CanvasManager.CManager.GetCanvas("Dialogue").transform.GetChild(5).gameObject;
            rightPanel = CanvasManager.CManager.GetCanvas("Dialogue").transform.GetChild(6).gameObject;
        }
        /*if (GameObject.Find("Ken_Dial_Book_FlCanvas") != null)
        {
            questionPanel = DialogCanvas.GetComponent<Ken_Canvas_Infos>().questionPanel;
        }*/

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
        ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<Interactions>().isInDialog = true;
        if(newDialog == transitionQuote)
        {
            ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<Interactions>().QuitCinematicMode();
        }

        if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth")
        {
            if(allDialogs.listOfDialogs[newDialog].canAskQuestions)
            {
                carnet.GetComponent<Animator>().SetBool("InDialog", true);
                //player.GetComponent<Interactions>().canOpenCarnet = true;// Initial
                dialogCanvas.transform.GetChild(4).gameObject.SetActive(true);
            }
            else 
            {
                carnet.GetComponent<Animator>().SetBool("ClickOn", true);
                carnet.GetComponent<Animator>().SetBool("InDialog", false);
                //player.GetComponent<Interactions>().canOpenCarnet = false;// Initial
                dialogCanvas.transform.GetChild(4).gameObject.SetActive(false);
            }
        }
        
        dialogLine = 0;
        dialogIndex = newDialog;
        Startdialogue();
    }

    public void ResponseEvent()
    {
        haveEvent = false;
        for(int i = 0 - 1; i < eventRedirection.eventGivenList.Count; i++)
        {
            if(ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<EventsCheck>().eventsList.Contains(eventRedirection.eventGivenList[i].ToString()))
            {
                ChangeDialog(eventRedirection.redirectionEventList[i]);
                haveEvent = true;
                //break;
            }
        }
    }

    public void Startdialogue()
    {
        if(GetComponent<Animator>())
        {
            GetComponent<Animator>().SetBool("Talk", true);
        }
        if(ActiveCharacterScript.ActiveCharacter.actualCharacter.name == "Kenneth")
        {
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
                leftPanel.GetComponent<DialogInterface>().HideHelp();
                string [] words = LanguageManager.Instance.GetNameOfTheSpeaker(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote).Split("_"[0]);
                leftPanel.transform.GetChild(2).GetComponent<Text>().text = words[0];
                StartCoroutine(ShowText(leftPanel));
                //show = ShowText(leftPanel);  // Works
                //StartCoroutine(show);  // Works
                //leftPanel.transform.GetChild(0).GetComponent<Text>().text = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote;
                leftPanel.transform.GetChild(4).GetComponent<Image>().sprite = LanguageManager.Instance.GetCharacterSprite(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote);
                /*if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName == PNJName)
                {
                    leftPanel.transform.GetChild(4).GetComponent<Image>().sprite = characterSprite;
                }
                else leftPanel.transform.GetChild(4).GetComponent<Image>().sprite = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].spriteCharacter;
                if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName == "Kenneth")
                {
                    leftPanel.transform.GetChild(4).GetComponent<Image>().sprite = ActiveCharacterScript.ActiveCharacter.GetCharacterSprite("Kenneth"); // KennethSprite;
                }*/
            }
            else 
            {
                rightPanel.SetActive(true);
                leftPanel.SetActive(false);
                rightPanel.GetComponent<DialogInterface>().HideHelp();
                string [] words = LanguageManager.Instance.GetNameOfTheSpeaker(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote).Split("_"[0]);
                rightPanel.transform.GetChild(2).GetComponent<Text>().text = words[0];
                StartCoroutine(ShowText(rightPanel));
                //show = ShowText(rightPanel);  // Works
                //StartCoroutine(show);  // Works
                //rightPanel.transform.GetChild(0).GetComponent<Text>().text = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote;
                rightPanel.transform.GetChild(4).GetComponent<Image>().sprite = LanguageManager.Instance.GetCharacterSprite(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote);
                /*if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName == PNJName)
                {
                    rightPanel.transform.GetChild(4).GetComponent<Image>().sprite = characterSprite;
                }
                else rightPanel.transform.GetChild(4).GetComponent<Image>().sprite = allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].spriteCharacter;
                if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].characterName == "Kenneth")
                {
                    rightPanel.transform.GetChild(4).GetComponent<Image>().sprite = ActiveCharacterScript.ActiveCharacter.GetCharacterSprite("Kenneth");
                }*/
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
        ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<Interactions>().EndDialog();
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
        panel.transform.GetChild(3).GetComponent<Image>().enabled = false;
        actualQuote = LanguageManager.Instance.GetDialog(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote);
        
        string [] words = actualQuote.Split(" "[0]);
        string[] actualWordList = new string[words.Length];
        string actualQuoteState = "";
        string quoteStructure = "";

        for(int n = 0; n < words.Length; n++)
        {
            if(BalisesManager.Balises.IsABalise(words[n])) {quoteStructure += words[n]; actualWordList[n] = words[n];}
            else 
            {
                quoteStructure += " ";
                actualWordList[n] = " ";
                if(n < words.Length - 1)
                {
                    quoteStructure += " ";
                }
            }
        }
        for(int i = 0; i < words.Length; i++)           //Fait la phrase finale mot par mot => phrase finale "May I <b> help </b> you ?
        {
            if(!BalisesManager.Balises.IsABalise(words[i]))
            {actualWordList[i] = words[i];}
            
            string currentWord = words[i];
            actualWordList[i] = "";
            for(int x = 0; x < words[i].Length + 1; x++)                     //S'occupe de générer le nouveau mot => May => I => <b>
            {
                actualQuoteState = "";
                actualWordList[i] = currentWord.Substring(0, x);
                for(int n = 0; n < words.Length; n++)                        //Fait la nouvelle phrase => quoteState "May _ <b> _ </b> _ _" => "May I <b> _ </b> _ _"
                {
                    actualQuoteState += actualWordList[n];
                    //Debug.Log("Actual Quote State : " + actualQuoteState + "index : " + n);
                    if(n < actualWordList.Length - 1 && !BalisesManager.Balises.IsABalise(actualWordList[n]))
                    {
                        actualQuoteState += " ";
                    }
                }
                if(!BalisesManager.Balises.IsABalise(words[i]))
                {
                    panel.transform.GetChild(1).GetComponent<Text>().text = actualQuoteState;       //Afficher la nouvelle phrase
                    yield return new WaitForSeconds(dialogDelay);
                }
            }
        }
        quoteFinished = true;
        panel.transform.GetChild(3).GetComponent<Image>().enabled = true;
        panel.GetComponent<DialogInterface>().StartTimer();
        /*if(dialogIndex != 1)
        {
            panel.transform.GetChild(3).GetComponent<Image>().enabled = true;
        }*/
        DialogSecondPhase();
    }

    public void FullQuote(GameObject panel)
    {
        StopAllCoroutines();
        //StopCoroutine(show);
        quoteFinished = true;
        panel.transform.GetChild(1).GetComponent<Text>().text = LanguageManager.Instance.GetDialog(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].quote);
        if(dialogIndex != 1)
        {
            panel.transform.GetChild(3).GetComponent<Image>().enabled = true;
            panel.GetComponent<DialogInterface>().StartTimer();
        }
        DialogSecondPhase();
    }

    void DialogSecondPhase()
    {
        if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Count > 0)
        {
            for(int i = 0; i < allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Count; i++)
            {
                if(!ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<PlayerMemory>().allStickers.Contains(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]))
                {
                    ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<PlayerMemory>().AddToMemory(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                    //player.GetComponent<PlayerMemory>().KeepInMemory(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                    //player.GetComponent<PlayerMemory>().allStickers.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                    //player.GetComponent<PlayerMemory>().stickerIndexCarnetList.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                    //player.GetComponent<PlayerMemory>().stickerIndexBoardList.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList[i]);
                }
                //allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.RemoveRange(0, allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Count);
                allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].newStickerIndexList.Remove(i);
            }
        }

        //add or not event
        if(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.Count > 0)
        {
            //JsonSave save = SaveGameManager.GetCurrentSave();
            for(int i = 0; i < allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.Count; i++)
            {
                if(!ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<EventsCheck>().eventsList.Contains(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i]))
                {
                    ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<EventsCheck>().eventsList.Add(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i]);
                    ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<EventsCheck>().CheckEvents(allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger[i]);
                }
            }
            //allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.RemoveRange(0, allDialogs.listOfDialogs[dialogIndex].dialog[dialogLine].eventTrigger.Count);
        }
        //dialogLine++;      // Début du probleme
    }
}