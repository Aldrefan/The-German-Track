using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons_Display : MonoBehaviour
{
    public TheGermanTrack.Button button;
    public Text text;
    public int dialogIndex;

    // Start is called before the first frame update
    void Start()
    {
        SetInformations();
    }

    public void SetInformations()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        if(save.language == "french")
        {
            text.text = button.frenchText;
        }
        else text.text = button.englishText;
        dialogIndex = button.index;
    }

    public void Ribery()
    {
        GameObject.Find("Leon").GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().ChangeDialog(dialogIndex);
        transform.parent.parent.gameObject.SetActive(false);
    }
}
