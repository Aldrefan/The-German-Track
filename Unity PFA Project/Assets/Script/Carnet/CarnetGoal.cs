﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarnetGoal : MonoBehaviour
{
    //public Vector3 scale;
    //public Vector2 heightWidth;
    //public Font font;
    //public int fontSize;
    public List<GoalKeys> goalList = new List<GoalKeys>();
    public List<GoalKeys> removeGoalList = new List<GoalKeys>();

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

    public void NewGoal(GoalKeys goalkeys)
    {
        if (!goalList.Contains(goalkeys))
        {
            goalList.Add(goalkeys);
            if(notif != null)
            {
                notif.textToNotify = LanguageManager.Instance.GetDialog(goalkeys.nameGoalKey);

            }
            else
            {
                notifSaver = LanguageManager.Instance.GetDialog(goalkeys.nameGoalKey);
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

    public void RemoveGoal(GoalKeys goalkeys)
    {
        if (!removeGoalList.Contains(goalkeys))
        {
            removeGoalList.Add(goalkeys);
            //if (!goalList.Contains(goalString))
            //{
            //    Debug.Log("Delete current Goal");
                goalList.Remove(goalkeys);
            //}
        }
    }

    void OnEnable()
    {
        foreach (GoalKeys goal in goalList)
        {

            GameObject newGoal = new GameObject("Goal", typeof (RectTransform));
            newGoal.transform.SetParent(transform);
            newGoal.GetComponent<RectTransform>().localPosition = Vector3.zero;
            newGoal.name = goal;
            newGoal.AddComponent<Text>();
            newGoal.GetComponent<RectTransform>().sizeDelta = heightWidth;
            newGoal.GetComponent<Text>().color = Color.black;
            newGoal.GetComponent<Text>().font = font;
            newGoal.GetComponent<Text>().text = goal;
            newGoal.GetComponent<Text>().horizontalOverflow =  HorizontalWrapMode.Overflow;
            newGoal.transform.localScale = scale;

            int counter = 0;
            foreach (Transform currentGoal in toCompleteGoals)
            {
                if(currentGoal.GetComponent<Goal>().ReturnGoal() ==  LanguageManager.Instance.GetDialog(goal.nameGoalKey))
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

        foreach (GoalKeys goal in removeGoalList)
        {
            int counter = 0;
            foreach (Transform currentGoal in completedGoals)
            {
                if (currentGoal.GetComponent<Goal>().ReturnGoal() == LanguageManager.Instance.GetDialog(goal.nameGoalKey))
                {
                    counter++;
                }

            }
            foreach (Transform finishedGoal in toCompleteGoals)
            {
                if (finishedGoal.GetComponent<Goal>().ReturnGoal() == LanguageManager.Instance.GetDialog(goal.nameGoalKey))
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

    void CreateGoal(bool isNewGoal, GoalKeys goal, Transform parent)
    {

        GameObject newGoal = Instantiate(goalObject, parent) as GameObject;
        newGoal.transform.SetParent(parent);
        newGoal.GetComponent<RectTransform>().localPosition = Vector3.zero;
        newGoal.name = goalObject.name;
        newGoal.GetComponent<Goal>().Init(goal.nameGoalKey, goal.descriptionGoalKey);
        newGoal.GetComponent<Goal>().goalDescriptionTransform = goalDescription;
        if (!isNewGoal)
        {
            newGoal.GetComponent<Goal>().ChangeColor(Color.gray);

        }
    }
}

[System.Serializable]
public class GoalKeys
{
    public string nameGoalKey;
    public string descriptionGoalKey;

    public GoalKeys(string newNameKey, string newDescriptionKey)
    {
        nameGoalKey = newNameKey;
        descriptionGoalKey = newDescriptionKey;
    }
} 