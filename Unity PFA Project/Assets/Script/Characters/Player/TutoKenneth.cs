using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoKenneth : MonoBehaviour
{
    public GameObject player;
    [HideInInspector]
    public bool isInHelp;
    [HideInInspector]
    public string currentTuto; //Pour checker le tuto en cours quand on ferme sa box
    [HideInInspector]
    public bool canEsc;
    public bool wantSkipTuto;
    public bool canSave;


    int countStickers;
    Vector3 posPlayer;
    bool sticker;


    [Space(20)]
    public GameObject topBoxHelp;
    public GameObject midBoxHelp;
    public GameObject bottomBoxHelp;


    [Space(20)]
    public RefNeeded refNeeded;
    [System.Serializable]
    public class RefNeeded
    {
        public GameObject leftDialog;
        public GameObject rightDialog;
        public GameObject newSticker;
        public GameObject book;
        public GameObject bookOnDialog;
        public GameObject board;
        public GameObject menu;
        public List<GameObject> saveButton;
    }


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
    public string textSkipTuto;
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
        public string endingString;
        [Space(30)]
        public string keyTitle;
        public string keyText;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(wantSkipTuto) skipTuto();
        preventSave();
        canEsc = true;

        if(canSave) allowSave();

        countStickers = player.GetComponent<PlayerMemory>().allStickers.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(!tutoDone.dialogsDone)
        {
            if(refNeeded.leftDialog.activeSelf || refNeeded.rightDialog.activeSelf)
            {
                checkTuto("tuto_dialogs");
            }

            if(player.GetComponent<Interactions>().PNJContact && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>())
            {
                if(player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().PNJName == "Clara Grey"
                && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().dialogLine != 0
                && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().dialogLine != 1)
                {
                    checkTuto("tuto_dialogs_end");
                }
            }
        }

        if(!tutoDone.newStickerDone)
        {
            if(countStickers < player.GetComponent<PlayerMemory>().allStickers.Count)
            {
                checkTuto("tuto_newSticker");
            }
        }

        if(!tutoDone.questionsDone && tutoDone.dialogsDone)
        {
            if(player.GetComponent<Interactions>().PNJContact
            && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>()
            && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().PNJName == "Clara Grey")
            {
                if(player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().dialogIndex
                == player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().transitionQuote)
                {
                    checkTuto("tuto_questions");
                }
            }

            if(refNeeded.book.activeSelf)
            {
                checkTuto("tuto_questions_2");
            }

            //Si on est dans le carnet et qu'ensuite la phrase de Clara n'est plus celle de transition
            if(currentTuto == "questions")
            {
                if(player.GetComponent<Interactions>().PNJContact
                && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().PNJName == "Clara Grey"
                && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().dialogIndex
                != player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().transitionQuote)
                {
                    closeBoxHelp();
                    tutoDone.questionsDone = true;
                }
            }
        }

        if(tutoDone.questionsDone && !tutoDone.quitDialogDone)
        {
            if(player.GetComponent<Interactions>().PNJContact
            && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().PNJName == "Clara Grey")
            {
                if(player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().dialogIndex
                == player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().transitionQuote)
                {
                    checkTuto("tuto_quitDialog");
                }
            }
            if(currentTuto == "quitDialog"
            && !refNeeded.leftDialog.activeSelf
            && !refNeeded.rightDialog.activeSelf)
            {
                checkTuto("tuto_quitDialog_end");
            }
        }

        if(tutoDone.dialogsDone && !tutoDone.moveDone)
        {
            if(!refNeeded.leftDialog.activeSelf && !refNeeded.rightDialog.activeSelf)
                checkTuto("tuto_move");
            if(posPlayer != player.transform.position && currentTuto == "move")
                StartCoroutine(WaitMove(1));
        }

        if(tutoDone.moveDone && !tutoDone.interactionDone)
        {
            if(player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().PNJName != null
            && player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().PNJName != "Clara Grey")
            {
                checkTuto("tuto_interaction");
            }
            if(refNeeded.leftDialog.activeSelf
            || refNeeded.rightDialog.activeSelf
            || refNeeded.board.activeSelf)
            {
                checkTuto("tuto_interaction_end");
            }
        }

        if(tutoDone.interactionDone && !tutoDone.openBookDone)
        {
            if(!refNeeded.leftDialog.activeSelf
            && !refNeeded.rightDialog.activeSelf)
            {
                checkTuto("tuto_openBook");
                if(refNeeded.book.activeSelf)
                {
                    checkTuto("tuto_openBook_end");
                }
            }
        }

        if(tutoDone.openBookDone && !tutoDone.navigateBookDone)
        {
            if(refNeeded.book.activeSelf)
            {
                checkTuto("tuto_navigateBook");
            }
        }

        if(tutoDone.navigateBookDone && !tutoDone.menuDone)
        {
            if(!refNeeded.book.activeSelf)
            {
                checkTuto("tuto_menu");
            }
            if(refNeeded.menu.activeSelf)
            {
                checkTuto("tuto_menu_end");
            }
        }

        if(!tutoDone.sprintDone)
        {
            if(player.GetComponent<MovementsPlayer>().canRun)
            {
                checkTuto("tuto_sprint");
            }
            if(player.GetComponent<MovementsPlayer>().sprint)
            {
                canSave = true;
                allowSave();
                StartCoroutine(WaitSprint(1));
            }
        }

        if(!tutoDone.boardDone)
        {
            if(refNeeded.board.activeSelf)
            {
                checkTuto("tuto_board");
            }
        }

        if(player.GetComponent<MovementsPlayer>().canRun && tutoDone.sprintDone)
        {
            canSave = true;
            allowSave();
        }

        if(refNeeded.newSticker.GetComponent<Animator>().speed == 0 && tutoDone.newStickerDone)
        {
            refNeeded.newSticker.GetComponent<Animator>().speed = 1;
        }

        if (refNeeded.menu.activeInHierarchy && refNeeded.newSticker.GetComponent<Animator>().speed != 0)
        {
            refNeeded.newSticker.GetComponent<Animator>().SetTrigger("AnimOff");
        }

        /*if(refNeeded.menu.activeInHierarchy
        && refNeeded.newSticker.transform.GetChild(0).GetComponent<Text>().color != new Color32(0,0,0,0))
        {
            Debug.Log("nigtfdvc");
            //refNeeded.newSticker.GetComponent<SpriteRenderer>().color = new Vector4(255,255,255,0);
            //refNeeded.newSticker.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Vector4(0,0,0,0);
            //refNeeded.newSticker.transform.GetChild(0).GetComponent<Text>().color = new Vector4(0,0,0,0);
            refNeeded.newSticker.SetActive(false);

            //refNeeded.newSticker.SetActive(true);
        }
        else
        {
            refNeeded.newSticker.SetActive(true);
            refNeeded.newSticker.GetComponent<SpriteRenderer>().color = new Vector4(255,255,255,0);
            refNeeded.newSticker.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Vector4(0,0,0,0);
        }*/

            /*if(refNeeded.menu.activeSelf
            && GameObject.Find("EtiquetteLaissezPasser").transform.GetChild(0).GetComponent<Text>().color.a != 0)
            {
                GameObject.Find("EtiquetteLaissezPasser").transform.GetChild(0).GetComponent<Text>().gameObject.SetActive(false);
                GameObject.Find("EtiquetteLaissezPasser").transform.GetChild(0).GetComponent<Text>().gameObject.SetActive(true);
            }*/
    }

    IEnumerator WaitMove(float Time){
        yield return new WaitForSeconds(Time);
        checkTuto("tuto_move_end");

    }
    IEnumerator WaitSticker(float Time){
        yield return new WaitForSeconds(Time);
        refNeeded.newSticker.GetComponent<Animator>().speed = 0;
    }
    IEnumerator WaitSprint(float Time){
        yield return new WaitForSeconds(Time);
        checkTuto("tuto_sprint_end");

    }

    public void checkTuto(string tutoEvent)
    {
        switch (tutoEvent)
        {
            //When Clara starts dialog
            case "tuto_dialogs":
                posPlayer = player.transform.position;
                if(!tutoDone.dialogsDone)
                {
                    openBoxHelp(dialogs, true);
                }
                break;
            //When pass the sentence 1
            case "tuto_dialogs_end":
                closeBoxHelp();
                tutoDone.dialogsDone = true;
                break;



            //When obtain Clara sticker
            case "tuto_newSticker":
                if(!tutoDone.newStickerDone)
                {
                    openBoxHelp(newSticker, true);
                    refNeeded.leftDialog.GetComponent<BoxCollider2D>().enabled = false;
                    refNeeded.rightDialog.GetComponent<BoxCollider2D>().enabled = false;
                    player.GetComponent<Interactions>().enabled = false;
                    if(!sticker)
                    {
                        StartCoroutine(WaitSticker(1));
                        sticker = true;
                    }
                }
                break;
            //When close box
            case "tuto_newSticker_end":
                tutoDone.newStickerDone = true;
                //player.GetComponent<PlayerMemory>().newSticker.GetComponent<Animator>().speed = 0;
                refNeeded.leftDialog.GetComponent<BoxCollider2D>().enabled = true;
                refNeeded.rightDialog.GetComponent<BoxCollider2D>().enabled = true;
                player.GetComponent<Interactions>().enabled = true;
                refNeeded.newSticker.GetComponent<Animator>().speed = 1;
                break;



            //When it's 1st transision quote
            case "tuto_questions":
                if(!tutoDone.questionsDone)
                {
                    openBoxHelp(questions, true);
                    canEsc = false;
                }
                break;
            //When open book to ask a question
            case "tuto_questions_2":
                if(!tutoDone.questionsDone)
                {
                    closeBoxHelp();
                    questions.positionBoxHelp = Tuto.PositionBoxHelp.Bottom;
                    openBoxHelp(questions, true);
                }
                break;
            //When ask a question
            case "tuto_questions_end":
                closeBoxHelp();
                tutoDone.questionsDone = true;
                break;


            
            //When its transition quote again
            case "tuto_quitDialog":
                if(!tutoDone.quitDialogDone)
                {
                    openBoxHelp(quitDialog, true);
                    canEsc = true;
                    refNeeded.bookOnDialog.GetComponent<Image>().enabled = false;
                }
                break;
            //When ESC on the 2nd quote transition
            case "tuto_quitDialog_end":
                closeBoxHelp();
                tutoDone.quitDialogDone = true;
                refNeeded.bookOnDialog.GetComponent<Image>().enabled = true;
                break;



            //When tuto quit dialog is close
            case "tuto_move":
                if(!tutoDone.moveDone)
                {
                    openBoxHelp(move, true);
                }
                break;
            //When walk after move tuto
            case "tuto_move_end":
                //closeBoxHelp();
                topBoxHelp.SetActive(false);
                tutoDone.moveDone = true;
                break;



            //When have a new PNJContact
            case "tuto_interaction":
                 openBoxHelp(interaction, true);
                 break;
            //When use E with something
            case "tuto_interaction_end":
                closeBoxHelp();
                tutoDone.interactionDone = true;
                break;



            //When reopen dialogs
            case "tuto_openBook":
                 openBoxHelp(openBook, true);
                 break;
            //When open book
            case "tuto_openBook_end":
                closeBoxHelp();
                tutoDone.openBookDone = true;
                break;



            //When open book
            case "tuto_navigateBook":
                 openBoxHelp(navigateBook, true);
                 break;
            //When close help
            case "tuto_navigateBook_end":
                tutoDone.navigateBookDone = true;
                break;



            //When close book
            case "tuto_menu":
                 openBoxHelp(menu, true);
                 break;
            //When open menu
            case "tuto_menu_end":
                closeBoxHelp();
                tutoDone.menuDone = true;
                break;


            
            //When can run
            case "tuto_sprint":
                 openBoxHelp(sprint, true);
                 break;
            //1sec after run
            case "tuto_sprint_end":
                closeBoxHelp();
                tutoDone.sprintDone = true;
                skipTuto();
                break;



            //When open board
            case "tuto_board":
                 openBoxHelp(board, true);
                 break;
            //When close tuto
            case "tuto_board_end":
                tutoDone.boardDone = true;
                break;


            
            default:
                break;
        }
    }

    public void skipTuto()
    {
        skipAll();

        canEsc = true;
        refNeeded.leftDialog.GetComponent<BoxCollider2D>().enabled = true;
        refNeeded.rightDialog.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Interactions>().enabled = true;
    }

    void skipAll()
    {
        wantSkipTuto = true;

        closeBoxHelp();

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

    void openBoxHelp(Tuto tempTuto, bool canSkipTuto)
    {
        //Trouve la box à la bonne position pour ensuite l'activer
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

        currentTuto = tempTuto.endingString;

        tempBoxHelp.SetActive(true);

        if(tempTuto.canClose)
        {
            //Active la croix pour fermer et l'image invisible de fond
            tempBoxHelp.transform.GetChild(4).gameObject.SetActive(true);
            tempBoxHelp.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            //Désactive la croix pour fermer et l'image invisible de fond
            tempBoxHelp.transform.GetChild(4).gameObject.SetActive(false);
            tempBoxHelp.transform.GetChild(0).gameObject.SetActive(false);
        }

        if(currentTuto == "quitDialog") tempBoxHelp.transform.GetChild(0).gameObject.SetActive(true);

        isInHelp = true;
        
        //Insère les bons textes
        tempBoxHelp.transform.GetChild(1).GetComponent<Text>().text = "- " + LanguageManager.Instance.GetDialog(tempTuto.keyTitle) + " -";
        tempBoxHelp.transform.GetChild(2).GetComponent<Text>().text = LanguageManager.Instance.GetDialog(tempTuto.keyText);
        tempBoxHelp.transform.GetChild(3).GetComponent<Text>().text = LanguageManager.Instance.GetDialog(textSkipTuto);

        //Check si on peut passer le tuto, et affiche ou non le bouton en conséquences
        if(canSkipTuto) tempBoxHelp.transform.GetChild(3).gameObject.SetActive(true);
        else tempBoxHelp.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void closeBoxHelp()
    {
        topBoxHelp.SetActive(false);
        midBoxHelp.SetActive(false);
        bottomBoxHelp.SetActive(false);

        isInHelp = false;
        checkTuto(currentTuto);
        currentTuto = null;
    }

    public void openBoardHelp()
    {
        openBoxHelp(board, false);
    }

    void preventSave()
    {
        foreach(GameObject objet in refNeeded.saveButton)
        {
            objet.GetComponent<Button>().interactable = false;
            objet.transform.GetChild(1).gameObject.SetActive(true);
            objet.GetComponent<Image>().color = new Color32(156,156,156,255);
            objet.transform.GetChild(0).GetComponent<Text>().color = new Color32(169,169,169,255);
            objet.transform.GetChild(1).GetComponent<Text>().color = new Color32(169,169,169,255);
        }
    }
    public void allowSave()
    {
        foreach(GameObject objet in refNeeded.saveButton)
        {
            objet.GetComponent<Button>().interactable = true;
            objet.transform.GetChild(1).gameObject.SetActive(false);
            objet.GetComponent<Image>().color = new Color32(255,255,255,255);
            objet.transform.GetChild(0).GetComponent<Text>().color = new Color32(255,255,255,255);
            objet.transform.GetChild(1).GetComponent<Text>().color = new Color32(255,255,255,255);
        }
        skipAll();
    }
}
