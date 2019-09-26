using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clara_Cinematic : MonoBehaviour
{
    bool movements;
    enum Command {Movement, StartDialog, Wait, ActiveDialogComponent, ChangeParent};

    [SerializeField]
    List<Command> commandList;
    [SerializeField]
    List<TimeAndDirection> annexInformation;
    public int action = 0;
    public bool fromStart;

    [System.Serializable]
    public class TimeAndDirection
    {
        public float direction;
        public float time;
        public Transform newParent;
        public Transform newPosition;
    }
    Rigidbody2D rb2D;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if(fromStart)
        {
            ExecuteCommand();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(movements)
        {
            transform.position = new Vector2(transform.position.x + annexInformation[action].direction, transform.position.y);
        }
    }

    IEnumerator MovementTimer(float time)
    {
        movements = true;
        if(annexInformation[action].direction < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else GetComponent<SpriteRenderer>().flipX = false;
        animator.SetBool("Walk", true);
        yield return new WaitForSecondsRealtime(time);
        animator.SetBool("Walk", false);
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().QuitCinematicMode();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInCinematic = false;
            GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", false);
        }
    }

    void Fonction_StartDialog()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator.SetBool("Talk", true);
        player.GetComponent<Interactions>().PNJContact = gameObject;
        player.GetComponent<Interactions>().StartDialog();
    }

    public void ExecuteCommand()
    {
        GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().ChangeState(Interactions.State.InCinematic);
        if(commandList[action] == Command.Movement)
        {
            IEnumerator newCoroutine = MovementTimer(annexInformation[action].time);
            StartCoroutine(newCoroutine);
        }
        if(commandList[action] == Command.StartDialog)
        {
            Fonction_StartDialog();
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInCinematic = true;
        }
        if(commandList[action] == Command.Wait)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInCinematic = true;
            IEnumerator newCoroutine = WaitTimer(annexInformation[action].time);
            StartCoroutine(newCoroutine);
        }
        if(commandList[action] == Command.ChangeParent)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInCinematic = true;
            ChangeParent();
        }
        if(commandList[action] == Command.ActiveDialogComponent)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().isInCinematic = true;
            ActiveDialogComponent();
        }
    }

    void ChangeParent()
    {
        transform.SetParent(annexInformation[action].newParent);
        transform.localPosition = annexInformation[action].newPosition.transform.localPosition;
        CheckIndex();
    }

    void ActiveDialogComponent()
    {
        transform.tag = "PNJinteractable";
        GetComponent<PNJ>().enabled = true;
        CheckIndex();
    }
}
