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
    GameObject oldPNJContact;
    public bool isInDialog;
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
    [SerializeField]
    string boardCanvasName;
    GameObject boardCanvas;

    [SerializeField]
    string dialogCanvasName;
    GameObject dialAndBookCanvas;
    public bool isOnTooltip;
    bool talk;
    #endregion

    #region State Informations
    public enum State {Normal, InDialog, OnBoard, OnCarnet, InCinematic, Pause, Inactive};
    [SerializeField]
    public State state;
    #endregion

    float posY;

    void Awake()
    {
        if (GameObject.Find("CarnetUI") != null)
        {
            carnetUI = GameObject.Find("CarnetUI").transform;
        }
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (CanvasManager.CManager != null)
        {

            dialAndBookCanvas = CanvasManager.CManager.GetCanvas(dialogCanvasName);
            boardCanvas = CanvasManager.CManager.GetCanvas(boardCanvasName);
        }
        //animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (Camera.main.GetComponent<CameraFollow>().actualRoom != null)
        {
            if (Camera.main.GetComponent<CameraFollow>().actualRoom.GetComponent<SceneInformations>() != null)
            {
                GetComponent<MovementsPlayer>().canRun = Camera.main.GetComponent<CameraFollow>().actualRoom.GetComponent<SceneInformations>().canRun;

            }else if (Camera.main.GetComponent<CameraFollow>().actualRoom.GetComponent<RoomInformations>() != null)
            {
                GetComponent<MovementsPlayer>().canRun = Camera.main.GetComponent<CameraFollow>().actualRoom.GetComponent<RoomInformations>().canRun;
            }

        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Normal)
        {
            if (collision.transform.tag == "PNJinteractable" || collision.transform.tag == "Item" || collision.transform.tag == "Board" || collision.transform.tag == "Interaction" || collision.transform.tag == "Shortcut")
            {
                if (collision.transform.childCount > 0)
                {
                    collision.transform.GetChild(0).gameObject.SetActive(true);

                    //float posY;
                    if(collision.transform.tag == "Shortcut")
                    {
                        /*collision.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
                        float boxYshortcut = collision.GetComponent<BoxCollider2D>().size.y;
                        float scaleYshortcut = collision.transform.localScale.y;
                        float scaleYgameobject = collision.transform.GetChild(0).localScale.y;
                        collision.transform.localScale = new Vector3 (2, 2, 2);
                        collision.transform.GetChild(0).GetChild(0).localScale = new Vector3(1.6f, 1.6f, 1.6f);
                        posY = 2.5f;*/
                    }
                    else if (collision.name == "doorKurt")
                        posY = 3.6f;
                    else
                    {
                        float spriteY = collision.GetComponent<SpriteRenderer>().sprite.rect.height;
                        float scaleY = collision.transform.localScale.y;
                        posY = ((spriteY / scaleY) * 0.05f) + 0.1f;
                    }

                    GameObject interactionE;
                    if(collision.transform.GetChild(0).childCount > 0)
                        interactionE = collision.transform.GetChild(0).GetChild(0).gameObject;
                    else
                        interactionE = collision.transform.GetChild(0).gameObject;

                    if(collision.transform.tag == "Shortcut")
                        {/*do nothing*/}
                    else
                        interactionE.transform.localPosition = new Vector3(0, posY, 0);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(state == State.Normal && state != State.InCinematic && state != State.InDialog)
        {
            if (collision.transform.tag == "PNJinteractable" || collision.transform.tag == "Item" || collision.transform.tag == "Board" || collision.transform.tag == "Interaction" || collision.transform.tag == "Shortcut")
            {
                //Debug.Log("pue la merde " + PNJContact);
                PNJContact = collision.gameObject;
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

    void Update()
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

            case State.Pause:
            QuitPauseMenu();
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
        CanvasManager.CManager.GetCanvas("CarnetPanel").gameObject.SetActive(true);
        carnetUI.GetComponent<Animator>().SetBool("ClickOn", true);
        GetComponent<PlayerMemory>().CheckStickersCarnet();
        ChangeState(State.OnCarnet);
    }
    public void QuitDialog()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(GameObject.Find("Tutorial").GetComponent<TutoKenneth>().canEsc && PNJContact.GetComponent<PNJ>().allDialogs.listOfDialogs[PNJContact.GetComponent<PNJ>().dialogIndex].canAskQuestions)
            {
                PNJContact.GetComponent<PNJ>().EndDialog();
                //Debug.Log("QuitDialog");
                carnetUI.GetComponent<Animator>().SetBool("ClickOn", false);
                dialAndBookCanvas.transform.GetChild(3).gameObject.SetActive(false);
                //animator.SetBool("Talk", false);
                ChangeState(State.Normal);
            }
        }
    }
    void ChangeLineOfDialog()
    {
        if(Input.GetButtonDown("Interaction") && !CanvasManager.CManager.GetCanvas("CarnetPanel").activeInHierarchy)
        {
            if(dialAndBookCanvas.GetComponent<Ken_Canvas_Infos>().leftPanel.activeInHierarchy || dialAndBookCanvas.GetComponent<Ken_Canvas_Infos>().rightPanel.activeInHierarchy)
            {
                if(PNJContact.GetComponent<PNJ>().quoteFinished)
                {
                    PNJContact.GetComponent<PNJ>().dialogLine++;
                    PNJContact.GetComponent<PNJ>().Startdialogue();
                }
                else
                {
                    GameObject activePanel = GameObject.FindObjectOfType<DialogInterface>().gameObject;
                    PNJContact.GetComponent<PNJ>().FullQuote(activePanel);
                }
            }
        }
    }

    void TeleportPossibility()
    {

        if(Input.GetButtonDown("Interaction") && PNJContact!= null  && PNJContact.tag == "Shortcut")
        {
            if (PNJContact.GetComponent<Shortcut>() != null)
            {
                PNJContact.GetComponent<Shortcut>().Teleport();
            }
            else if(PNJContact.transform.parent.parent.GetComponent<RoomInformations>()!=null)
            {
                PNJContact.transform.parent.parent.GetComponent<RoomInformations>().Teleport(PNJContact.name);
            }
        }
    }
    void OpenDialog()
    {
        if (dialAndBookCanvas != null)
        {
            if (Input.GetButtonDown("Interaction") && PNJContact != null && !CanvasManager.CManager.GetCanvas("CarnetPanel").gameObject.activeInHierarchy)
            {
                if (PNJContact.tag == "PNJinteractable" || PNJContact.tag == "Item" || PNJContact.tag == "Interaction")
                {
                    ChangeState(State.InDialog);
                    if (PNJContact.GetComponent<OutlineSystem>())
                    { PNJContact.GetComponent<OutlineSystem>().HideOutline(); }
                    StartDialog();
                }
            }
        }
    }

    public void CheckOpenDialog()
    {
        if(state == State.Normal)
        {
            if (PNJContact.tag == "PNJinteractable" || PNJContact.tag == "Item" || PNJContact.tag == "Interaction")
                {
                    ChangeState(State.InDialog);
                    if (PNJContact.GetComponent<OutlineSystem>())
                    { PNJContact.GetComponent<OutlineSystem>().HideOutline(); }
                    StartDialog();
                }
        }
    }

    void OpenBoard()
    {
        if(Input.GetButtonDown("Interaction") && PNJContact != null && PNJContact.tag == "Board")
        {
            OpenBoardExe();
        }
    }

    public void OpenBoardExe()
    {
        dialAndBookCanvas.SetActive(false);
        boardCanvas.SetActive(true);
        CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<String_Manager>().SavePositions();
        Camera.main.GetComponent<Camera_Manager>().OnBoard();
        GetComponent<PlayerMemory>().CheckStickersBoard();
        ChangeState(State.OnBoard);
        Camera.main.GetComponent<CameraFollow>().actualRoom.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        GameObject.FindObjectOfType<SaveFile>().ReturnStickersOnBoard(GameSaveSystem.LoadGameData(), CanvasManager.CManager.GetCanvas("Board_FIX").transform);
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
        CanvasManager.CManager.GetCanvas("CarnetPanel").gameObject.SetActive(false);
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
        StartCoroutine(TimerQuitDialog());
        //ChangeState(State.Normal);
    }

    public void ChangeState(State newState)
    {
        if(newState != State.Normal)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
        //Debug.Log(newState);
        switch(newState)
        {
            case State.InCinematic:
            isInCinematic = false;
            CanvasManager.CManager.GetCanvas("CarnetPanel").gameObject.SetActive(false);
            carnetUI.GetComponent<Animator>().SetBool("ClickOn", true);
            carnetUI.GetComponent<Animator>().SetBool("InDialog", false);
            //dialAndBookCanvas.GetComponent<Ken_Canvas_Infos>().carnet.transform.parent.gameObject.SetActive(false);
            state = State.InCinematic;
            break;

            case State.Normal:
            onBook = false;
            isInDialog = false;
            animator.SetBool("Talk", false);
            carnetUI.GetComponent<Animator>().SetBool("ClickOn", false);
            carnetUI.GetComponent<Animator>().SetBool("InDialog", false);
            state = State.Normal;
            break;

            case State.InDialog:
            animator.SetBool("Talk", true);
            isInDialog = true;
            state = State.InDialog;
            break;

            case State.OnBoard:
            state = State.OnBoard;
            break;

            case State.OnCarnet:
            onBook = true;
            state = State.OnCarnet;
            break;

            case State.Pause :
            CanvasManager.CManager.GetCanvas("Pause").SetActive(true);
            carnetUI.GetComponent<Animator>().SetBool("ClickOn", true);
            carnetUI.GetComponent<Animator>().SetBool("InDialog", false);
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
            AudioManager.Instance.PlaySoundTest("Téléphone 1");
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
        //int redirectionEventListCount = PNJContact.GetComponent<PNJ>().eventRedirection.redirectionEventList.Count;
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

        talk = false;

        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        if (eventGivenListCount == 0 && !PNJContact.GetComponent<PNJ>().haveEvent)
        {
            if (PNJContact.tag != "Interaction")
            {
                if (PnjMet.Contains(PNJContact.name))
                {
                    PNJContact.GetComponent<PNJ>().ChangeDialog(1);
                }
                else
                {
                    PnjMet.Add(PNJContact.name);
                    PNJContact.GetComponent<PNJ>().ChangeDialog(0);
                }
            }
            else PNJContact.GetComponent<PNJ>().ChangeDialog(0);
            talk = true;
        }
        else if (eventGivenListCount > 0 && PNJContact.GetComponent<PNJ>().haveEvent)
        {
            if (PNJContact.tag != "Interaction")
            {
                for (int i = 0; i < eventGivenListCount; i++)
                {
                    for (int n = 0; n < GetComponent<EventsCheck>().eventsList.Count; n++)
                    {
                        if (PNJContact.GetComponent<PNJ>().eventRedirection.eventGivenList[i] == GetComponent<EventsCheck>().eventsList[n])
                        {
                            PNJContact.GetComponent<PNJ>().ChangeDialog(PNJContact.GetComponent<PNJ>().eventRedirection.redirectionEventList[i]);
                            talk = true;
                            //return;
                        }
                    }
                }
            }
        }
        else if (eventGivenListCount == 0 && PNJContact.GetComponent<PNJ>().haveEvent)
        {
            if (PNJContact.tag != "Interaction")
            {
                for (int i = 0; i < eventGivenListCount; i++)
                {
                    for (int n = 0; n < GetComponent<EventsCheck>().eventsList.Count; n++)
                    {
                        if (PNJContact.GetComponent<PNJ>().eventRedirection.eventGivenList[i] == GetComponent<EventsCheck>().eventsList[n])
                        {
                            PNJContact.GetComponent<PNJ>().ChangeDialog(PNJContact.GetComponent<PNJ>().eventRedirection.redirectionEventList[i]);
                            talk = true;
                            //return;
                        }
                    }
                }
            }
        }

        if (!talk)
        {
            if(PNJContact.tag != "Interaction")
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
            else PNJContact.GetComponent<PNJ>().ChangeDialog(0);
        }
        
        //GetComponent<MovementsPlayer>().enabled = false;// Initial
        //isInDialog = true;// Initial
        //PNJContact.transform.GetChild(0).gameObject.SetActive(false);
        //PNJContact.GetComponent<PNJ>().Startdialogue();
    }

    public void EndDialog()
    {
        if(state != State.InCinematic)
        {
            dialAndBookCanvas.transform.GetChild(3).gameObject.SetActive(false);
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

    void QuitPauseMenu()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            QuitPauseMenuExe();
        }
    }

    void QuitPauseMenuExe()
    {
        GameObject.FindObjectOfType<Menu>().gameObject.SetActive(false);
        ChangeState(State.Normal);
    }

    IEnumerator TimerQuitDialog()
    {
        state = State.Inactive;
        yield return new WaitForSecondsRealtime(0.1f);
        if(state != State.InCinematic)
        {
            ChangeState(State.Normal);
        }
    }

    IEnumerator ChangeStateTimer(State newState)
    {
        state = State.Inactive;
        yield return new WaitForSeconds(0.1f);
        state = newState;
    }
}