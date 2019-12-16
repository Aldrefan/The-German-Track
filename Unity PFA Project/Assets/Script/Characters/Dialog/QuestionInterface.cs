using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionInterface : MonoBehaviour
{
    public GameObject buttonBase;
    public void InstantiateQuestions(List<Button> buttonsList)
    {
        foreach(Button button in buttonsList)
        {
            if(button.eventNeeded == "")
            {
                GameObject newButton = Instantiate(buttonBase, transform.GetChild(1));
                newButton.GetComponent<Buttons_Display>().button = button;
                newButton.GetComponent<Buttons_Display>().SetInformations();
            }
            else if(GameObject.FindObjectOfType<ActiveCharacterScript>().actualCharacter.GetComponent<EventsCheck>().eventsList.Contains(button.eventNeeded))
            {
                GameObject newButton = Instantiate(buttonBase, transform.GetChild(1));
                newButton.GetComponent<Buttons_Display>().button = button;
                newButton.GetComponent<Buttons_Display>().SetInformations();
            }
        }
    }

    void OnDisable()
    {
        foreach(Transform child in transform.GetChild(1))
        {
            Destroy(child.gameObject);
        }
    }
}
