using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clara_Cinematic : MonoBehaviour
{
    bool movements;
    enum Command {Movement, StartDialog, Wait, ActiveDialogComponent, ChangeParent, PlaySound, DeactivateSelf, DeactivateOther, ActivateObject, FadePanel, SetDay, EndGame};

    [SerializeField]
    List<Command> commandList;
    
    public List<TimeAndDirection> annexInformation;
    public int action = 0;
    public bool fromStart;

    [System.Serializable]
    public class TimeAndDirection
    {
        public GameObject objectToMove;
        public float direction;
        public float time;
        public Transform newParent;
        public Transform newPosition;
        public AudioClip clip;
        public AudioSource Origin;
    }

    public bool triggerByContact;

    Rigidbody2D rb2D;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        if(fromStart)
        {
            ExecuteCommand();
        }
    }
    
    void Awake()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(movements)
        {
            annexInformation[action].objectToMove.transform.position = new Vector2(annexInformation[action].objectToMove.transform.position.x + annexInformation[action].direction, annexInformation[action].objectToMove.transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && triggerByContact)
        {
            StartCoroutine(TimerBeforeStart());
        }
    }

    IEnumerator TimerBeforeStart()
    {
        yield return new WaitForSeconds(0.01f);
        GetComponent<Collider2D>().enabled = false;
        ExecuteCommand();
    }

    IEnumerator MovementTimer(float time)
    {
        movements = true;
        //GetComponent<BoxCollider2D>().enabled = false;
        //Debug.Log(GetComponent<BoxCollider2D>().enabled);
        annexInformation[action].objectToMove.GetComponent<BoxCollider2D>().enabled = false;
        if(annexInformation[action].direction < 0 && annexInformation[action].objectToMove.tag != "Player")
        {
            annexInformation[action].objectToMove.GetComponent<SpriteRenderer>().flipX = true;
        }
        else annexInformation[action].objectToMove.GetComponent<SpriteRenderer>().flipX = false;
        if(annexInformation[action].objectToMove.tag == "Player")
        {
            annexInformation[action].objectToMove.GetComponent<MovementsPlayer>().CheckSensAndFlip(annexInformation[action].direction);
        }
        annexInformation[action].objectToMove.GetComponent<Animator>().SetBool("Talk", false);
        annexInformation[action].objectToMove.GetComponent<Animator>().SetBool("Walk", true);
        yield return new WaitForSeconds(time);
        //GetComponent<BoxCollider2D>().enabled = true;
        //Debug.Log(GetComponent<BoxCollider2D>().enabled);
        annexInformation[action].objectToMove.GetComponent<BoxCollider2D>().enabled = true;
        annexInformation[action].objectToMove.GetComponent<Animator>().SetBool("Walk", false);
        movements = false;
        CheckIndex();
    }

    IEnumerator WaitTimer(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        CheckIndex();
    }

    public void CheckIndex()
    {
        if(action < commandList.Count - 1)
        {
            action++;
            ExecuteCommand();
        }
        else 
        {
            if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInCinematic = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInDialog = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().QuitCinematicMode();
                GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", false);
            }
        }
    }

    void Fonction_StartDialog()
    {
        if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player.GetComponent<Interactions>().state == Interactions.State.OnBoard)
            {
                player.GetComponent<Interactions>().dialAndBookCanvas.SetActive(true);
                player.GetComponent<Interactions>().boardCanvas.SetActive(false);
            }
            if(annexInformation[action].objectToMove == null)
            {
                player.GetComponent<Interactions>().PNJContact = gameObject;
            }
            else player.GetComponent<Interactions>().PNJContact = annexInformation[action].objectToMove;
        
            player.GetComponent<Interactions>().isInCinematic = true;
            player.GetComponent<Interactions>().isInDialog = true;
            //annexInformation[action].objectToMove.GetComponent<Animator>().SetBool("Talk", true);
            player.GetComponent<Interactions>().StartDialog();
        }
    }

    public void ExecuteCommand()
    {
        Debug.Log("Execute");
        GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", true);
        if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().ChangeState(Interactions.State.InCinematic);
        }
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().dialAndBookCanvas.transform.GetChild(2).GetComponent<Animator>().SetBool("ClickOn", true);
        //Debug.Log("Action n° " + action + " Command " + commandList[action]);
        switch (commandList[action])
        {
            case Command.Movement :
            StartCoroutine("MovementTimer", annexInformation[action].time);
            break;

            case Command.StartDialog :
            Fonction_StartDialog();
            break;

            case Command.Wait :
            StartCoroutine("WaitTimer", annexInformation[action].time);
            break;

            case Command.ChangeParent :
            ChangeParent();
            break;

            case Command.ActiveDialogComponent :
            ActiveDialogComponent();
            break;

            case Command.PlaySound :
            PlaySound();
            break;

            case Command.DeactivateSelf :
            DeactivateSelf();
            break;

            case Command.DeactivateOther :
            DeactivateOther();
            break;

            case Command.ActivateObject :
            ActivateObject();
            break;

            case Command.FadePanel :
            FadePanel();
            break;

            case Command.SetDay :
            SetDay();
            break;

            case Command.EndGame :
            EndGame();
            Debug.Log("CommandEndGame");
            break;
        }
    }

    void EndGame()
    {
        Debug.Log("EndGame");
        GameObject.Find("FadePanel").GetComponent<Animator>().SetTrigger("FadeIn");
        StartCoroutine("EndTimer");
    }
    IEnumerator EndTimer()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        GameObject.Find("EndCanvas").GetComponent<EndScreen>().EndDemo();
    }

    void SetDay()
    {
        GameObject.FindObjectOfType<DayNightLight>().DayTime();
        CheckIndex();
    }

    void DeactivateSelf()
    {
        if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInCinematic = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInDialog = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().QuitCinematicMode();
        }
        GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", false);
        Destroy(gameObject);
    }

    void ChangeParent()
    {
        annexInformation[action].objectToMove.transform.SetParent(annexInformation[action].newParent);
        annexInformation[action].objectToMove.transform.localPosition = annexInformation[action].newPosition.transform.localPosition;
        CheckIndex();
    }

    void ActiveDialogComponent()
    {
        annexInformation[action].objectToMove.transform.tag = "PNJinteractable";
        annexInformation[action].objectToMove.GetComponent<PNJ>().enabled = true;
        CheckIndex();
    }

    void PlaySound()
    {
        annexInformation[action].Origin.clip = annexInformation[action].clip;
        annexInformation[action].Origin.Play();
        StartCoroutine("Timer", annexInformation[action].clip.length);
        //CheckIndex();
    }

    void DeactivateOther()
    {
        Destroy(annexInformation[action].objectToMove);
        CheckIndex();
    }

    void ActivateObject()
    {
        annexInformation[action].objectToMove.SetActive(true);
        CheckIndex();
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        CheckIndex();
    }

    void FadePanel()
    {
        GameObject.Find("FadePanel").GetComponent<Animator>().SetTrigger("FadeIn");
        //StartCoroutine("Timer", 2);
        CheckIndex();
    }
}