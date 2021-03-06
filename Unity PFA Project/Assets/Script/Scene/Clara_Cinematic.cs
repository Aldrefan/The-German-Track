﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clara_Cinematic : MonoBehaviour
{
    bool movements;
    enum Command {Movement, StartDialog, Wait, ActiveDialogComponent, ChangeParent, PlaySound, DeactivateSelf, DeactivateOther, ActivateObject, FadePanel, SetDay, EndGame, LoadScene, SetAnimBool, ShowImage, PlayAnimation, UseShortcut, Flip, DisableObject, Reculer, Event};

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
        public int newSceneName;
        public BoolNameAndState boolNameAndState;
        public Sprite image;
    }

    [System.Serializable]
    public class BoolNameAndState
    {
        public string name;
        public bool state;
        public bool reverseAnim;
    }

    public bool triggerByContact;
    public bool hasToLoop;

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
        float mass = 0;
        //GetComponent<BoxCollider2D>().enabled = false;
        //Debug.Log(GetComponent<BoxCollider2D>().enabled);
        if(annexInformation[action].objectToMove.tag != "Player")
        {
            annexInformation[action].objectToMove.GetComponent<BoxCollider2D>().enabled = false;
            if(annexInformation[action].direction < 0)
            {
                annexInformation[action].objectToMove.GetComponent<SpriteRenderer>().flipX = true;
            }
            else annexInformation[action].objectToMove.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            mass = annexInformation[action].objectToMove.GetComponent<Rigidbody2D>().mass;
            annexInformation[action].objectToMove.GetComponent<Rigidbody2D>().mass = 0;
            annexInformation[action].objectToMove.GetComponent<MovementsPlayer>().CheckSensAndFlip(annexInformation[action].direction);
        }
        //annexInformation[action].objectToMove.GetComponent<Animator>().SetBool("Talk", false); // à voir
        //annexInformation[action].objectToMove.GetComponent<Animator>().SetBool("Walk", true); // à voir
        yield return new WaitForSeconds(time);
        //GetComponent<BoxCollider2D>().enabled = true;
        //Debug.Log(GetComponent<BoxCollider2D>().enabled);
        annexInformation[action].objectToMove.GetComponent<BoxCollider2D>().enabled = true;
        annexInformation[action].objectToMove.GetComponent<Animator>().SetBool("Walk", false);
        if(annexInformation[action].objectToMove.tag == "Player")
        {annexInformation[action].objectToMove.GetComponent<Rigidbody2D>().mass = mass;}
        movements = false;
        CheckIndex();
    }

    IEnumerator ReculeTimer(float time)
    {
        movements = true;
        float mass = 0;
        if(annexInformation[action].objectToMove.tag != "Player")
        {
            annexInformation[action].objectToMove.GetComponent<BoxCollider2D>().enabled = false;
            if(annexInformation[action].direction < 0)
            {
                annexInformation[action].objectToMove.GetComponent<SpriteRenderer>().flipX = false;
            }
            else annexInformation[action].objectToMove.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            mass = annexInformation[action].objectToMove.GetComponent<Rigidbody2D>().mass;
            annexInformation[action].objectToMove.GetComponent<Rigidbody2D>().mass = 0;
            annexInformation[action].objectToMove.GetComponent<MovementsPlayer>().CheckSensAndFlip(-annexInformation[action].direction);
        }

        yield return new WaitForSeconds(time);

        annexInformation[action].objectToMove.GetComponent<BoxCollider2D>().enabled = true;
        annexInformation[action].objectToMove.GetComponent<Animator>().SetBool("Walk", false);
        if(annexInformation[action].objectToMove.tag == "Player")
        {annexInformation[action].objectToMove.GetComponent<Rigidbody2D>().mass = mass;}
        movements = false;
        CheckIndex();
    }

    IEnumerator WaitTimer(float time)
    {
        yield return new WaitForSeconds(time);
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
                CanvasManager.CManager.GetCanvas("Dialogue").SetActive(true);
                CanvasManager.CManager.GetCanvas("Board_FL").SetActive(false);
                //player.GetComponent<Interactions>().boardCanvas.SetActive(false);
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
        GameObject.Find("BlackBands").GetComponent<Animator>().SetBool("Cinematic", true);
        if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.name == "Kenneth" && GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.GetComponent<Interactions>().state != Interactions.State.InCinematic)
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
            break;

            case Command.LoadScene :
            LoadScene();
            break;

            case Command.SetAnimBool :
            SetAnimBool();
            break;

            case Command.ShowImage :
            ShowImage();
            break;

            case Command.PlayAnimation :
            PlayAnimation();
            break;

            case Command.UseShortcut :
            UseShortcut();
            break;

            case Command.Flip :
            Flip();
            break;

            case Command.DisableObject :
            DisableObject();
            break;

            case Command.Reculer :
            StartCoroutine(ReculeTimer(annexInformation[action].time));
            break;

            case Command.Event :
            Event();
            break;
        }
    }

    void Event()
    {
        GameObject.FindObjectOfType<EventsCheck>().CheckEvents(annexInformation[action].boolNameAndState.name);
        CheckIndex();
    }

    void DisableObject()
    {
        annexInformation[action].objectToMove.SetActive(false);
        CheckIndex();
    }

    void Flip()
    {
        annexInformation[action].objectToMove.GetComponent<SpriteRenderer>().flipX = !annexInformation[action].objectToMove.GetComponent<SpriteRenderer>().flipX;
        CheckIndex();
    }

    void UseShortcut()
    {
        annexInformation[action].objectToMove.GetComponent<RoomInformations>().Teleport(annexInformation[action].boolNameAndState.name);
        CheckIndex();
    }

    void PlayAnimation()
    {
        annexInformation[action].objectToMove.GetComponent<Animator>().Play(annexInformation[action].boolNameAndState.name);
        CheckIndex();
    }

    void ShowImage()
    {
        GameObject canvas = Resources.Load("GameObject/CanvasShowImage") as GameObject;
        canvas = Instantiate(canvas);
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        canvas.GetComponent<Canvas>().sortingLayerName = "ForeGround";
        canvas.transform.GetChild(0).GetComponent<Image>().sprite = annexInformation[action].image;
        canvas.transform.GetChild(0).GetComponent<Animator>().Play("FadeImage");
        CheckIndex();
    }

    void SetAnimBool()
    {
        if(annexInformation[action].objectToMove.tag == "Player")
        {
            annexInformation[action].objectToMove.GetComponent<MovementsPlayer>().enabled = !annexInformation[action].boolNameAndState.state;
        }
        if(annexInformation[action].boolNameAndState.reverseAnim)
        {annexInformation[action].objectToMove.GetComponent<Animator>().SetFloat(annexInformation[action].boolNameAndState.name + "Speed", annexInformation[action].objectToMove.GetComponent<Animator>().GetFloat(annexInformation[action].boolNameAndState.name + "Speed") * -1);}
        annexInformation[action].objectToMove.GetComponent<Animator>().SetBool(annexInformation[action].boolNameAndState.name, annexInformation[action].boolNameAndState.state);
        CheckIndex();
    }

    void LoadScene()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(annexInformation[action].newSceneName);
    }

    void EndGame()
    {
        GameObject.Find("FadePanel").GetComponent<Animator>().SetBool("FadeIn",true);
        StartCoroutine("EndTimer");
    }
    IEnumerator EndTimer()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        GameObject.Find("FadePanel").GetComponent<Animator>().SetBool("FadeIn", false);
        GameObject.Find("EndCanvas").GetComponent<EndScreen>().EndDemo();
    }

    IEnumerator EndFade()
    {
        //Debug.Log("2");
        yield return new WaitForSecondsRealtime(0.001f);
        //Debug.Log("3");

        GameObject.Find("FadePanel").GetComponent<Animator>().SetBool("FadeIn", false);

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
        CanvasManager.CManager.GetCanvas("Fade_Panel").GetComponent<Animator>().SetBool("FadeIn", true);
        //Debug.Log("1");

        StartCoroutine(EndFade());
        CheckIndex();
    }
}