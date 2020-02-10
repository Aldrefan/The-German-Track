using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StickersGivenToPNJ : MonoBehaviour
{
    public static StickersGivenToPNJ SGTP;

    public List<PNJMemory> PnjsInGame;
    
    [Serializable]
    public class PNJMemory
    {
        public string name;
        public List<int> stickersGiven;
    }

    void Awake()
    {
        SGTP = this;
    }

    bool contain = false;

    public void AddStickerInAList(string nameOfThePNJ, int index)
    {
        PNJMemory target = PnjsInGame.Find(x => x.name == nameOfThePNJ);
        if(target != null)
        {
            if(!target.stickersGiven.Contains(index))
            {
                target.stickersGiven.Add(index);
            }
        }
        else 
        {
            target = new PNJMemory{name = nameOfThePNJ, stickersGiven = new List<int>()};
            target.name = nameOfThePNJ;
            target.stickersGiven.Add(index);
            PnjsInGame.Add(target);
        }
    }

    public bool CheckStickerInList(string nameOfThePNJ, int index)
    {
        contain = false;
        foreach(PNJMemory pnj in PnjsInGame)
        {
            if(pnj.name == nameOfThePNJ)
            {
                if(pnj.stickersGiven.Contains(index))
                {
                    contain = true;
                    break;
                }
                break;
            }
        }
        return contain;
    }
}
