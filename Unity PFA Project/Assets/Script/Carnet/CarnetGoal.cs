using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarnetGoal : MonoBehaviour
{
    //public Vector3 scale;
    //public Vector2 heightWidth;
    //public Font font;
    //public int fontSize;
    public List<string> goalList;
    public List<string> removeGoalList;

    public Transform completedGoals;
    public Transform toCompleteGoals;
    public Transform goalDescription;
    public Object goalObject;

    public ObjectiveNotif notif;
    public string notifSaver;

    // Start is called before the first frame update
    public void Init()
    {
        completedGoals = this.transform.Find("CompletedGoals").Find("GoalListScrollViewport").Find("GoalListBckGrd").Find("GoalListViewport");
        toCompleteGoals = this.transform.Find("GoalToComplete").Find("GoalListScrollViewport").Find("GoalListBckGrd").Find("GoalListViewport");
        goalDescription = this.transform.Find("GoalDescription");
        goalObject = Resources.Load("UIObject/Goal");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGoal(string goalString)
    {
        if (!goalList.Contains(goalString))
        {
            goalList.Add(goalString);
            if(notif != null)
            {
                notif.textToNotify = goalString;

            }
            else
            {
                notifSaver = goalString;
            }
        }
    }
    
    public void RetryNotif()
    {
        if (notif != null && notifSaver != null)
        {
            notif.textToNotify = notifSaver;
            notifSaver = null;

        }
    }

    public void RemoveGoal(string goalString)
    {
        if (!removeGoalList.Contains(goalString))
        {
            removeGoalList.Add(goalString);
            //if (!goalList.Contains(goalString))
            //{
            //    Debug.Log("Delete current Goal");
                goalList.Remove(goalString);
            //}
        }
    }

    void OnEnable()
    {
        foreach (string goal in goalList)
        {
            int counter = 0;
            foreach (Transform currentGoal in toCompleteGoals)
            {
                if(currentGoal.GetComponent<Goal>().ReturnGoal() == goal)
                {
                    counter++;
                }
                
            }
            if (counter ==0) 
            {
                CreateGoal(true, goal, toCompleteGoals);
            }

            //newGoal.GetComponent<Text>().horizontalOverflow =  HorizontalWrapMode.Overflow;
            //newGoal.AddComponent<Text>();
            //newGoal.GetComponent<RectTransform>().sizeDelta = heightWidth;
            //newGoal.GetComponent<Text>().color = Color.black;
            //newGoal.GetComponent<Text>().font = font;
            //newGoal.GetComponent<Text>().text = goal;
            //newGoal.transform.localScale = scale;
        }

        foreach (string goal in removeGoalList)
        {
            int counter = 0;
            foreach (Transform currentGoal in completedGoals)
            {
                if (currentGoal.GetComponent<Goal>().ReturnGoal() == goal)
                {
                    counter++;
                }

            }
            foreach (Transform finishedGoal in toCompleteGoals)
            {
                if (finishedGoal.GetComponent<Goal>().ReturnGoal() == goal)
                {
                    finishedGoal.SetParent(completedGoals);
                    finishedGoal.GetComponent<Goal>().ChangeColor(Color.gray);
                    counter++;
                }
            }

            if (counter == 0)
            {
                CreateGoal(false, goal, completedGoals);
            }
        }
    }

    void CreateGoal(bool isNewGoal, string goalSentence, Transform parent)
    {

        GameObject newGoal = Instantiate(goalObject, parent) as GameObject;
        newGoal.transform.SetParent(parent);
        newGoal.GetComponent<RectTransform>().localPosition = Vector3.zero;
        newGoal.name = goalObject.name;
        newGoal.GetComponent<Goal>().Init(goalSentence);
        newGoal.GetComponent<Goal>().goalDescriptionTransform = goalDescription;
        if (!isNewGoal)
        {
            newGoal.GetComponent<Goal>().ChangeColor(Color.gray);

        }
    }
}