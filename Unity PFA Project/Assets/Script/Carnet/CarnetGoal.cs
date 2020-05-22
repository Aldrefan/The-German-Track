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
    public List<GoalKeys> goalList = new List<GoalKeys>();
    public List<GoalKeys> removeGoalList = new List<GoalKeys>();

    public Transform completedGoals;
    public Transform toCompleteGoals;
    public Transform goalDescription;
    public Object goalObject;

    public ObjectiveNotif notif;
    public string notifSaver;
    public bool notifState;

    public int goalToDo;
    public int goalDone;

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
            goalToDo++;
            goalList.Add(goalkeys);
            if(notif != null)
            {
                notif.NotifQueue.Add(new StringToNotif(LanguageManager.Instance.GetDialog(goalkeys.nameGoalKey), false));

            }
            else
            {
                notifSaver = LanguageManager.Instance.GetDialog(goalkeys.nameGoalKey);
                notifState = false;
            }
        }
    }
    
    public void RetryNotif()
    {
        if (notif != null && notifSaver != null)
        {
            notif.NotifQueue.Add(new StringToNotif(LanguageManager.Instance.GetDialog(notifSaver), notifState));
            notifSaver = null;
            notifState = false;

        }
    }

    public void RemoveGoal(GoalKeys goalkeys)
    {
        if (!removeGoalList.Contains(goalkeys))
        {
            goalToDo--;
            goalDone++;

            removeGoalList.Add(goalkeys);
            //if (!goalList.Contains(goalString))
            //{
            //    Debug.Log("Delete current Goal");

            //goalList.Remove(goalkeys);
            //}
            if (notif != null)
            {
                notif.NotifQueue.Add(new StringToNotif(LanguageManager.Instance.GetDialog(goalkeys.nameGoalKey), true));

            }
            else
            {
                notifSaver = LanguageManager.Instance.GetDialog(goalkeys.nameGoalKey);
                notifState = true;
            }

        }
    }

    void OnEnable()
    {

        if (goalList.Count != goalToDo)
        {
            GoalCheck();
        }

        foreach (GoalKeys goal in goalList)
        {
            int counter = 0;
            foreach (Transform currentGoal in toCompleteGoals)
            {
                if(currentGoal.GetComponent<Goal>().nameKey ==  goal.nameGoalKey)
                {
                    counter++;
                }
                
            }
            if (counter ==0) 
            {
                CreateGoal(true,goal, toCompleteGoals);
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
                if (currentGoal.GetComponent<Goal>().nameKey == goal.nameGoalKey)
                {
                    counter++;
                }

            }
            foreach (Transform finishedGoal in toCompleteGoals)
            {
                if (finishedGoal.GetComponent<Goal>().nameKey == goal.nameGoalKey)
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

    void GoalCheck()
    {
        List<GoalKeys> goalToRemove = new List<GoalKeys>();
        foreach (GoalKeys finishedGoal in removeGoalList)
        {
            foreach (GoalKeys goal in goalList)
            {

                if (goal.nameGoalKey == finishedGoal.nameGoalKey)
                {
                    goalToRemove.Add(goal);
                }
            }
        }
        foreach (GoalKeys goalEnded in goalToRemove)
        {
            goalList.Remove(goalEnded);
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