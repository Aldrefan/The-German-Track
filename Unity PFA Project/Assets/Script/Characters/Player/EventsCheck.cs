using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCheck : MonoBehaviour
{
    public List<string> eventsList;
    public bool fauteuil;
    bool lamp;
    public GameObject EtiquetteLaissezPasser;
    public GameObject clara;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fauteuil && Input.GetButtonDown("Interaction"))
        {
            GetComponent<Interactions>().PNJContact = gameObject;
            GetComponent<PNJ>().ChangeDialog(4);
        }

        /*if(lamp && Input.GetButtonDown("Interaction"))// Initial
        {
            //JsonSave save = SaveGameManager.GetCurrentSave();
            if(GetComponent<PlayerMemory>().allStickers.Contains(15))
            {
                if(eventsList.Contains("LettreDécodée"))
                {
                    GetComponent<Interactions>().PNJContact = gameObject;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PNJ>().ChangeDialog(3);
                }
                else
                {
                    eventsList.Add("LettreDécodée");
                    GetComponent<Interactions>().PNJContact = gameObject;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PNJ>().ChangeDialog(2);
                    //GameObject.Find("Lamp").SetActive(false);
                    lamp = false;
                }
            }
            else 
            {
                GetComponent<Interactions>().PNJContact = gameObject;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PNJ>().ChangeDialog(3);
            }
        }*/
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
            GetComponent<Interactions>().PNJContact = GameObject.Find("police_receptionist");
            //collision.transform.GetChild(0).gameObject.SetActive(true);
            //GameObject.Find("trigger_PoliceReceptionist").SetActive(false);
            GetComponent<Interactions>().StartDialog();
        }
        
        if (col.name == "dialog_williamscott")
        {
            GetComponent<Interactions>().PNJContact = col.gameObject;
            col.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Interactions>().StartDialog();
        }
        if(col.name == "KD_InvisibleWall")
        {
            //JsonSave save = SaveGameManager.GetCurrentSave();
            if(GetComponent<PlayerMemory>().allStickers.Contains(1) && GetComponent<PlayerMemory>().allStickers.Contains(2) && GetComponent<PlayerMemory>().allStickers.Contains(8) && GetComponent<PlayerMemory>().allStickers.Contains(4))
            {
                GameObject.Find("KD_InvisibleWall").SetActive(false);
            }
            else
            {
                GetComponent<Interactions>().PNJContact = gameObject;
                GetComponent<PNJ>().ChangeDialog(0);
            }
        }

        if(col.name == "E_InvisibleWall")
        {
            GetComponent<Interactions>().PNJContact = gameObject;
            GetComponent<PNJ>().ChangeDialog(1);
        }

        if(col.name == "Fauteuil")
        {
            col.transform.GetChild(0).gameObject.SetActive(true);
            fauteuil = true;
        }

        if(col.name == "Lamp")
        {
            col.transform.GetChild(0).gameObject.SetActive(true);
            lamp = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.name == "Fauteuil")
        {
            col.transform.GetChild(0).gameObject.SetActive(false);
            fauteuil = false;
        }
        if(col.name == "Lamp")
        {
            lamp = false;
        }
    }

    public void CheckEvents(string newEvent)
    {
        switch (newEvent)
        {
            case "laissezPasser":
                EtiquetteLaissezPasser.GetComponent<Animator>().SetTrigger("NewSticker");
                break;
            case "hopitalOpen":
                GameObject.Find("doorHopital").SetActive(false);
                break;

            case "policeOpen":
                GameObject.Find("doorPolice").SetActive(false);
                GameObject.Find("trigger_PoliceReceptionist").SetActive(false);
                break;

            case "endDialogWilliamScott":
                GameObject.Find("dialog_williamscott").SetActive(false);
                //animation du faux scott qui sort
                //tp du vrai scott au bon endroit
                break;

            case "pibPhoneUnlocked":
                GameObject pibPhone = GameObject.Find("pib_phone");
                pibPhone.transform.GetChild(0).gameObject.SetActive(false);
                pibPhone.transform.GetChild(1).gameObject.SetActive(true);
                pibPhone.transform.GetChild(2).gameObject.SetActive(true);
                break;

            case "numberMarvinMeyer":
                GetComponent<Interactions>().PNJContact = GameObject.Find("marvin_meyer_phone");
                GetComponent<Interactions>().StartDialog();
                break;

            case "numberClaraGrey":
                GetComponent<Interactions>().PNJContact = GameObject.Find("clara_grey_phone");
                GetComponent<Interactions>().StartDialog();
                break;

            case "doorKurtOpen":
                GameObject.Find("doorKurt").SetActive(false);
                break;

            default:
                break;
        }
    }

}
