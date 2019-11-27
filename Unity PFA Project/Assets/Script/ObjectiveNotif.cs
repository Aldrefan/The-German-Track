using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveNotif : MonoBehaviour
{
    [SerializeField]
    float visibilityTime = 3;

    EventsCheck playerEventsCheck;
    Animator notifAtor;
    Text notifText;

    [HideInInspector]
    public string textToNotify;

    // Update is called once per frame
    void Update()
    {
        if(playerEventsCheck == null)
        {
            StartCoroutine(FindPlayer());
        }
        else
        {
            playerEventsCheck.notif = this;

        }

        SetNotifVisible();

    }

    IEnumerator FindPlayer()
    {
        new WaitForSeconds(0.1f);
        if (FindObjectOfType<EventsCheck>() != null)
        {
            notifText = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            notifAtor = this.GetComponent<Animator>();
            playerEventsCheck = FindObjectOfType<EventsCheck>();
        }
        yield return null;
    }

    void SetNotifVisible()
    {
        if(notifText.text != textToNotify)
        {
            notifText.text = textToNotify;
            notifAtor.SetBool("Visible", true);
            StartCoroutine(UnvisibleNotif());
        }
    }

    IEnumerator UnvisibleNotif()
    {
        new WaitForSeconds(visibilityTime);
        notifAtor.SetBool("Visible", false);
        yield return null;
    }
}
