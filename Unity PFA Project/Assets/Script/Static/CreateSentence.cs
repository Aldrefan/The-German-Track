using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSentence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateNewText("1", "HelloWorld");
        Debug.Log(BiblioEndSentence.SelectSentence("2"));
    }

    void CreateNewText(string KeyWord, string Text)
    {
        int Counter = BiblioEndSentence.TextBiblio.Count;
        for(int i=0; i< BiblioEndSentence.TextBiblio.Count; i++)
        {
            if(BiblioEndSentence.TextBiblio[i].keyWord != KeyWord)
            {
                Counter--;

            }
        }
        if (Counter == 0)
        {
            BiblioEndSentence.TextBiblio.Add(new NewSentence(KeyWord, Text));

        }
    }
}

