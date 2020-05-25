using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChanger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueComponent
    {
        public string modificationName;
        public GameObject characterToModify;
        public PNJ newComponent;
    }
    [SerializeField] List<DialogueComponent> dialogues;

    public static DialogueChanger DialChangr;

    void Awake()
    {
        DialChangr = this;
    }

    public void ChangeDialogueComponent(string modifRef)
    {
        DialogueComponent DC = dialogues.Find(x => x.modificationName == modifRef);
        //Destroy(DC.characterToModify.GetComponent<PNJ>());
        //PNJ pnjComponent = DC.newComponent;
        PNJ oldComponent = DC.characterToModify.GetComponent<PNJ>();
        oldComponent.transitionQuote = DC.newComponent.transitionQuote;
        oldComponent.negativeQuote = DC.newComponent.negativeQuote;
        oldComponent.allDialogs = DC.newComponent.allDialogs;
        oldComponent.stickerRedirection = DC.newComponent.stickerRedirection;
        oldComponent.eventRedirection = DC.newComponent.eventRedirection;
        oldComponent.haveEvent = DC.newComponent.haveEvent;
        oldComponent.stickerAlreadyGivenList = DC.newComponent.stickerAlreadyGivenList;
        oldComponent.stickersAlreadyGiven = DC.newComponent.stickersAlreadyGiven;
    }
}
