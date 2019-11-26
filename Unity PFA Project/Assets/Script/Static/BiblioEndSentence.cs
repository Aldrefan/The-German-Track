using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class BiblioEndSentence 
{
    public static List<NewSentence> TextBiblio = new List<NewSentence>();

    public static string SelectSentence(string slctKeyWord)
    {
        foreach(NewSentence Text in TextBiblio)
        {
            if (Text.keyWord == slctKeyWord)
            {
                return Text.fullSentence;
            }
            return null;
        }
        return null;
    }

}

public class NewSentence
{
    public string keyWord;
    public string fullSentence;

    public NewSentence(string newKeyWord, string newSentence)
    {
        keyWord = newKeyWord;
        fullSentence = newSentence;
    }
}
