using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Goal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    string goalDescription;
    string descKey;

    [HideInInspector]
    public string nameKey;

    [HideInInspector]
    public Transform goalDescriptionTransform;

    public void Init(string newNameKey, string newDescKey)
    {
        nameKey = newNameKey;
        descKey = newDescKey;

        this.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        this.transform.GetChild(0).GetComponent<Text>().horizontalOverflow =  HorizontalWrapMode.Wrap;

        if (LanguageManager.Instance.GetDialog(newNameKey) == "")
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "Goal";
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Text>().text = LanguageManager.Instance.GetDialog(newNameKey);
        }

        if (LanguageManager.Instance.GetDialog(newDescKey) == "")
        {
            goalDescription = "Hello World !";
        }
        else
        {
            goalDescription = LanguageManager.Instance.GetDialog(newDescKey);
        }
    }

    void Update()
    {
        CheckLanguage();
    }

    void CheckLanguage()
    {
        if(this.transform.GetChild(0).GetComponent<Text>().text != LanguageManager.Instance.GetDialog(nameKey))
        {
            Init(nameKey, descKey);
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
