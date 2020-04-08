using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class DialogMaker : MonoBehaviour
{
    [SerializeField]
    private string destinationFolder;

    [SerializeField]
    string characterNameReference;

    [SerializeField]
    string realCharacterName;
    
    [SerializeField]
    bool haveEvent;

    [System.Serializable]
    public class DialogsInformations
    {
        public string dialogName;
        public int stickerIndexToGive;
        public List<CharactersInDialog> charactersInDialogs_frNames = new List<CharactersInDialog>();
    }

    [System.Serializable]
    public class CharactersInDialog
    {
        public string characterName;
        public bool left;
    }

    [SerializeField]
    List<DialogsInformations> dialogList = new List<DialogsInformations>();

    [System.Serializable]
    public class EventPositionAndName
    {
        public string position;
        public List<string> eventsNames = new List<string>();
    }

    [SerializeField]
    List<EventPositionAndName> eventPositionsInDialog = new List<EventPositionAndName>();

    [SerializeField]
    List<StickersPositionAndName> stickersPositionsInDialogs = new List<StickersPositionAndName>();

    [System.Serializable]
    public class StickersPositionAndName
    {
        public string position;
        public List<int> stickerIndexs = new List<int>();
    }
    PNJ newComponent;
    
    List<PNJ.DialogCapacity> cap = new List<PNJ.DialogCapacity>();
    PNJ.Quote quotelist = new PNJ.Quote();
    PNJ.StickerRedirection sRList = new PNJ.StickerRedirection();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tamere());
    }

    // Update is called once per frame
    void Initialisation()
    {
        newComponent = gameObject.AddComponent<PNJ>();
        newComponent.haveEvent = haveEvent;
        newComponent.PNJName = realCharacterName;
        newComponent.negativeQuote = 3;
        newComponent.transitionQuote = 2;
        /*PNJ.StickerRedirection sRList = new PNJ.StickerRedirection();
        PNJ.EventRedirection eRList = new PNJ.EventRedirection();
        for(int i = 0; i < stickerRedirection.redirectionList.Count; i++)
        {
            sRList.stickerGivenList.Add(stickerRedirection.stickerGivenList[i]);
            sRList.redirectionList.Add(stickerRedirection.redirectionList[i]);
        }
        newComponent.stickerRedirection = sRList;*/
        //Debug.Log(characterNameReference + "_" + firstDialogKey);
        InitializeDialogs();
    }

    IEnumerator tamere()
    {
        yield return new WaitForSeconds(0.5f);
        Initialisation();
    }

    void InitializeDialogs()
    {
        for(int n = 0; n < dialogList.Count; n++)
        {
            //List<string> characterList = new List<string>();
            int chiasse = LanguageManager.Instance.GetDialogPosition(characterNameReference + "_" + dialogList[n].dialogName);
            int length = LanguageManager.Instance.DialogQuoteLength(chiasse);
            //characterList = LanguageManager.Instance.GetListOfCharacters(chiasse, length);
            List<PNJ.ArrayOfQuotes> dialog = new List<PNJ.ArrayOfQuotes>();
            for(int i = 0; i < length; i++)
            {
                dialog.Add(new PNJ.ArrayOfQuotes{quote = LanguageManager.Instance.GetDialogKeyfromIndex(chiasse + i), isInLeftSide = false, newStickerIndexList = new List<int>(), eventTrigger = new List<string>()});
                if(stickersPositionsInDialogs.Any(x => (x.position == dialog[i].quote)))
                {
                    for(int x = 0; x < NbrOfStickersToAdd(dialog[i].quote); x++)
                    {dialog[i].newStickerIndexList.Add(stickersPositionsInDialogs.Find(w => (w.position == LanguageManager.Instance.GetDialogKeyfromIndex(chiasse + i))).stickerIndexs[x]);}
                }
                if(eventPositionsInDialog.Any(x => (x.position == dialog[i].quote)))
                {
                    for(int x = 0; x < NbrOfEventsToAdd(dialog[i].quote); x++)
                    {dialog[i].eventTrigger.Add(eventPositionsInDialog.Find(w=> (w.position == LanguageManager.Instance.GetDialogKeyfromIndex(chiasse + i))).eventsNames[x]);}
                }
                dialog[i].isInLeftSide = dialogList[n].charactersInDialogs_frNames.Find(w => (w.characterName == LanguageManager.Instance.GetNameOfTheSpeaker(dialog[i].quote))).left;
            }
            cap.Add(new PNJ.DialogCapacity{dialog = dialog, canAskQuestions = false, hasChoices = false, endDialog = false, buttonsList = new List<TheGermanTrack.Button>()});
            //Debug.Log(cap.Count);
            quotelist.listOfDialogs.Add(cap[n]);
            //Debug.Log(cap[0].dialog.Count);
            newComponent.allDialogs = quotelist;
            if(dialogList[n].stickerIndexToGive != 0)
            {
                sRList.stickerGivenList.Add(dialogList[n].stickerIndexToGive);
                sRList.redirectionList.Add(n);
            }
            newComponent.stickerRedirection = sRList;
        }
    }

    int NbrOfStickersToAdd(string positionT)
    {
        return stickersPositionsInDialogs.Find(x => (x.position == positionT)).stickerIndexs.Count;
    }

    int NbrOfEventsToAdd(string positionT)
    {
        return eventPositionsInDialog.Find(x => (x.position == positionT)).eventsNames.Count;
    }
}
