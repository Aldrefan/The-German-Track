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

    public List<StringToNotif> NotifQueue = new List<StringToNotif>();

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
            carnetGoal.RetryNotif();
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
            if (GameObject.FindObjectOfType<Ken_Canvas_Infos>() != null)
            {
                carnetGoal = GameObject.FindObjectOfType<Ken_Canvas_Infos>().transform.Find("Panel").Find("Carnet").Find("Goal").Find("GoalFrame").GetComponent<CarnetGoal>();
            }
        }
    }

    void SetNotifVisible()
    {
        CheckNullGoal();

        if(NotifQueue.Count !=0)
        {

            if (!notifAtor.GetBool("Visible"))
            {
                if (notifText.text != NotifQueue[0].goalTitle || NotifQueue[0].goalAchieved != notifAtor.GetBool("ValidGoal"))
                {
                    notifText.text = NotifQueue[0].goalTitle;

                    if (notifText.text != "")
                    {
                        if (notifAtor.GetBool("ValidGoal") != NotifQueue[0].goalAchieved)
                        {
                            notifAtor.SetBool("ValidGoal", NotifQueue[0].goalAchieved);
                            visibilityTime = 5;
                        }
                        else
                        {
                            visibilityTime = 3;
                        }
                        notifAtor.SetBool("Visible", true);

                        StartCoroutine(UnvisibleNotif());
                    }
                }
            }
        }
    }



    IEnumerator UnvisibleNotif()
    {
        yield return new WaitForSeconds(visibilityTime);
        if (notifAtor.GetBool("ValidGoal"))
        {

            notifAtor.SetBool("ValidGoal", false);

        }
        notifAtor.SetBool("Visible", false);

        if (NotifQueue.Count != 0)
        {
            notifText.text = "";
            NotifQueue.RemoveAt(0);
        }
    }

    void CheckNullGoal()
    {
        if (NotifQueue.Count != 0)
        {
            foreach (StringToNotif notif in NotifQueue)
            {
                if (notif.goalTitle == null)
                {
                    NotifQueue.Remove(notif);
                    return;
                }
            }
        }
    }
}

[System.Serializable]
public class StringToNotif
{
    public bool goalAchieved;
    public string goalTitle;

    public StringToNotif(string newGoal, bool newState)
    {
        goalTitle = newGoal;
        goalAchieved = newState;
    }
}
