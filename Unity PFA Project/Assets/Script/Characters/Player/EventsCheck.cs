using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCheck : MonoBehaviour
{
    public List<string> eventsList;
    bool lamp;
    public GameObject EtiquetteLaissezPasser;
    public List<GameObject> appartmentKurtLocked;

    PlayerMemory playerMemory;
    PlayerMemory PlayerMemory => playerMemory = playerMemory ?? GameObject.FindObjectOfType<PlayerMemory>();
    CarnetGoal carnetGoal;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<Ken_Canvas_Infos>() != null)
        {
            carnetGoal = GameObject.FindObjectOfType<Ken_Canvas_Infos>().transform.Find("Panel").Find("Carnet").Find("Goal").Find("GoalFrame").GetComponent<CarnetGoal>();

        }
        if(GameObject.FindObjectOfType<Interactions>().PnjMet.Contains("Kurt Becker"))
        {
            UnlockKurtAppartment();
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(GetComponent<Interactions>().PNJContact && GetComponent<Interactions>().PNJContact.name == "Fauteuil" && Input.GetButtonDown("Interaction") && GetComponent<Interactions>().state != Interactions.State.InCinematic)
        {
            GetComponent<Interactions>().PNJContact.GetComponent<Clara_Cinematic>().ExecuteCommand();
        }*/
        if(GetComponent<Interactions>().PNJContact && GetComponent<Interactions>().PNJContact.tag == "Interaction" && Input.GetButtonDown("Interaction") && GetComponent<Interactions>().state != Interactions.State.InCinematic && GetComponent<Interactions>().state == Interactions.State.InDialog)
        {
            Debug.Log("Check Interaction");
            CheckInteraction();
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("Directional Light").GetComponent<DayNightLight>().DayTime();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "trigger_PoliceReceptionist")
        {
            if(eventsList.Contains("policeOpen"))
            {
                Destroy(col.gameObject);
            }
            else
            {
                GetComponent<Interactions>().PNJContact = GameObject.Find("police_receptionist");
                GameObject.Find("police_receptionist").GetComponent<Clara_Cinematic>().CheckIndex();
                //collision.transform.GetChild(0).gameObject.SetActive(true);
                //GameObject.Find("trigger_PoliceReceptionist").SetActive(false);
                GetComponent<Interactions>().StartDialog();
            }
        }

        if(col.name == "doorPolice")
        {
            if(eventsList.Contains("policeOpen"))
            {
                Destroy(col.gameObject);
            }
        }
        
        if (col.name == "dialog_williamscott")
        {
            if(GetComponent<Interactions>().PnjMet.Contains("dialog_williamscott"))
            {
                Destroy(col.gameObject);
            }
        }

        if(col.name == "Fauteuil")
        {
            //Debug.Log("Contact Fauteuil");
            if(eventsList.Contains("HasSlep"))
            {
                col.GetComponent<PNJ>().enabled = false;
                col.GetComponent<Clara_Cinematic>().enabled = false;
                Destroy(col.gameObject);
            }
            /*else if(Input.GetButtonDown("Interaction"))
            {col.GetComponent<Clara_Cinematic>().ExecuteCommand();}*/
        }
        if(col.name == "KD_InvisibleWall")
        {
            //JsonSave save = SaveGameManager.GetCurrentSave();
            if(GetComponent<PlayerMemory>().allStickers.Contains(1) && GetComponent<PlayerMemory>().allStickers.Contains(2) && GetComponent<PlayerMemory>().allStickers.Contains(8) && GetComponent<PlayerMemory>().allStickers.Contains(4))
            {
                //GameObject.Find("KD_InvisibleWall").SetActive(false);
                col.GetComponent<Clara_Cinematic>().ExecuteCommand();
            }
            else
            {
                GetComponent<Interactions>().PNJContact = gameObject;
                GetComponent<PNJ>().ChangeDialog(0);
                GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            }
        }

        if(col.name == "E_InvisibleWall")
        {
            if(eventsList.Contains("HasSlep"))
            {
                Destroy(col.gameObject);
            }
            else
            { 
                GetComponent<Interactions>().PNJContact = gameObject;
                GetComponent<PNJ>().ChangeDialog(1);
                GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            }
        }

        if(col.name == "doorHopital")
        {
            if(eventsList.Contains("hopitalOpen"))
            {
                CheckEvents("hopitalOpen");
            }
            else
            {
                ////GetComponent<Interactions>().PNJContact = GameObject.Find("hospital_receptionist");
                ////GetComponent<Interactions>().state = Interactions.State.InDialog;
                ////GetComponent<Interactions>().StartDialog();
                col.GetComponent<Clara_Cinematic>().action = 0;
                col.GetComponent<Clara_Cinematic>().ExecuteCommand();
            }
        }

        /*if(col.name == "TestCollider")
        {
            CheckEvents("changelouisetest");
        }*/

        /*if(col.name == "Lamp")
        {
            col.transform.GetChild(0).gameObject.SetActive(true);
            //lamp = true;
        }*/
    }

    public void CheckInteraction()
    {
        switch(GetComponent<Interactions>().PNJContact.name)
        {
            case "doorKurt":
            GetComponent<Interactions>().PNJContact.GetComponent<Clara_Cinematic>().ExecuteCommand();
            break;

            case "Fauteuil" :
            GetComponent<Interactions>().PNJContact.GetComponent<Clara_Cinematic>().ExecuteCommand();
            break;

            case "Lamp" :
            LampEvent();
            break;
        }
    }

    void LampEvent()
    {
        if(GetComponent<Interactions>().PNJContact.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            GetComponent<Interactions>().PNJContact.transform.GetChild(1).gameObject.SetActive(false);
        }
        else 
        {
            GetComponent<Interactions>().PNJContact.transform.GetChild(1).gameObject.SetActive(true);
        }
        
        if(/*GetComponent<PlayerMemory>().allStickers.Contains(15)*/ eventsList.Contains("LettreADecoder") && !eventsList.Contains("LettreDecodee")) //possède l'objectif de la lettre codée non fait
        {
            if(/*eventsList.Contains("LettreDécodée")*/GetComponent<PlayerMemory>().allStickers.Contains(10) && GetComponent<PlayerMemory>().allStickers.Contains(45)) //a déjà décodé
            {
                //GetComponent<Interactions>().PNJContact = gameObject;
                //GameObject.FindGameObjectWithTag("Player").GetComponent<PNJ>().ChangeDialog(0);
                GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().ChangeDialog(3);
                GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            }
            else
            {
                GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().ChangeDialog(2);
                GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
                //eventsList.Add("LettreDécodée");
                //GetComponent<Interactions>().PNJContact = gameObject;
                //GameObject.FindGameObjectWithTag("Player").GetComponent<PNJ>().ChangeDialog(1);
                //GameObject.Find("Lamp").SetActive(false);
                //lamp = false;
            }
        }
        else if(eventsList.Contains("LettreDecodee")) //a déjà décodé)
        {
            GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().ChangeDialog(3);
            GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            //GetComponent<Interactions>().PNJContact = gameObject;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PNJ>().ChangeDialog(0);
        }
        else
        {
            GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().ChangeDialog(0);
            GetComponent<Interactions>().ChangeState(Interactions.State.InDialog);
            //GetComponent<Interactions>().PNJContact = gameObject;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PNJ>().ChangeDialog(0);
        }
    }

    void PierceFound()
    {
        foreach (Transform childTransform in CanvasManager.CManager.GetCanvas("CarnetPanel").transform.Find("Carnet").Find("Characters").Find("Page(Clone)"))
        {
            if(childTransform.GetComponent<Sticker_Display>().sticker.index == 1)
            {
                childTransform.GetComponent<Sticker_Display>().sticker = PlayerMemory.stickersScriptableList[16];
                childTransform.GetComponent<Sticker_Display>().SetInformations();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.name == "Fauteuil")
        {
            col.transform.GetChild(0).gameObject.SetActive(false);
        }
        if(col.name == "Lamp")
        {
            col.transform.GetChild(0).gameObject.SetActive(false);
            //lamp = false;
        }
        if (col.name == "doorHopital")
        {
            this.GetComponent<Interactions>().PNJContact = null;
        }
    }

    void UnlockKurtAppartment()
    {
        for(int i = 0; i < appartmentKurtLocked.Count; i++)
        {
            appartmentKurtLocked[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void CheckEvents(string newEvent)
    {
        switch (newEvent)
        {
            case "laissezPasser":
                //EtiquetteLaissezPasser.GetComponent<Animator>().SetTrigger("NewSticker");
                carnetGoal.RemoveGoal(new GoalKeys("LaissezPasser", "LaissezPasser"));
                carnetGoal.NewGoal(new GoalKeys("Goal_01","DescGoal_01"));
                break;

            case "hopitalOpen":
                GameObject.Find("doorHopital").gameObject.SetActive(false);
                break;

            case "policeOpen":
                GameObject.Find("doorPolice").SetActive(false);
                //GameObject.Find("trigger_PoliceReceptionist").SetActive(false);
                break;

            /*case "endDialogWilliamScott":
                GameObject.Find("dialog_williamscott").SetActive(false);
                //animation du faux scott qui sort
                //tp du vrai scott au bon endroit
                break;*/

            case "pibPhoneUnlocked":
                GameObject pibPhone = GameObject.Find("PIPPhone");
                pibPhone.transform.tag = "Interaction";
                break;

            case "getNumberMarvinMeyer":
                carnetGoal.NewGoal(new GoalKeys("Goal_02", "DescGoal_02"));
                break;

            case "numberMarvinMeyer":
                GameObject.Find("pib_phone").transform.GetChild(1).gameObject.SetActive(false);
                GetComponent<Interactions>().PNJContact = GameObject.Find("marvin_meyer_phone");
                GetComponent<Interactions>().StartDialog();
                break;

            case "numberClaraGrey":
                GetComponent<Interactions>().PNJContact = GameObject.Find("clara_grey_phone");
                GetComponent<Interactions>().StartDialog();
                break;

            /*case "doorKurtOpen":
                GetComponent<Interactions>().PNJContact.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/Door/KEY_In_Wood_Door_01_mono");
                GetComponent<Interactions>().PNJContact.GetComponent<AudioSource>().Play();
                GameObject.Find("doorKurt").SetActive(false);
                break;*/

            case "GoToSleep":
                carnetGoal.NewGoal(new GoalKeys("Goal_03", "DescGoal_03"));
                break;

            case "HasSlep":
                carnetGoal.RemoveGoal(new GoalKeys("Goal_03", "DescGoal_03"));
                break;

            case "GoToSeeWhite":
                carnetGoal.NewGoal(new GoalKeys("Goal_04", "DescGoal_04"));

                break;

            case "HasSeenWhite":
                carnetGoal.RemoveGoal(new GoalKeys("Goal_04", "DescGoal_04"));
                break;

            case "HasSeenLou":
                carnetGoal.RemoveGoal(new GoalKeys("Goal_01", "DescGoal_01"));
                break;

            case "HasContactedMarvin":
                carnetGoal.RemoveGoal(new GoalKeys("Goal_02", "DescGoal_02"));
                break;
            
            case "TalkToTheBarman":
                carnetGoal.NewGoal(new GoalKeys("Goal_05", "DescGoal_05"));
                break;

            case "HasTalkedToTheBarman":
                carnetGoal.RemoveGoal(new GoalKeys("Goal_05", "DescGoal_05"));
                break;

            case "TalkToKurt":
                UnlockKurtAppartment();
                break;

            case "LettreADecoder":
                carnetGoal.NewGoal(new GoalKeys("Goal_06", "DescGoal_06"));
                break;

            case "LettreDecodee":
                carnetGoal.RemoveGoal(new GoalKeys("Goal_06", "DescGoal_06"));
                break;

            case "OuvrirPorteKurt":
                break;

            case "Fade":
            GetComponent<Interactions>().enabled = false;
            GameObject.FindObjectOfType<DialogInterface>().GetComponent<Collider2D>().enabled = false;
            GetComponent<Clara_Cinematic>().ExecuteCommand();
            break;

            case "SpeakToPeople":
                carnetGoal.NewGoal(new GoalKeys("GoalName_01", "GoalDesc_01"));
                break;

            case "FindPierce":
                carnetGoal.RemoveGoal(new GoalKeys("GoalName_01", "GoalDesc_01"));
                carnetGoal.NewGoal(new GoalKeys("GoalName_02", "GoalDesc_02"));
                GameObject.Find("Door_01_01").GetComponent<BoxCollider2D>().enabled = true;
                break;

            case "PierceFound":
                //GameObject.Find("Door_01_01").GetComponent<BoxCollider2D>().enabled = false;
                //RoomManager.RM.roomList[1].transform.GetChild(1).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                //RoomManager.RM.roomList[1].transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                GameObject.Find("Door_01_01").GetComponent<BoxCollider2D>().enabled = false;
                GameObject.Find("CineTriggerBar").GetComponent<BoxCollider2D>().enabled = true;
                carnetGoal.RemoveGoal(new GoalKeys("GoalName_02", "GoalDesc_02"));
                PierceFound();
                break;

            case "WhiteAim":
                GameObject.Find("White").GetComponent<Animator>().SetBool("Aim", true);
                GameObject.Find("White").GetComponent<PNJStates>().boolList.Find(x => x.boolName == "Aim").state = true;
                GameObject.Find("Louise").GetComponent<Animator>().SetBool("Wake", true);
                GameObject.Find("Louise").GetComponent<PNJStates>().boolList.Find(x => x.boolName == "Wake").state = true;
                DialogueChanger.DialChangr.ChangeDialogueComponent("Louise2");
                DialogueChanger.DialChangr.ChangeDialogueComponent("Renard2");
                carnetGoal.NewGoal(new GoalKeys("GoalName_03", "GoalDesc_03"));
            break;

            case "Bar_Panic":
                GameObject.Find("Louise").GetComponent<Animator>().Play("Louise_Crouch", 0);
                GameObject.Find("Dr").GetComponent<Animator>().Play("Dr_Idle_Crouch", 0);
            break;

            default:
                break;
        }
    }
}
