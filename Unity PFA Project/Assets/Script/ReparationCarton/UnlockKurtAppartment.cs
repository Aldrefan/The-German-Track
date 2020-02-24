using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockKurtAppartment : MonoBehaviour
{
    GameObject firePlace;
    GameObject doorSalon;
    EventsCheck eventManager;

    bool checkFinished;

    void Start()
    {
        firePlace = this.transform.Find("Cheminée").gameObject;
        doorSalon = this.transform.Find("Shortcuts").GetChild(0).gameObject; 
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
            if (doorSalon.GetComponent<BoxCollider2D>().enabled == false)
            {
                if (eventManager != null)
                {
                    if (eventManager.eventsList.Contains("TalkToKurt"))
                    {
                        doorSalon.GetComponent<BoxCollider2D>().enabled = true;
                        firePlace.GetComponent<BoxCollider2D>().enabled = true;
                        Debug.Log("1");
                    }
                }
                else
                {
                    eventManager = GameObject.FindObjectOfType<EventsCheck>();
                }
            }
            else
            {
                checkFinished = true;
            }
        }
    }
}
