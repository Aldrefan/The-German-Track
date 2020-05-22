using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePNGTransition : MonoBehaviour
{
    public List<QuoteEvent> quoteToChange = new List<QuoteEvent>();

    EventsCheck playerEventsCheck;
    PNJ pnjComponent;
    PNJ PnjComponent => pnjComponent = pnjComponent ?? this.GetComponent<PNJ>();

    bool eventListInCheck;
    int transitionIndex;



    private void Update()
    {
        if (!eventListInCheck)
        {
            eventListInCheck = true;
            StartCoroutine(CheckEventList());
        }

        if(transitionIndex != 0)
        {
            PnjComponent.transitionQuote = transitionIndex;
            transitionIndex = 0;
            eventListInCheck = false;
        }
    }

    IEnumerator CheckEventList()
    {
        yield return new WaitForSeconds(1);
        foreach(QuoteEvent quoteTrigger in quoteToChange)
        {
            foreach (string eventName in playerEventsCheck.eventsList)
            {
                if (quoteTrigger.quoteEventTrigger == eventName)
                {
                    transitionIndex = quoteTrigger.quoteIndex;
                    yield break;
                }
            }
        }
        eventListInCheck = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<EventsCheck>() && playerEventsCheck == null)
            {
                playerEventsCheck = other.GetComponent<EventsCheck>();
            }
        }
    }

}

public class QuoteEvent
{
    public int quoteIndex;
    public string quoteEventTrigger;
}
