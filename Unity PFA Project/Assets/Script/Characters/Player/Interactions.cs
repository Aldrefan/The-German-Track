﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{

    #region Movements Informations
    [Header("Movements Settings")]
    public bool canRun;
    Rigidbody2D rb2d;
    Animator animator;
    #endregion

    #region Contact Informations
    [Header("Contact Settings")]
    public GameObject PNJContact;
    public bool isInDialog;
    GameObject dialogueManager;
    bool boardIsNear;
    bool onBook = false;
    Transform carnet;
    #endregion

    #region Other Informations
    [Header("Other Settings")]
    public bool canOpenCarnet = true;
    public List<string> PnjMet;
    public bool isInCinematic;
    public GameObject boardCanvas;
    public GameObject dialAndBookCanvas;
    #endregion

    #region State Informations
    public enum State {Normal, InDialog, OnBoard, OnCarnet, InCinematic};
    [SerializeField]
    public static State state;
    #endregion

    void Awake()
    {
        carnet = GameObject.Find("CarnetUI").transform;
    }

    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager");
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "PNJinteractable" || collision.transform.tag == "Item" || collision.transform.tag == "Board" || collision.transform.tag == "Interaction" || collision.transform.tag == "Shortcut")
        {
            PNJContact = collision.gameObject;
            collision.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.transform.tag == "PNJinteractable" || collision.transform.tag == "Item" || collision.transform.tag == "Board" || collision.transform.tag == "Interaction" || collision.transform.tag == "Shortcut")
        {
            PNJContact = null;
            collision.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void Update() // Rien
    {
        /*if(Input.GetButtonDown("Interaction"))
        {
            if(PNJContact != null)
            {
                if(!isInDialog)
                {
                    StartDialog();
                }
                else 
                {
                    if(PNJContact.GetComponent<PNJ>().quoteFinished)
                    {
                        PNJContact.GetComponent<PNJ>().Startdialogue();
                    }
                    else 
                    {
                        GameObject activePanel = GameObject.FindObjectOfType<DialogInterface>().gameObject;
                        PNJContact.GetComponent<PNJ>().FullQuote(activePanel);
                    }
                }
            }
            else if(boardIsNear)
            {
                if(!onBoard)
                {
                    if(isInDialog)
                    {
                        EndDialog();
                    }
                    Camera.main.GetComponent<Camera_Manager>().OnBoard();
                    GetComponent<PlayerMemory>().CheckStickersBoard();
                    onBoard = true;
                }
            }
        }

        if(Input.GetButtonDown("Cancel"))
        {
            if(isInDialog && PNJContact.GetComponent<PNJ>().allDialogs.listOfDialogs[PNJContact.GetComponent<PNJ>().dialogIndex].canAskQuestions)
            {
                Camera.main.GetComponent<Camera_Manager>().NotOnCarnet();
                onBook = false;
                PNJContact.GetComponent<PNJ>().EndDialog();
            }
            else OpenOrCloseCarnet();
        }*/
    }


    void FixedUpdate()
    {
        switch(state)
        {
            case State.Normal:
            OpenBook();
            TeleportPossibility();
            OpenDialog();
            OpenBoard();
            break;

            case State.InDialog:
            QuitDialog();
            ChangeLineOfDialog();
            break;

            case State.OnBoard:
            break;

            case State.OnCarnet:
            CloseBook();
            break;

            case State.InCinematic:
            ChangeLineOfDialog();
            break;
        }
    }

    void EnableMovements()
    {
        GetComponent<MovementsPlayer>().enabled = true;
    }

    void DisableMovements()
    {
        GetComponent<MovementsPlayer>().enabled = false;
    }

    void OpenBook()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(!onBook)
            {
                OpenBookExe();
            }
            else
            {
                CloseBook();
            }
        }
    }
    public void OpenBookExe()
    {
        //Camera.main.GetComponent<Camera_Manager>().OnCarnet();
        dialAndBookCanvas.transform.GetChild(dialAndBookCanvas.transform.childCount - 1).gameObject.SetActive(true);
        dialAndBookCanvas.transform.GetChild(2).GetComponent<Animator>().SetBool("ClickOn", true);
        GetComponent<PlayerMemory>().CheckStickersCarnet();
        ChangeState(State.OnCarnet);
    }
    void QuitDialog()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(PNJContact.GetComponent<PNJ>().dialogIndex > 0 || PNJContact.GetComponent<PNJ>().allDialogs.listOfDialogs[PNJContact.GetComponent<PNJ>().dialogIndex].canAskQuestions)
            {
                PNJContact.GetComponent<PNJ>().EndDialog();
                dialAndBookCanvas.transform.GetChild(2).GetComponent<Animator>().SetBool("InDialog", false);
                ChangeState(State.Normal);
            }
        }
    }
    void ChangeLineOfDialog()
    {
        if(Input.GetButtonDown("Interaction"))
        {
            if(PNJContact.GetComponent<PNJ>().quoteFinished)
            {
                PNJContact.GetComponent<PNJ>().Startdialogue();
            }
            else 
            {
                GameObject activePanel = GameObject.FindObjectOfType<DialogInterface>().gameObject;
                PNJContact.GetComponent<PNJ>().FullQuote(activePanel);
            }
        }
    }

    void TeleportPossibility()
    {
        if(Input.GetButtonDown("Interaction") && PNJContact.tag == "Shortcut")
        {
            PNJContact.GetComponent<Shortcut>().Teleport();
        }
    }
    void OpenDialog()
    {
        if(Input.GetButtonDown("Interaction") && PNJContact.tag == "PNJinteractable")
        {
            GetComponent<Animator>().SetBool("Walk", false);
            dialAndBookCanvas.transform.GetChild(2).GetComponent<Animator>().SetBool("InDialog", true);
            ChangeState(State.InDialog);
            StartDialog();
        }
    }
    void OpenBoard()
    {
        if(Input.GetButtonDown("Interaction") && PNJContact.tag == "Board")
        {
            dialAndBookCanvas.SetActive(false);
            boardCanvas.SetActive(true);
            Camera.main.GetComponent<Camera_Manager>().OnBoard();
            GetComponent<PlayerMemory>().CheckStickersBoard();
            ChangeState(State.OnBoard);
        }
    }

    void CloseBook()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            CloseBookExe();
        }
    }
    public void CloseBookExe()
    {
        //Camera.main.GetComponent<Camera_Manager>().NotOnCarnet();
        dialAndBookCanvas.transform.GetChild(dialAndBookCanvas.transform.childCount - 1).gameObject.SetActive(false);
        dialAndBookCanvas.transform.GetChild(2).GetComponent<Animator>().SetBool("ClickOn", false);
        if(isInDialog)
        {
            state = State.InDialog;
        }
        else 
        {
            ChangeState(State.Normal);
        }
    }

    public void CloseBoard()
    {
        dialAndBookCanvas.SetActive(true);
        boardCanvas.SetActive(false);
        ChangeState(State.Normal);
    }

    public void ChangeState(State newState)
    {
        switch(newState)
        {
            case State.InCinematic:
            DisableMovements();
            state = State.InCinematic;
            break;

            case State.Normal:
            onBook = false;
            isInDialog = false;
            EnableMovements();
            state = State.Normal;
            break;

            case State.InDialog:
            DisableMovements();
            state = State.InDialog;
            break;

            case State.OnBoard:
            DisableMovements();
            state = State.OnBoard;
            break;

            case State.OnCarnet:
            onBook = true;
            DisableMovements();
            state = State.OnCarnet;
            break;
        }
    }

    public void QuitCinematicMode()
    {
        if(isInDialog)
        {
            state = State.InDialog;
        }
        else 
        {
            EnableMovements();
            PNJContact = null;
            GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", false);
            state = State.Normal;
        }
    }

    public void EnterDialogMode()
    {
        state = State.InDialog;
        
            GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", true);
    }
    public void StartDialog()
    {
        //GetComponent<Animator>().SetBool("Talk", true);
        //GetComponent<Animator>().SetBool("Walk", false);
        //CloseCarnet();
        int redirectionEventListCount = PNJContact.GetComponent<PNJ>().eventRedirection.redirectionEventList.Count;
        int eventGivenListCount = PNJContact.GetComponent<PNJ>().eventRedirection.eventGivenList.Count;

        if(redirectionEventListCount > 0 && eventGivenListCount > 0)
        {
            PNJContact.GetComponent<PNJ>().ResponseEvent();
        }

        if((redirectionEventListCount == 0) || (eventGivenListCount == 0) || (!PNJContact.GetComponent<PNJ>().haveEvent))
        {
            if(PnjMet.Contains(PNJContact.name))
            {
                PNJContact.GetComponent<PNJ>().ChangeDialog(1);
            }
            else 
            {
                PnjMet.Add(PNJContact.name);
                PNJContact.GetComponent<PNJ>().ChangeDialog(0);
            }
        }
        
        //GetComponent<MovementsPlayer>().enabled = false;// Initial
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        //isInDialog = true;// Initial
        PNJContact.transform.GetChild(0).gameObject.SetActive(false);
        //PNJContact.GetComponent<PNJ>().Startdialogue();
    }

    public void EndDialog()
    {
        GetComponent<Animator>().SetBool("Talk", false);
        if (PNJContact == GameObject.Find("dialog_williamscott"))
        {
            GetComponent<EventsCheck>().eventsList.Add("endDialogWilliamScott");
            GetComponent<EventsCheck>().CheckEvents("endDialogWilliamScott");
            GameObject.Find("scott_nelson").GetComponent<Clara_Cinematic>().ExecuteCommand();
        }
        if (PNJContact == GameObject.Find("marvin_meyer_phone"))
        {
            GetComponent<EventsCheck>().eventsList.Remove("numberMarvinMeyer");
            PNJContact = GameObject.Find("empty_pibphone");
        }
        if (PNJContact == GameObject.Find("clara_grey_phone"))
        {
            GetComponent<EventsCheck>().eventsList.Remove("numberClaraGrey");
            PNJContact = GameObject.Find("empty_kennethphone");
        }
        if(GetComponent<EventsCheck>().fauteuil)
        {
            GetComponent<EventsCheck>().StartCoroutine("Fade");
            GameObject.Find("FadePanel").GetComponent<Animator>().SetTrigger("FadeIn");
            GameObject.Find("Fauteuil").SetActive(false);
            GetComponent<EventsCheck>().clara.SetActive(false);
            GameObject.Find("E_InvisibleWall").SetActive(false);
            GetComponent<EventsCheck>().fauteuil = false;
        }
        
        GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", false);

        //GetComponent<MovementsPlayer>().enabled = true;
        //carnet.GetComponent<Animator>().SetBool("InDialog", false);
        /*if(PNJContact.name == "Clara")
        {
            isInCinematic = false;
        }*/
        if(PNJContact.tag == "Item")
        {
            PNJContact.SetActive(false);
            PNJContact = null;
        }
        isInDialog = false;
        carnet.GetComponent<Animator>().SetBool("InDialog", false);
        carnet.GetComponent<Animator>().SetBool("ClickOn", false);
        //PNJContact = null;
        //canOpenCarnet = true;
        //state = State.Normal;
    }

    /*public void OpenOrCloseCarnet()
    {
        if(canOpenCarnet)
        {
            if(!onBoard)
            {
                if(!onBook)
                {
                    Camera.main.GetComponent<Camera_Manager>().OnCarnet();
                    GetComponent<PlayerMemory>().CheckStickersCarnet();
                    onBook = true;
                }
                else
                {
                    CloseCarnet();
                }
            }
        }
    }*/

    public void CloseCarnet()
    {
        Camera.main.GetComponent<Camera_Manager>().NotOnCarnet();
        onBook = false;
    }
}