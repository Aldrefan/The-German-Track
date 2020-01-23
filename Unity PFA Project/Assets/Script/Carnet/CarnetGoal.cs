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

    Transform completedGoals;
    Transform toCompleteGoals;
    Object goalObject;

    [HideInInspector]
    public ObjectiveNotif notif;

    // Start is called before the first frame update
    void Start()
    {
        completedGoals = this.transform.Find("CompletedGoals").GetChild(0).GetChild(0).GetChild(0);
        toCompleteGoals = this.transform.Find("GoalToComplete").GetChild(0).GetChild(0).GetChild(0);
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
            notif.textToNotify = goalString;
        }

    }

    public void RemoveGoal(string goalString)
    {
        if (!removeGoalList.Contains(goalString))
        {
            removeGoalList.Add(goalString);
        }
    }

    void OnEnable()
    {
        foreach (string goal in goalList)
        {
            GameObject newGoal = Instantiate(goalObject,toCompleteGoals) as GameObject;
            newGoal.transform.SetParent(toCompleteGoals);
            newGoal.GetComponent<RectTransform>().localPosition = Vector3.zero;
            newGoal.name = goalObject.name;
            newGoal.GetComponent<Goal>().Init(goal);
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
            foreach (Transform finishedGoal in toCompleteGoals)
            {
                if(finishedGoal.name == goal)
                {
                    finishedGoal.SetParent(completedGoals);
                    finishedGoal.GetComponent<Image>().color = Color.gray;
                }
            }
        }

    }
}