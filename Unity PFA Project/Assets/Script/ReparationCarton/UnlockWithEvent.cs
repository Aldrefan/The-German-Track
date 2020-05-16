using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWithEvent : MonoBehaviour
{
    [SerializeField]
    string eventName;

    bool isUnlock;

    EventsCheck playerEventsCheck;
    BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = this.GetComponent<BoxCollider2D>();
        UnlockingCheck();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEvent();
    }

    void UnlockingCheck()
    {
        if (coll.enabled != isUnlock)
        {
            coll.enabled = isUnlock;
        }
    }

    void CheckEvent()
    {
        if (playerEventsCheck != null)
        {
            if (playerEventsCheck.eventsList.Contains(eventName))
            {
                isUnlock = true;
                UnlockingCheck();
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerEventsCheck == null) {
                playerEventsCheck = other.GetComponent<EventsCheck>();
            }
        }
    }


}
