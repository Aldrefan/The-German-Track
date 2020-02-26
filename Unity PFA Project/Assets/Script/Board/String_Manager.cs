using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class String_Manager : MonoBehaviour
{
public List<GameObject> pinList = new List<GameObject>();
LineRenderer lineRenderer;
bool checking = false;
public int finalDemoHypothese;
public List<int> validateList;
int index = 0;
int microIndex = 0;
public List<int> hypotheseresponses;
bool finished;
public List<string> quoteList;
bool createASticker;

[System.Serializable]
public class Hypotheses
{
    public List<GameObject> list;
}

[System.Serializable]
public class HypotheseList
{
    public List<Hypotheses> list;
}

public HypotheseListT ListOfHypLists = new HypotheseListT();

[System.Serializable]
public class HypothesesT
{
    public List<int> list;
}

[System.Serializable]
public class HypotheseListT
{
    public List<HypothesesT> list;
}

public GameObject player;
public GameObject stickerTemplate;
bool thereIsAProfile = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pinList.Count > 0)
        {
            lineRenderer.enabled = true;
            for(int i = 0; i < pinList.Count; ++i)
            {
                lineRenderer.positionCount = pinList.Count;
                lineRenderer.SetPosition(i, new Vector3(pinList[i].transform.GetChild(pinList[i].transform.childCount - 1).position.x, pinList[i].transform.GetChild(pinList[i].transform.childCount - 1).position.y, pinList[i].transform.GetChild(pinList[i].transform.childCount - 1).position.z - 1));
            }
        }
        else lineRenderer.enabled = false;

        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            Loop();
        }*/
    }

    public void Loop()
    {
        for(int i = 0; i < pinList.Count; i++)
            {
                Destroy(pinList[0].transform.GetChild(pinList[i].transform.childCount - 1).gameObject);
                pinList.Remove(pinList[0]);
            }
        if(pinList.Count > 0)
        {
            Loop();
        }
    }

    public void AddPin(GameObject pin)
    {
        pinList.Add(pin);
    }

    public void DeletePin(GameObject pin)
    {
        pinList.Remove(pin);
    }

    void OnEnable()
    {
        //player.SetActive()
    }
    
    public void CheckComponent()
    {
        for(int i = 0; i < ListOfHypLists.list.Count; i++)                                                   // Pour chaque élément dans l'hypothèse
        {
            List<int> validateStickersList = new List<int>();
            List<int> notValidateStickersList = new List<int>();

            for(int n = 0; n < pinList.Count; n++)
            {
                if(player.GetComponent<PlayerMemory>().stickersScriptableList[pinList[n].GetComponent<Sticker_Display>().sticker.index].type.ToString() == "Profile")
                {thereIsAProfile = true;}
                if(ListOfHypLists.list[i].list.Contains(pinList[n].GetComponent<Sticker_Display>().sticker.index))
                {
                    validateStickersList.Add(pinList[n].GetComponent<Sticker_Display>().sticker.index);
                }
                else notValidateStickersList.Add(pinList[n].GetComponent<Sticker_Display>().sticker.index);
            }
            if(validateStickersList.Count == ListOfHypLists.list[i].list.Count)
            {
                if(notValidateStickersList.Count == 0)
                {
                    if(!player.GetComponent<PlayerMemory>().allStickers.Contains(hypotheseresponses[i]))
                    {
                        player.GetComponent<PlayerMemory>().stickerIndexCarnetList.Add(hypotheseresponses[i]);
                        player.GetComponent<PlayerMemory>().allStickers.Add(hypotheseresponses[i]);
                        Transform boardCanvas = GameObject.Find("BoardCanvas").transform;
                        Camera camera = Camera.main;
                        GameObject newSticker = Instantiate(stickerTemplate, new Vector3(camera.transform.position.x, camera.transform.position.y, boardCanvas.position.z), boardCanvas.rotation, boardCanvas);
                        //hypotheseresponses[i], new Vector3(camera.transform.position.x, camera.transform.position.y, boardCanvas.position.z), boardCanvas.rotation, boardCanvas); // Fait apparaitre l'hypothèse crée
                        newSticker.GetComponent<Sticker_Display>().sticker = player.GetComponent<PlayerMemory>().stickersScriptableList[hypotheseresponses[i]];
                        
                        newSticker.GetComponent<ParticleSystem>().Play();
                        newSticker.GetComponent<AudioSource>().Play();
                        newSticker.GetComponent<StickerManager>().OnBoard();
                        if(newSticker.GetComponent<Sticker_Display>().sticker.index == finalDemoHypothese)
                        {
                            if(GameObject.Find("EndDialog"))
                            {
                                //player.GetComponent<Interactions>().dialAndBookCanvas.SetActive(true);
                                //player.GetComponent<Interactions>().boardCanvas.SetActive(false);
                                //Debug.Log(player.name);
                                player.SetActive(true);
                                player.GetComponent<Interactions>().CloseBoard();
                                Camera.main.GetComponent<Camera_Manager>().NotOnBoard();
                                //player.GetComponent<Interactions>().state = Interactions.State.Pause;
                                GameObject.Find("EndDialog").GetComponent<Clara_Cinematic>().ExecuteCommand();
                            }
                        }
                    }
                    createASticker = true;
                    ListOfHypLists.list.RemoveAt(i);   
                    hypotheseresponses.RemoveAt(i);
                    quoteList.Clear();
                }
                else if(notValidateStickersList.Count > 0)
                {
                    if(notValidateStickersList.Count == 1)
                    {
                        quoteList.Add(LanguageManager.Instance.GetDialog("Board_01"));
                    }
                    else quoteList.Add(LanguageManager.Instance.GetDialog("Board_02"));
                }
                else if(notValidateStickersList.Count == ListOfHypLists.list[i].list.Count)
                {
                    quoteList.Add(LanguageManager.Instance.GetDialog("Board_07"));
                }
            }
            else if(validateStickersList.Count == ListOfHypLists.list[i].list.Count - 1 && pinList.Count == ListOfHypLists.list[i].list.Count - 1)
            {
                quoteList.Add(LanguageManager.Instance.GetDialog("Board_03"));
            }
            else if(validateStickersList.Count == ListOfHypLists.list[i].list.Count - 1 && pinList.Count == ListOfHypLists.list[i].list.Count)
            {
                quoteList.Add(LanguageManager.Instance.GetDialog("Board_04"));
            }
        }
        if(!createASticker && quoteList.Count > 0)
        {
            StartCoroutine("ActivateTime");
        }
        createASticker = false;

        //Loop();
    }

    IEnumerator ActivateTime()
    {
        GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).gameObject.SetActive(true);
        if(thereIsAProfile)
        {
            GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = quoteList[quoteList.Count - 1];
        }
        else GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = LanguageManager.Instance.GetDialog("Board_08");
        thereIsAProfile = false;
        quoteList.Clear();
        yield return new WaitForSeconds(1);
        for(int i = 0; i > 0; i--)
        {
            GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).GetComponent<Image>().color = new Vector4(0, 0, 0, i);
        }
        GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).gameObject.SetActive(false);
    }
}
