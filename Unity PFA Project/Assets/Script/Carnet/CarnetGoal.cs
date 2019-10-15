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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            NewGoal("Fait quelque chose putain, pense à ta mère");
        }
    }

    public void NewGoal(string goalString)
    {
        GameObject newGoal = new GameObject("Goal", typeof (RectTransform));
        newGoal.transform.SetParent(transform);
        newGoal.GetComponent<RectTransform>().localPosition = Vector3.zero;
        newGoal.AddComponent<Text>();
        newGoal.GetComponent<RectTransform>().sizeDelta = heightWidth;
        newGoal.GetComponent<Text>().color = Color.black;
        newGoal.GetComponent<Text>().font = font;
        newGoal.GetComponent<Text>().text = goalString;
        newGoal.GetComponent<Text>().horizontalOverflow =  HorizontalWrapMode.Overflow;
        newGoal.transform.localScale = scale;
    }
}