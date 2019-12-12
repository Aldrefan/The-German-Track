using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoKenneth : MonoBehaviour
{
    public GameObject player;
    public bool isInHelp;
    [Space(20)]
    public GameObject topBoxHelp;
    public GameObject midBoxHelp;
    public GameObject bottomBoxHelp;
    [Space(20)]
    public string textSkipTuto;
    public TutoDone tutoDone;
    [System.Serializable]
    public class TutoDone
    {
        public bool dialogsDone;
        public bool newStickerDone;
        public bool questionsDone;
        public bool quitDialogDone;
        public bool moveDone;
        public bool interactionDone;
        public bool openBookDone;
        public bool navigateBookDone;
        public bool menuDone;
        public bool sprintDone;
        public bool boardDone;
    }

    [Header("Tutorials")]
    public Tuto dialogs;
    public Tuto newSticker;
    public Tuto questions;
    public Tuto quitDialog;
    public Tuto move;
    public Tuto interaction;
    public Tuto openBook;
    public Tuto navigateBook;
    public Tuto menu;
    public Tuto sprint;
    public Tuto board;

    [System.Serializable]
    public class Tuto
    {
        public enum PositionBoxHelp
        {
            Top, 
            Mid, 
            Bottom
        };
        public PositionBoxHelp positionBoxHelp;
        public bool canClose;
        [Space(30)]
        public string keyTitle;
        public string keyText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkTuto(string tutoEvent)
    {
        switch (tutoEvent)
        {
            //When Clara starts dialog
            case "dialogs":
                if(!tutoDone.dialogsDone)
                {
                    openBoxHelp(dialogs);
                }
                break;

            //When pass to sentence 2
            case "endDialogs":
                closeBoxHelp();
                break;

            //When obtain Clara sticker
            case "newSticker":
                if(!tutoDone.newStickerDone)
                {
                    openBoxHelp(newSticker);
                }
                break;

            //When it's 1st transision quote
            case "questions":
                if(!tutoDone.questionsDone)
                {
                    openBoxHelp(questions);
                }
                break;
            
            //When ask a question
            case "questionAsked":
                closeBoxHelp();
                break;
            
            //When its transition quote again
            case "quitDialog":
                if(!tutoDone.quitDialogDone)
                {
                    openBoxHelp(quitDialog);
                }
                break;
            
            //When use E with something
            case "interacDone":
                closeBoxHelp();
                break;
            
            default:
                break;
        }
    }

    public void skipTuto()
    {
        closeTopBoxHelp();
        closeMidBoxHelp();
        closeBottomBoxHelp();

        tutoDone.dialogsDone = true;
        tutoDone.newStickerDone = true;
        tutoDone.questionsDone = true;
        tutoDone.quitDialogDone = true;
        tutoDone.moveDone = true;
        tutoDone.interactionDone = true;
        tutoDone.openBookDone = true;
        tutoDone.navigateBookDone = true;
        tutoDone.menuDone = true;
        tutoDone.sprintDone = true;
        tutoDone.boardDone = true;
    }

    void openBoxHelp(Tuto tempTuto)
    {
        GameObject tempBoxHelp = null;

        switch (tempTuto.positionBoxHelp)
        {
            case Tuto.PositionBoxHelp.Top:
                tempBoxHelp = topBoxHelp;
                break;
            case Tuto.PositionBoxHelp.Mid:
                tempBoxHelp = midBoxHelp;
                break;
            case Tuto.PositionBoxHelp.Bottom:
                tempBoxHelp = bottomBoxHelp;
                break;
            default:
                break;
        }

        tempBoxHelp.SetActive(true);

        if(tempTuto.canClose)
        {
            tempBoxHelp.transform.GetChild(4).gameObject.SetActive(true);
        }
        else tempBoxHelp.transform.GetChild(4).gameObject.SetActive(false);

        isInHelp = true;
        
        tempBoxHelp.transform.GetChild(1).GetComponent<Text>().text = "- " + LanguageManager.Instance.GetDialog(tempTuto.keyTitle) + " -";
        tempBoxHelp.transform.GetChild(2).GetComponent<Text>().text = LanguageManager.Instance.GetDialog(tempTuto.keyText);
        tempBoxHelp.transform.GetChild(3).GetComponent<Text>().text = LanguageManager.Instance.GetDialog(textSkipTuto);
    }

    void closeBoxHelp()
    {
        closeTopBoxHelp();
        closeMidBoxHelp();
        closeBottomBoxHelp();
    }

    public void closeTopBoxHelp()
    {
        topBoxHelp.SetActive(false);
        isInHelp = false;
    }
    public void closeMidBoxHelp()
    {
        midBoxHelp.SetActive(false);
        isInHelp = false;
    }
    public void closeBottomBoxHelp()
    {
        bottomBoxHelp.SetActive(false);
        isInHelp = false;
    }

    public void openBoardBoxHelp()
    {
        openBoxHelp(board);
    }
}
