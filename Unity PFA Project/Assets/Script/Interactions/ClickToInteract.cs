using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToInteract : MonoBehaviour
{
    [SerializeField]
    bool startCinematic;
    void OnMouseDown()
    {
        
    }

    public void DoAction()
    {
        if(startCinematic)
        {
            transform.parent.GetComponent<Clara_Cinematic>().ExecuteCommand();
        }
        else
        {
            if(ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<Interactions>().state == Interactions.State.Normal)
            {
                if(transform.parent.tag == "Interaction" || transform.parent.tag == "PNJinteractible" || transform.parent.tag == "Item")
                {
                    ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<Interactions>().CheckOpenDialog();
                }
                else if(transform.parent.tag == "Board")
                {
                    ActiveCharacterScript.ActiveCharacter.actualCharacter.GetComponent<Interactions>().OpenBoardExe();
                }
                else if(transform.parent.tag == "Shortcut" || transform.parent.parent.tag == "Shortcut")
                {
                    if (transform.parent.parent.GetComponent<Shortcut>())
                    {
                        transform.parent.parent.GetComponent<Shortcut>().Teleport();
                    }
                    if (transform.parent.parent.GetComponent<RoomInformations>())
                    {
                        transform.parent.parent.GetComponent<RoomInformations>().Teleport(GameObject.FindObjectOfType<Interactions>().PNJContact.name);

                    }
                }
            }
        }
    }
}
