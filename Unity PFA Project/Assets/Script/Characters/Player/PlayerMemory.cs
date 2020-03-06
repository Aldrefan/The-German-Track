using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemory : MonoBehaviour
{
    public List<int> stickerIndexCarnetList;
    public List<int> stickerIndexBoardList;
    public List<int> allStickers;
    //Transform carnet;
    public List<Vector3> stickersPositionBoard;

    [SerializeField]
    private string boardCanvasName;

    Transform boardCanvas;
    [SerializeField]
    string newStickerName;
    GameObject newSticker;
    
    [SerializeField]
    private string carnetCanvasName;

    Transform carnet;
    Vector2 offsets = new Vector2(5, 5);
    public GameObject stickerTemplate;
    public List<Sticker> stickersScriptableList;

    void Awake()
    {
        //carnet = GameObject.Find("FlottingCanvas").transform.GetChild(3).GetChild(0).transform;// Initial
        //newSticker = GameObject.Find("FlottingCanvas").transform.GetChild(GameObject.Find("FlottingCanvas").transform.childCount - 5).gameObject;// Initial
        
    }

    // Start is called before the first frame update
    void Start()
    {
        boardCanvas = CanvasManager.CManager.GetCanvas(boardCanvasName).transform;
        carnet = CanvasManager.CManager.GetCanvas(carnetCanvasName).transform;
        newSticker = CanvasManager.CManager.GetCanvas(newStickerName);
        //JsonSave save = SaveGameManager.GetCurrentSave();
        //for(int i = 0; i < save.memoryStickers.Count; i++)
        //{
            //AddToMemory(save.memoryStickers[i]);
        //}
    }

    public void AddToMemory(int stickerIndex)
    {
        newSticker.GetComponent<Animator>().SetTrigger("NewSticker");
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
        allStickers.Add(stickerIndex);
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
                GameObject child = Instantiate(stickerTemplate, CanvasManager.CManager.GetCanvas("Carnet").transform.GetChild(0));
                child.GetComponent<Sticker_Display>().sticker = stickersScriptableList[newStickerIndex];
                switch(stickersScriptableList[stickerIndexCarnetList[i]].type)
                {
                    case Sticker.Type.Profile:
                    child.GetComponent<StickerManager>().OnCarnet();
                    carnet.transform.GetChild(0).GetComponent<CarnetIndex>().CheckChildNumber(child);
                    break;

                    case Sticker.Type.Clue:
                    child.GetComponent<StickerManager>().OnCarnet();
                    carnet.transform.GetChild(1).GetComponent<CarnetIndex>().CheckChildNumber(child);
                    break;

                    case Sticker.Type.Fact:
                    child.GetComponent<StickerManager>().OnCarnet();
                    carnet.transform.GetChild(2).GetComponent<CarnetIndex>().CheckChildNumber(child);
                    break;

                    case Sticker.Type.Hypothesis:
                    child.GetComponent<StickerManager>().OnCarnet();
                    carnet.transform.GetChild(3).GetComponent<CarnetIndex>().CheckChildNumber(child);
                    break;
                }
            }
            stickerIndexCarnetList.Clear();
        }
    }

    public void CheckStickersBoard()
    {
        if(stickerIndexBoardList.Count > 0)
        {
            int numberOfStickersToAdd = stickerIndexBoardList.Count;
            //JsonSave save = SaveGameManager.GetCurrentSave();
            for(int i = 0; i < numberOfStickersToAdd; i++)
            {
                int newStickerIndex = stickerIndexBoardList[0];
                stickerIndexBoardList.RemoveAt(0);
                //if(save.stickersIndexOnBoard.Contains(newStickerIndex))
                if(!stickerIndexBoardList.Contains(newStickerIndex))
                {
                    GameObject sticker = Instantiate(stickerTemplate, boardCanvas);
                    sticker.GetComponent<Sticker_Display>().sticker = stickersScriptableList[newStickerIndex];
                    //Instantiate(stickerList[newStickerIndex], boardCanvas);
                    //sticker.transform.localPosition = save.stickersPositionOnBoard[x];
                    //sticker.transform.localPosition = stickersPositionBoard[x];
                    ////sticker.GetComponent<RectTransform>().localPosition = new Vector2(0 + offsets.x * i, -100 + offsets.y * i);////
                    ////sticker.transform.localPosition = new Vector2(0 + offsets.x * i, -100 + offsets.y * i);////
                    CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().CheckTypeAndSort(sticker);
                    sticker.GetComponent<StickerManager>().OnBoard();
                }
                /*if(stickerIndexBoardList.Contains(newStickerIndex))
                {
                    //for(int x = 0; x < stickersIndexOnBoard.Count; x++)
                    for(int x = i; x < stickerIndexBoardList.Count; x++)
                    {
                        //if(save.stickersIndexOnBoard[x] == newStickerIndex)
                        if(stickerIndexBoardList[x] == newStickerIndex)
                        {
                            GameObject sticker = Instantiate(stickerTemplate, boardCanvas);
                            sticker.GetComponent<Sticker_Display>().sticker = stickersScriptableList[newStickerIndex];
                            //Instantiate(stickerList[newStickerIndex], boardCanvas);
                            //sticker.transform.localPosition = save.stickersPositionOnBoard[x];
                            //sticker.transform.localPosition = stickersPositionBoard[x];
                            sticker.GetComponent<RectTransform>().localPosition = new Vector2(0 + offsets.x * i, -100 + offsets.y * i);
                            sticker.transform.localPosition = new Vector2(0 + offsets.x * i, -100 + offsets.y * i);
                            sticker.GetComponent<StickerManager>().OnBoard();
                        }
                    }
                }
                else
                {
                    GameObject newSticker = Instantiate(stickerTemplate, boardCanvas);
                    newSticker.GetComponent<Sticker_Display>().sticker = stickersScriptableList[newStickerIndex];
                    newSticker.GetComponent<RectTransform>().localPosition = new Vector2(0 + offsets.x * i, -100 + offsets.y * i);
                    newSticker.transform.localPosition = new Vector2(0 + offsets.x * i, -100 + offsets.y * i);
                    //Debug.Log(newSticker.name + " " + "Position : " + newSticker.GetComponent<RectTransform>().localPosition);
                    //Debug.Log(new Vector2(0 + offsets.x * i, -100 + offsets.y * i));
                    newSticker.GetComponent<StickerManager>().OnBoard();
                }*/
            }
            //stickerIndexBoardList.Clear();   // a vérifier
        }
        //CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<String_Manager>().CheckHypotheses();   // a vérifier
    }
}