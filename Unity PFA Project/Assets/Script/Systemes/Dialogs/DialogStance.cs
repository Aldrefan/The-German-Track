using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogStance", menuName = "DialogStance")]
public class DialogStance : ScriptableObject
{
    [System.Serializable]
    public class StickerRedirection
    {
        public List<int> stickerGivenList;
        public List<int> redirectionList;
    }

    public EventRedirection eventRedirection;
    [System.Serializable]
    public class EventRedirection
    {
        public List<string> eventGivenList; //dans l'ordre inverse des priorités
        public List<int> redirectionEventList;
    }

    [System.Serializable]
    public class Quote
    {
        public List<DialogCapacity> listOfDialogs;
    }

    [System.Serializable]
    public class DialogCapacity
    {
        public List<ArrayOfQuotes> dialog;
        public bool canAskQuestions;
        public bool hasChoices;
        public bool endDialog;
        public List<TheGermanTrack.Button> buttonsList;
    }

    [System.Serializable]
    public class ArrayOfQuotes
    {
        public string quote;
        public string characterName;
        public bool isInLeftSide;
        public List<int> newStickerIndexList;
        public List<string> eventTrigger;
        public Sprite spriteCharacter;
    }
    
    string currentLine = "";
    float dialogDelay = 0.01f;
    public Quote allDialogs;
string actualQuote;
}
