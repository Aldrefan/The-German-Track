using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPorteKurt : MonoBehaviour
{
    GameObject shortCut;
    GameObject doorKurt;
    Interactions plInteracts;
    EventsCheck eventManager;

    bool checkFinished;

    void Start()
    {

        shortCut = this.transform.Find("Shortcuts").GetChild(0).gameObject;
        doorKurt = this.transform.Find("doorKurt").gameObject;
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
            if (!shortCut.activeSelf)
            {
                if (eventManager != null)
                {
                    if (eventManager.eventsList.Contains("OuvrirPorteKurt")&&plInteracts.state == Interactions.State.Normal)
                    {
                        doorKurt.SetActive(false);
                        shortCut.SetActive(true);
                    }
                }
                else
                {
                    eventManager = GameObject.FindObjectOfType<EventsCheck>();
                    plInteracts = eventManager.GetComponent<Interactions>();
                }
            }
            else
            {
                checkFinished = true;
            }
        }
    }
}
