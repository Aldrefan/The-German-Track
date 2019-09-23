using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{

    #region Movements Informations
    [Header("Movements Settings")]
    public float walk_speed;
    public float run_speed;
    bool facingRight = true;
    bool sprint = false;
    float speed;
    public bool canRun;
    Rigidbody2D rb2d;
    Animator animator;
    #endregion

    #region Contact Informations
    [Header("Contact Settings")]
    public GameObject PNJContact;
    public bool isInDialog;
    public bool onBoard;
    public bool isInContact;
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
    #endregion

    #region State Informations
    enum State {Normal, InDialog, OnBoard, OnCarnet, InCinematic};
    [SerializeField]
    State state;
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

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.tag == "PNJinteractable")
        {
            PNJContact = collision.gameObject;
            collision.transform.GetChild(0).gameObject.SetActive(true);
            isInContact = true;
        }
        if(collision.transform.tag == "Board")
        {
            boardIsNear = true;
            collision.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (collision.transform.tag == "Item")
        {
            PNJContact = collision.gameObject;
            collision.transform.GetChild(0).gameObject.SetActive(true);
            isInContact = true;
        }
    }*/

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

    void Movements()
    {
        if(!GetComponent<MovementsPlayer>().enabled)
        {
            GetComponent<MovementsPlayer>().enabled = true;
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
        Camera.main.GetComponent<Camera_Manager>().OnCarnet();
        GetComponent<PlayerMemory>().CheckStickersCarnet();
        DisableMovements();
        onBook = true;
        state = State.OnCarnet;
    }
    void QuitDialog()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(PNJContact.GetComponent<PNJ>().dialogIndex > 0 || PNJContact.GetComponent<PNJ>().allDialogs.listOfDialogs[PNJContact.GetComponent<PNJ>().dialogIndex].canAskQuestions && state != State.InCinematic)
            {
                PNJContact.GetComponent<PNJ>().EndDialog();
                state = State.Normal;
                EnableMovements();
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
            DisableMovements();
            state = State.InDialog;
            isInDialog = true;
            StartDialog();
        }
    }
    void OpenBoard()
    {
        if(Input.GetButtonDown("Interaction") && PNJContact.tag == "Board")
        {
            Camera.main.GetComponent<Camera_Manager>().OnBoard();
            GetComponent<PlayerMemory>().CheckStickersBoard();
            DisableMovements();
            onBoard = true;
            state = State.OnBoard;
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
        Camera.main.GetComponent<Camera_Manager>().NotOnCarnet();
        if(isInDialog)
        {
            state = State.InDialog;
        }
        else 
        {
            state = State.Normal;
            onBook = false;
            EnableMovements();
        }
    }


    void BoardMovements()
    {

    }

    public void CloseBoard()
    {
        state = State.Normal;
        EnableMovements();
    }

    public void EnterCinematicMode()
    {
        state = State.InCinematic;
    }
    public void QuitCinematicMode()
    {
        state = State.Normal;
    }

    public void EnterDialogMode()
    {
        state = State.InDialog;
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

        GetComponent<MovementsPlayer>().enabled = true;
        carnet.GetComponent<Animator>().SetBool("InDialog", false);
        if(PNJContact.name == "Clara")
        {
            isInCinematic = false;
        }
        if(PNJContact.tag == "Item")
        {
            PNJContact.SetActive(false);
            isInContact = false;
            PNJContact = null;
        }
        /*else if(!isInContact)// Initial
        {
            PNJContact = null;
        }*/
        isInDialog = false;
        PNJContact = null;
        isInContact = false;
        GetComponent<Interactions>().canOpenCarnet = true;
        carnet.GetComponent<Animator>().SetBool("InDialog", false);
        carnet.GetComponent<Animator>().SetBool("ClickOn", false);
        state = State.Normal;
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