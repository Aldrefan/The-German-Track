using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Goal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    string goalName;

    public string goalDescription;

    //[HideInInspector]
    public string goalDescriptionKey;

    [HideInInspector]
    public Transform goalDescriptionTransform;

    public void Init(string name)
    {
        goalName = name;
        this.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        this.transform.GetChild(0).GetComponent<Text>().horizontalOverflow =  HorizontalWrapMode.Wrap;
        this.transform.GetChild(0).GetComponent<Text>().text = name;

        if (goalDescriptionKey == "")
        {
            goalDescription = "Hello World !";
        }
        else
        {
            goalDescription = LanguageManager.Instance.GetDialog(goalDescriptionKey);
        }
    }

    public void ChangeColor(Color newColor)
    {
        this.GetComponent<Image>().color = newColor;
    }

    public string ReturnGoal()
    {
        return this.transform.GetChild(0).GetComponent<Text>().text;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("OnGoal");
        goalDescriptionTransform.GetChild(0).GetComponent<Text>().text = goalDescription;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("OutGoal");
        goalDescriptionTransform.GetChild(0).GetComponent<Text>().text = "";
    }
}
