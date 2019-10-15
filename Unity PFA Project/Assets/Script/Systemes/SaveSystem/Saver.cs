using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saver : MonoBehaviour
{
    public int levelFM;
    public int lieuFM;
    public List<int> stickersIndexOnBoardFM;
    public List<Vector3> stickersPositionOnBoardFM;
    public List<int> memoryStickersFM;
    public List<string> meetingListFM;
    public GameObject carnet;
    public GameObject boardCanvas;
    public List<string> eventListFM;
    public List<string> caracters;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            MakeASave();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            JsonSave save = SaveGameManager.GetCurrentSave();
            SaveGameManager.DeleteAllSaves();
        }
    }

    public void MakeASave()
    {
        if(caracters.Contains("Kenneth"))
        {KennethSave();}
        if(caracters.Contains("Leon"))
        {LeonSave();}
        if(caracters.Contains("Garance"))
        {GaranceSave();}
    }

    void KennethSave()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        save.level = SceneManager.GetActiveScene().buildIndex;
        save.lieu = lieuFM;
        for(int i = 0; i < meetingListFM.Count; i++)
        {
            if(save.meetingList.Contains(meetingListFM[i]))
            {}
            else save.meetingList.Add(meetingListFM[i]);
        }
        for(int i = 0; i < player.GetComponent<PlayerMemory>().allStickers.Count; i++)
        {
            if(save.memoryStickers.Contains(player.GetComponent<PlayerMemory>().allStickers[i]))
            {}
            else save.memoryStickers.Add(player.GetComponent<PlayerMemory>().allStickers[i]);
        }
        for(int i = 0; i < eventListFM.Count; i++)
        {
            if(save.eventList.Contains(eventListFM[i]))
            {}
            else save.eventList.Add(eventListFM[i]);
        }
        /*for(int i = 0; i < 4; i++)
        {
            if(carnet.transform.GetChild(i).childCount > 1)
            {
                for(int x = 1; x < carnet.transform.GetChild(i).childCount; x++)
                {
                    for(int y = 0; y < carnet.transform.GetChild(i).GetChild(x).childCount; y++)
                    {
                        if(save.carnetStickersList.Contains(carnet.transform.GetChild(i).GetChild(x).GetChild(y).GetComponent<CarnetSticker>().stickerIndex))
                        {}
                        else save.carnetStickersList.Add(carnet.transform.GetChild(i).GetChild(x).GetChild(y).GetComponent<CarnetSticker>().stickerIndex);
                    }
                }
            }
        } */
        SaveGameManager.Save();
    }


    void LeonSave()
    {

    }

    void GaranceSave()
    {

    }
    
    public void ClearSave()
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        //save.carnetStickersList.Clear();
        save.level = 1;
        save.memoryStickers.Clear();
        save.stickersIndexOnBoard.Clear();
        save.stickersPositionOnBoard.Clear();
        save.eventList.Clear();
        SaveGameManager.Save();
    }
}
