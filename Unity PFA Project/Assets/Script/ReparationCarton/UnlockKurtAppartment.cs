using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockKurtAppartment : MonoBehaviour
{
    GameObject firePlace;
    GameObject doorSalon;
    EventsCheck eventManager;

    GameObject sideDoorAnim;
    GameObject sideDoorOutline;

    bool checkFinished;

    void Start()
    {
        firePlace = this.transform.Find("Cheminée").gameObject;
        doorSalon = this.transform.Find("Shortcuts").GetChild(0).gameObject;
        sideDoorAnim = this.transform.Find("Porte kurt latérale").gameObject;
        sideDoorOutline = this.transform.Find("OutlinePorteKurtLatérale").gameObject;
        sideDoorAnim.GetComponent<Animator>().enabled = false;
        SetActiveGO(sideDoorOutline, false);

    }

    // Update is called once per frame
    void Update()
    {
        CheckEvents();
    }

    void CheckEvents()
    {
        if (!checkFinished)
        {
            if (eventManager != null)
            {
                Debug.Log("3");

                if (eventManager.eventsList.Contains("TalkToKurt"))
                {
                    Debug.Log("4");

                    doorSalon.GetComponent<BoxCollider2D>().enabled = true;
                    firePlace.GetComponent<BoxCollider2D>().enabled = true;
                    sideDoorAnim.GetComponent<Animator>().enabled = true;
                    SetActiveGO(sideDoorOutline, true);
                    checkFinished = true;
                }
            }
            else
            {
                eventManager = GameObject.FindObjectOfType<EventsCheck>();
            }
        }

        
    }

    void SetActiveGO(GameObject newObject, bool Value)
    {
        newObject.SetActive(Value);
    }
}
