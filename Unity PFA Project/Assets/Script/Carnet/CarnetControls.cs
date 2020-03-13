using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarnetControls : MonoBehaviour
{
    public GameObject player;
    public GameObject nextPageButton;
    public GameObject previousPageButton;

    [SerializeField]
    private Text hyp_Text;
    [SerializeField]
    private Text facts_Text;
    [SerializeField]
    private Text clues_Text;
    [SerializeField]
    private Text profils_Text;
    [SerializeField]
    private Text inv_Text;

    [SerializeField]
    private Transform texts;
    [SerializeField]
    private Transform buttons;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /*public void SaveCarnet()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        for(int i = 0; i < transform.GetChild(0).GetChild(i).childCount; i++)
        {
            for(int x = 0; x < transform.GetChild(0).GetChild(i).GetChild(x).childCount; x++)
            {
                if(save.carnetCharactersList.Contains(transform.GetChild(0).GetChild(i).GetChild(x).gameObject))
                {}
                else save.carnetCharactersList.Add(transform.GetChild(0).GetChild(i).GetChild(x).gameObject);
            }
        }

        for(int i = 0; i < transform.GetChild(1).GetChild(i).childCount; i++)
        {
            for(int x = 0; x < transform.GetChild(1).GetChild(i).GetChild(x).childCount; x++)
            {
                if(save.carnetHintsList.Contains(transform.GetChild(1).GetChild(i).GetChild(x).gameObject))
                {}
                else save.carnetHintsList.Add(transform.GetChild(1).GetChild(i).GetChild(x).gameObject);
            }
        }
        
        for(int i = 0; i < transform.GetChild(2).GetChild(i).childCount; i++)
        {
            for(int x = 0; x < transform.GetChild(2).GetChild(i).GetChild(x).childCount; x++)
            {
                if(save.carnetFactsList.Contains(transform.GetChild(2).GetChild(i).GetChild(x).gameObject))
                {}
                else save.carnetFactsList.Add(transform.GetChild(2).GetChild(i).GetChild(x).gameObject);
            }
        }

        for(int i = 0; i < transform.GetChild(3).GetChild(i).childCount; i++)
        {
            for(int x = 0; x < transform.GetChild(3).GetChild(i).GetChild(x).childCount; x++)
            {
                if(save.carnetGuessList.Contains(transform.GetChild(3).GetChild(i).GetChild(x).gameObject))
                {}
                else save.carnetGuessList.Add(transform.GetChild(3).GetChild(i).GetChild(x).gameObject);
            }
        }
        SaveGameManager.Save();
    }
 */
    public void NextPage()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeInHierarchy)
            {
                if(transform.GetChild(i).name == "Characters" || transform.GetChild(i).name == "Indices" || transform.GetChild(i).name == "Faits" || transform.GetChild(i).name == "Hypothèses")
                {
                    CarnetIndex carnetIndex = transform.GetChild(i).GetComponent<CarnetIndex>();
                    for(int p = 0; p < transform.GetChild(i).childCount; p++)
                    {
                        if(carnetIndex.pageList[p].gameObject.activeInHierarchy && p + 1 < carnetIndex.pageList.Count/*transform.GetChild(i).GetChild(p).gameObject.activeInHierarchy && p + 1 < transform.GetChild(i).childCount */)
                        {
                            carnetIndex.pageList[p].gameObject.SetActive(false);
                            carnetIndex.pageList[p + 1].gameObject.SetActive(true);
                            //transform.GetChild(i).GetChild(p).gameObject.SetActive(false);
                            //transform.GetChild(i).GetChild(p + 1).gameObject.SetActive(true);
                            return;
                        }
                    }
                }
            }
        }
    }

    public void PreviousPage()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeInHierarchy)
            {
                CarnetIndex carnetIndex = transform.GetChild(i).GetComponent<CarnetIndex>();
                for(int p = 0; p < transform.GetChild(i).childCount; p++)
                {
                    if(carnetIndex.pageList[p].gameObject.activeInHierarchy && p - 1 >= 0)
                    {
                        carnetIndex.pageList[p].gameObject.SetActive(false);
                        carnetIndex.pageList[p - 1].gameObject.SetActive(true);
                        //transform.GetChild(i).GetChild(p).gameObject.SetActive(false);
                        //transform.GetChild(i).GetChild(p - 1).gameObject.SetActive(true);
                        return;
                    }
                }
            }
        }
    }

    public void StickerType_Characters()
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        transform.GetChild(0).gameObject.SetActive(true); //characters
        transform.GetChild(1).gameObject.SetActive(false); //clues
        transform.GetChild(2).gameObject.SetActive(false); //facts
        transform.GetChild(3).gameObject.SetActive(false); //hypotheses
        transform.GetChild(4).gameObject.SetActive(false); //Goal
        transform.GetChild(5).gameObject.SetActive(false); //Controles
        //transform.GetChild(4).gameObject.SetActive(false); //saves
        //transform.GetChild(5).gameObject.SetActive(false);  //options
        if(transform.GetChild(0).childCount > 2)
        {
            previousPageButton.SetActive(true);
            nextPageButton.SetActive(true);
        }
        else
        {
            previousPageButton.SetActive(false);
            nextPageButton.SetActive(false);
        }
        player.GetComponent<PlayerMemory>().CheckStickersCarnet();
    }

    public void StickerType_Hints()
    {
        if (!transform.GetChild(1).gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        transform.GetChild(0).gameObject.SetActive(false); //characters
        transform.GetChild(1).gameObject.SetActive(true); //clues
        transform.GetChild(2).gameObject.SetActive(false); //facts
        transform.GetChild(3).gameObject.SetActive(false); //hypotheses
        transform.GetChild(4).gameObject.SetActive(false); //Goal
        transform.GetChild(5).gameObject.SetActive(false); //Controles
        //transform.GetChild(4).gameObject.SetActive(false); //saves
        //transform.GetChild(5).gameObject.SetActive(false);  //options
        if(transform.GetChild(1).childCount > 2)
        {
            previousPageButton.SetActive(true);
            nextPageButton.SetActive(true);
        }
        else
        {
            previousPageButton.SetActive(false);
            nextPageButton.SetActive(false);
        }
        player.GetComponent<PlayerMemory>().CheckStickersCarnet();
    }

    public void StickerType_Facts()
    {
        if (!transform.GetChild(2).gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        transform.GetChild(0).gameObject.SetActive(false); //characters
        transform.GetChild(1).gameObject.SetActive(false); //clues
        transform.GetChild(2).gameObject.SetActive(true); //facts
        transform.GetChild(3).gameObject.SetActive(false); //hypotheses
        transform.GetChild(4).gameObject.SetActive(false); //Goal
        transform.GetChild(5).gameObject.SetActive(false); //Controles
        //transform.GetChild(4).gameObject.SetActive(false); //saves
        //transform.GetChild(5).gameObject.SetActive(false);  //options
        if(transform.GetChild(2).childCount > 2)
        {
            previousPageButton.SetActive(true);
            nextPageButton.SetActive(true);
        }
        else
        {
            previousPageButton.SetActive(false);
            nextPageButton.SetActive(false);
        }
        player.GetComponent<PlayerMemory>().CheckStickersCarnet();
    }

    public void StickerType_Guess()
    {
        if (!transform.GetChild(3).gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        transform.GetChild(0).gameObject.SetActive(false); //characters
        transform.GetChild(1).gameObject.SetActive(false); //clues
        transform.GetChild(2).gameObject.SetActive(false); //facts
        transform.GetChild(3).gameObject.SetActive(true); //hypotheses
        transform.GetChild(4).gameObject.SetActive(false); //Goal
        transform.GetChild(5).gameObject.SetActive(false); //Controles
        //transform.GetChild(4).gameObject.SetActive(false); //saves
        //transform.GetChild(5).gameObject.SetActive(false);  //options
        if(transform.GetChild(3).childCount > 2)
        {
            previousPageButton.SetActive(true);
            nextPageButton.SetActive(true);
        }
        else
        {
            previousPageButton.SetActive(false);
            nextPageButton.SetActive(false);
        }
        player.GetComponent<PlayerMemory>().CheckStickersCarnet();
    }

    public void OpenInvestigation()
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        transform.GetChild(0).gameObject.SetActive(true); //characters
        transform.GetChild(1).gameObject.SetActive(false); //clues
        transform.GetChild(2).gameObject.SetActive(false); //facts
        transform.GetChild(3).gameObject.SetActive(false); //hypotheses
        transform.GetChild(4).gameObject.SetActive(false); //Goal
        transform.GetChild(5).gameObject.SetActive(false); //Controles
        texts.GetChild(0).gameObject.SetActive(true); //Name of the characters section
        texts.GetChild(1).gameObject.SetActive(true); //Name of the clues section
        texts.GetChild(2).gameObject.SetActive(true); //Name of the facts section
        texts.GetChild(3).gameObject.SetActive(true); //Name of the hypotheses section
        buttons.GetChild(2).gameObject.SetActive(true); //Button of the characters section
        buttons.GetChild(3).gameObject.SetActive(true); //Button of the clues section
        buttons.GetChild(4).gameObject.SetActive(true); //Button of the facts section
        buttons.GetChild(5).gameObject.SetActive(true); //Button of the hypotheses section
        //transform.GetChild(4).gameObject.SetActive(false); //saves
        //transform.GetChild(5).gameObject.SetActive(false);  //options
    }

    public void OpenGoals()
    {
        if (!transform.GetChild(4).gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        transform.GetChild(0).gameObject.SetActive(false); //characters
        transform.GetChild(1).gameObject.SetActive(false); //clues
        transform.GetChild(2).gameObject.SetActive(false); //facts
        transform.GetChild(3).gameObject.SetActive(false); //hypotheses
        transform.GetChild(4).gameObject.SetActive(true); //Goal
        transform.GetChild(5).gameObject.SetActive(false); //Controles
        texts.GetChild(0).gameObject.SetActive(false); //Name of the characters section
        texts.GetChild(1).gameObject.SetActive(false); //Name of the clues section
        texts.GetChild(2).gameObject.SetActive(false); //Name of the facts section
        texts.GetChild(3).gameObject.SetActive(false); //Name of the hypotheses section
        buttons.GetChild(2).gameObject.SetActive(false); //Button of the characters section
        buttons.GetChild(3).gameObject.SetActive(false); //Button of the clues section
        buttons.GetChild(4).gameObject.SetActive(false); //Button of the facts section
        buttons.GetChild(5).gameObject.SetActive(false); //Button of the hypotheses section
    }

    public void OpenSaves()
    {
        if (!transform.GetChild(4).gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        previousPageButton.SetActive(false);
        nextPageButton.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false); //characters
        transform.GetChild(1).gameObject.SetActive(false); //clues
        transform.GetChild(2).gameObject.SetActive(false); //facts
        transform.GetChild(3).gameObject.SetActive(false); //hypotheses
        transform.GetChild(4).gameObject.SetActive(true); //saves
        transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true); //options
        transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false); //feedbackSave
        transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false); //returntitle
        transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(false); //quitgame
        transform.GetChild(5).gameObject.SetActive(false);  //options
    }

    public void OpenControls()
    {
        if (!transform.GetChild(5).gameObject.activeSelf)
        {
            GameObject.Find("TypeSound").transform.GetComponent<AudioSource>().Play(0);
        }
        previousPageButton.SetActive(false);
        nextPageButton.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false); //characters
        transform.GetChild(1).gameObject.SetActive(false); //clues
        transform.GetChild(2).gameObject.SetActive(false); //facts
        transform.GetChild(3).gameObject.SetActive(false); //hypotheses
        transform.GetChild(4).gameObject.SetActive(false); //Goals
        transform.GetChild(5).gameObject.SetActive(true);  //Controles
        texts.GetChild(0).gameObject.SetActive(false); //Name of the characters section
        texts.GetChild(1).gameObject.SetActive(false); //Name of the clues section
        texts.GetChild(2).gameObject.SetActive(false); //Name of the facts section
        texts.GetChild(3).gameObject.SetActive(false); //Name of the hypotheses section
        buttons.GetChild(2).gameObject.SetActive(false); //Button of the characters section
        buttons.GetChild(3).gameObject.SetActive(false); //Button of the clues section
        buttons.GetChild(4).gameObject.SetActive(false); //Button of the facts section
        buttons.GetChild(5).gameObject.SetActive(false); //Button of the hypotheses section
    }
}
