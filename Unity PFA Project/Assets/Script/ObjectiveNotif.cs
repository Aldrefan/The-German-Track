using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveNotif : MonoBehaviour
{
    [SerializeField]
    float visibilityTime = 3;

    CarnetGoal carnetGoal;
    Animator notifAtor;
    Text notifText;

    [HideInInspector]
    public string textToNotify;

    // Update is called once per frame
    void Update()
    {
        if(carnetGoal == null)
        {
            StartCoroutine(FindPlayer());
        }
        else
        {
            carnetGoal.notif = this;
            carnetGoal.Init();
        }

        if (carnetGoal != null)
        {
            SetNotifVisible();
        }
        
    }

    IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        if (FindObjectOfType<EventsCheck>() != null)
        {
            notifText = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            notifAtor = this.GetComponent<Animator>();
            carnetGoal = GameObject.FindObjectOfType<Ken_Canvas_Infos>().transform.Find("Panel").Find("Carnet").Find("Goal").Find("GoalFrame").GetComponent<CarnetGoal>();
        }
    }

    void SetNotifVisible()
    {
        if(notifText.text != textToNotify)
        {

            notifText.text = textToNotify;
            if(notifText.text != "")
            {
                notifAtor.SetBool("Visible", true);

            }
            StartCoroutine(UnvisibleNotif());
        }
    }

    IEnumerator UnvisibleNotif()
    {
        yield return new WaitForSeconds(visibilityTime);
        notifAtor.SetBool("Visible", false);

    }
}
