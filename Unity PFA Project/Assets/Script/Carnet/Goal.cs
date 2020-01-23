using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Goal : MonoBehaviour, IPointerEnterHandler
{
    string goalName;
    string goalDescription;

    public void Init(string name)
    {
        goalName = name;
        this.transform.GetChild(0).GetComponent<Text>().horizontalOverflow =  HorizontalWrapMode.Overflow;
        this.transform.GetChild(0).GetComponent<Text>().text = name;

    }

    public void ChangeColor(Color newColor)
    {
        this.GetComponent<Image>().color = newColor;
    }

    public string ReturnGoal()
    {
        return this.transform.GetChild(0).GetComponent<Text>().text;
    }

    public void OnPointerEnter (PointerEventData pointerEventData)
    {

    }
}
