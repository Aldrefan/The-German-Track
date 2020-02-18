using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BalisesManager : MonoBehaviour
{
    public static BalisesManager Balises;

    private List<string> balisesList = new List<string>{"<b>", "</b>", "<i>", "</i>", "<size=50>", "</size>", "<color=#FFFFFF00>", "</color>", "<color=red>", "</color>", "<size=10>", "<color=green>", "<color=silver>", "<color=blue>", "<color=yellow>", "<s>", "</s>"};
    string newBalise;
    bool isEven;

    void Awake()
    {
        Balises = this;
    }
    
    public string SetTo(string sentBalise)
    {
        switch(sentBalise)
        {
            case "<b>":
            newBalise = "</b>";
            break;

            case "<i>":
            newBalise = "</i>";
            break;

            case "<size=50>":
            newBalise = "</size>";
            break;

            default:
            break;
        }
        return newBalise;
    }

    public bool IsABalise(string sentBalise)
    {
        if(balisesList.Contains(sentBalise))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BaliseState(string sentBalise)
    {
        for(int i = 0; i < balisesList.Count; i++)
        {
            if(balisesList[i] == sentBalise)
            {
                isEven = i % 2 == 0;
            }
        }
        return isEven;
    }

    public int GetClosingBalise(string[] list, string balise)
    {
        string baliseToFind = SetTo(balise);
        int index = 0;
        for(int i = 0; i < list.Length; i++)
        {
            if(list[i] == baliseToFind)
            {
                index = i;
            }
        }
        return index;
    }
}
