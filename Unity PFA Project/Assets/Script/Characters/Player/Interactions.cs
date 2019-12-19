using System.Collections;
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
    public Transform carnet;
    Transform carnetUI;
    #endregion

    #region Other Informations
    [Header("Other Settings")]
    public bool canOpenCarnet = true;
    public List<string> PnjMet;
    public bool isInCinematic;
    public GameObject boardCanvas;
    public GameObject dialAndBookCanvas;
    public bool isOnTooltip;
    #endregion

    #region State Informations
    public enum State {Normal, InDialog, OnBoard, OnCarnet, InCinematic, Pause};
    [SerializeField]
    public State state;
    #endregion

    void Awake()
    {
        carnetUI = GameObject.Find("CarnetUI").transform;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager");
        rb2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(state == State.Normal || state != State.InCinematic)
        {
            if (collision.transform.tag == "PNJinteractable" || collision.transform.tag == "Item" || collision.transform.tag == "Board" || collision.transform.tag == "Interaction" || collision.transform.tag == "Shortcut")
            {
                PNJContact = collision.gameObject;
                if (collision.transform.childCount > 0)
                {
                    collision.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.transform.tag == "PNJinteractable" || collision.transform.tag == "Item" || collision.transform.tag == "Board" || collision.transform.tag == "Interaction" || collision.transform.tag == "Shortcut")
        {
            PNJContact = null;
            if (collision.transform.childCount > 0)
            {
                collision.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
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
            OpenPhone();
            OpenPauseMenu();
            break;

            case State.InDialog:
            ChangeLineOfDialog();
            QuitDialog();
            OpenBookDuringDialog();
            break;

            case State.OnBoard:
            break;

            case State.OnCarnet:
            CloseBook();
            break;

            case State.InCinematic:
            ChangeLineOfDialog();
            break;

            case State.Pause :
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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetBool("Walk", false);
    }

    void OpenBookDuringDialog()
    {
        if(Input.GetButtonDown("MenuSpecial") && carnetUI.GetComponent<Animator>().GetBool("InDialog"))
        {
            if(!GameObject.Find("Carnet"))
            {
                OpenBookExe();
            }
            else
            {
                CloseBook();
            }
        }
    }

    void OpenBook()
    {
        if(Input.GetButtonDown("MenuSpecial"))
        {
            if(!GameObject.Find("Carnet"))
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
        dialAndBookCanvas.transform.GetChild(5).gameObject.SetActive(true);
        carnetUI.GetComponent<Animator>().SetBool("ClickOn", true);
        GetComponent<PlayerMemory>().CheckStickersCarnet();
        ChangeState(State.OnCarnet);
    }
    void QuitDialog()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(PNJContact.GetComponent<PNJ>().dialogIndex > 0 && PNJContact.GetComponent<PNJ>().allDialogs.listOfDialogs[PNJContact.GetComponent<PNJ>().dialogIndex].canAskQuestions)
            {
                PNJContact.GetComponent<PNJ>().EndDialog();
                carnetUI.GetComponent<Animator>().SetBool("ClickOn", false);
                //animator.SetBool("Talk", false);
                ChangeState(State.Normal);
            }
        }
    }
    void ChangeLineOfDialog()
    {
        if(Input.GetButtonDown("Interaction") && !dialAndBookCanvas.transform.GetChild(6).gameObject.activeInHierarchy)
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
        if(Input.GetButtonDown("Interaction") && PNJContact!= null  && PNJContact.tag == "Shortcut")
        {
            PNJContact.GetComponent<Shortcut>().Teleport();
        }
    }
    void OpenDialog()
    {
        if(Input.GetButtonDown("Interaction") && PNJContact!= null && !dialAndBookCanvas.transform.GetChild(6).gameObject.activeInHierarchy)
        {
            if(PNJContact.tag == "PNJinteractable" || PNJContact.tag == "Item")
            {
                //GetComponent<Animator>().SetBool("Walk", false);
                //dialAndBookCanvas.transform.GetChild(2).GetComponent<Animator>().SetBool("InDialog", true);
                //animator.SetBool("Talk", true);
                ChangeState(State.InDialog);
                StartDialog();
            }
        }
    }
    void OpenBoard()
    {
        if(Input.GetButtonDown("Interaction") && PNJContact != null && PNJContact.tag == "Board")
        {
            dialAndBookCanvas.SetActive(false);
            boardCanvas.SetActive(true);
            Camera.main.GetComponent<Camera_Manager>().OnBoard();
            GetComponent<PlayerMemory>().CheckStickersBoard();
            ChangeState(State.OnBoard);
            Camera.main.GetComponent<CameraFollow>().actualRoom.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            //gameObject.SetActive(false);
        }
    }

    void CloseBook()
    {
        if(Input.GetButtonDown("Cancel") || Input.GetButtonDown("MenuSpecial"))
        {
            CloseBookExe();
        }
    }
    public void CloseBookExe()
    {
        //Camera.main.GetComponent<Camera_Manager>().NotOnCarnet();
        dialAndBookCanvas.transform.GetChild(5).gameObject.SetActive(false);
        //dialAndBookCanvas.transform.GetChild(dialAndBookCanvas.transform.childCount - 1).gameObject.SetActive(false);
        carnetUI.GetComponent<Animator>().SetBool("ClickOn", false);
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
        //Debug.Log(newState);
        switch(newState)
        {
            case State.InCinematic:
            carnetUI.GetComponent<Animator>().SetBool("ClickOn", true);
            carnetUI.GetComponent<Animator>().SetBool("InDialog", false);
            DisableMovements();
            state = State.InCinematic;
            break;

            case State.Normal:
            onBook = false;
            isInDialog = false;
            animator.SetBool("Talk", false);
            carnetUI.GetComponent<Animator>().SetBool("ClickOn", false);
            carnetUI.GetComponent<Animator>().SetBool("InDialog", false);
            EnableMovements();
            state = State.Normal;
            break;

            case State.InDialog:
            DisableMovements();
            animator.SetBool("Talk", true);
            isInDialog = true;
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

            case State.Pause :
            GameObject.Find("Necessary_Floating_Canvas").transform.GetChild(2).gameObject.SetActive(true);
            carnetUI.GetComponent<Animator>().SetBool("ClickOn", true);
            carnetUI.GetComponent<Animator>().SetBool("InDialog", false);
            DisableMovements();
            state = State.Pause;
            break;
        }
    }

    public void QuitCinematicMode()
    {
        if(isInDialog)
        {
            ChangeState(State.InDialog);
            //state = State.InDialog;
        }
        else 
        {
            PNJContact = null;
            GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", false);
            ChangeState(State.Normal);
        }
    }

    void OpenPhone()
    {
        if(Input.GetButtonDown("Interaction") && PNJContact != null && PNJContact.name == "Phone")
        {
            ChangeState(State.InDialog);
            StartDialog();
        }
    }

    public void EnterDialogMode()
    {
        ChangeState(State.InDialog);
        //state = State.InDialog;
        GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", true);
    }
    public void StartDialog()
    {
        //animator.SetBool("Talk", true);
        //animator.SetBool("Walk", false);
        //CloseCarnet();
//        int redirectionEventListCount = PNJContact.GetComponent<PNJ>().eventRedirection.redirectionEventList.Count;
        int eventGivenListCount = PNJContact.GetComponent<PNJ>().eventRedirection.eventGivenList.Count;
        /*if(PNJContact.GetComponent<PNJTest>().allDialogs[PNJContact.GetComponent<PNJTest>().dialogIndex].canAskQuestions)
        {
            carnet.GetComponent<Animator>().SetBool("InDialog", true);
        }
        else
        {
            carnet.GetComponent<Animator>().SetBool("InDialog", false);
            carnet.GetComponent<Animator>().SetBool("ClickOn", true);
        }
        if(redirectionEventListCount > 0 && eventGivenListCount > 0)
        {
            PNJContact.GetComponent<PNJ>().ResponseEvent();
        }*/

        if(((eventGivenListCount == 0) || (!PNJContact.GetComponent<PNJ>().haveEvent)))
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
        //PNJContact.transform.GetChild(0).gameObject.SetActive(false);
        //PNJContact.GetComponent<PNJ>().Startdialogue();
    }

    public void EndDialog()
    {
        if(state != State.InCinematic)
        {
            GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", false);
            carnetUI.GetComponent<Animator>().SetBool("InDialog", false);
            //ChangeState(State.Normal);
            StartCoroutine("TimerQuitDialog");
        }
        else 
        {
            carnetUI.GetComponent<Animator>().SetBool("InDialog", false);
            carnetUI.GetComponent<Animator>().SetBool("ClickOn", true);            
            PNJContact.GetComponent<Clara_Cinematic>().CheckIndex();
            PNJContact = null;
        }
        if(PNJContact != null && PNJContact.tag == "Item")
        {
            PNJContact.SetActive(false);
            PNJContact = null;
        }
    }

    void OpenPauseMenu()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            ChangeState(State.Pause);
        }
    }

    IEnumerator TimerQuitDialog()
    {
        state = State.Pause;
        yield return new WaitForSecondsRealtime(0.1f);
        ChangeState(State.Normal);
    }
}