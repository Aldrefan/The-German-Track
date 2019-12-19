using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarnetGoal : MonoBehaviour
{
    public Vector3 scale;
    public Font font;
    public Vector2 heightWidth;
    public int fontSize;
    public List<string> goalList;
    public List<string> removeGoalList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGoal(string goalString)
    {
        goalList.Add(goalString);
    }

    public void RemoveGoal(string goalString)
    {
        removeGoalList.Add(goalString);
    }

    void OnEnable()
    {
        foreach (string goal in goalList)
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
        }
        goalList.Clear();

        foreach (string goal in removeGoalList)
        {
            foreach (Transform finishedGoal in transform)
            {
                if(finishedGoal.name == goal)
                {
                    Destroy(finishedGoal.gameObject);
                }
            }
        }
        removeGoalList.Clear();
    }
}