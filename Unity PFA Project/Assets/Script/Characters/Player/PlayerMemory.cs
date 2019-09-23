using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemory : MonoBehaviour
{
    public List<int> stickerIndexCarnetList;
    public List<int> stickerIndexBoardList;
    public List<int> allStickers;
    Transform carnet;
    public List<GameObject> stickerList;
    public Transform boardCanvas;
    public Vector2Int charactersRange;
    public Vector2Int indicesRange;
    public Vector2Int hypothesesRange;
    public Vector2Int faitsRange;
    public GameObject newSticker;

    void Awake()
    {
        carnet = GameObject.Find("FlottingCanvas").transform.GetChild(3).GetChild(0).transform;
        newSticker = GameObject.Find("FlottingCanvas").transform.GetChild(GameObject.Find("FlottingCanvas").transform.childCount - 5).gameObject;
        JsonSave save = SaveGameManager.GetCurrentSave();
        for(int i = 0; i < save.memoryStickers.Count; i++)
        {
            AddToMemory(save.memoryStickers[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddToMemory(int stickerIndex)
    {
        stickerIndexBoardList.Add(stickerIndex);
        stickerIndexCarnetList.Add(stickerIndex);
        allStickers.Add(stickerIndex);
    }

    public void KeepInMemory(int stickerIndex)
    {
        //JsonSave save = SaveGameManager.GetCurrentSave();
        newSticker.GetComponent<Animator>().SetTrigger("NewSticker");
        stickerIndexBoardList.Add(stickerIndex);
        stickerIndexCarnetList.Add(stickerIndex);
        /*if(save.memoryStickers.Contains(stickerIndex))
        {}
        else save.memoryStickers.Add(stickerIndex); */
        //SaveGameManager.Save();
    }

    public void CheckStickersCarnet()
    {
        if(stickerIndexCarnetList.Count > 0)
        {
            for(int i = 0; i < stickerIndexCarnetList.Count; i++)
            {
                int newStickerIndex = stickerIndexCarnetList[i];
                if(newStickerIndex >= charactersRange.x && newStickerIndex <= charactersRange.y)
                {
                    GameObject child = Instantiate(stickerList[newStickerIndex]);
                    child.GetComponent<StickerManager>().OnCarnet();
                    carnet.transform.GetChild(0).GetComponent<CarnetIndex>().CheckChildNumber(child);
                }
                if(newStickerIndex >= indicesRange.x && newStickerIndex <= indicesRange.y)
                {
                    GameObject child = Instantiate(stickerList[newStickerIndex], Vector3.zero, carnet.rotation, carnet.GetChild(1));
                    child.GetComponent<StickerManager>().OnCarnet();
                    carnet.transform.GetChild(1).GetComponent<CarnetIndex>().CheckChildNumber(child);
                    //stickerIndexCarnetList.RemoveAt(i);
                }
                if(newStickerIndex >= faitsRange.x && newStickerIndex <= faitsRange.y)
                {
                    GameObject child = Instantiate(stickerList[newStickerIndex], Vector3.zero, carnet.rotation, carnet.GetChild(2));
                    child.GetComponent<StickerManager>().OnCarnet();
                    carnet.transform.GetChild(2).GetComponent<CarnetIndex>().CheckChildNumber(child);
                }
                if(newStickerIndex >= hypothesesRange.x && newStickerIndex <= hypothesesRange.y)
                {
                    GameObject child = Instantiate(stickerList[newStickerIndex], Vector3.zero, carnet.rotation, carnet.GetChild(3));
                    child.GetComponent<StickerManager>().OnCarnet();
                    carnet.transform.GetChild(3).GetComponent<CarnetIndex>().CheckChildNumber(child);
                }
            }
            stickerIndexCarnetList.Clear();
        }
    }

    public void CheckStickersBoard()
    {
        if(stickerIndexBoardList.Count > 0)
        {
            JsonSave save = SaveGameManager.GetCurrentSave();
            for(int i = 0; i < stickerIndexBoardList.Count; i++)
            {
                int newStickerIndex = stickerIndexBoardList[i];
                if(save.stickersIndexOnBoard.Contains(newStickerIndex))
                {
                    for(int x = 0; x < save.stickersIndexOnBoard.Count; x++)
                    {
                        if(save.stickersIndexOnBoard[x] == newStickerIndex)
                        {
                            GameObject sticker = Instantiate(stickerList[newStickerIndex], boardCanvas);
                            sticker.transform.localPosition = save.stickersPositionOnBoard[x];
                            sticker.GetComponent<StickerManager>().OnBoard();
                        }
                    }
                }
                else
                {
                    GameObject newSticker = Instantiate(stickerList[newStickerIndex], boardCanvas);
                    newSticker.transform.localPosition = new Vector3(0, -100, -10);
                    newSticker.GetComponent<StickerManager>().OnBoard();
                }
            }
            stickerIndexBoardList.Clear();
        }
    }
}