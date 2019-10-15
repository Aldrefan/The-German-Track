using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class String_Manager : MonoBehaviour
{
    public List<GameObject> pinList = new List<GameObject>();
    LineRenderer lineRenderer;
    bool checking = false;
    public GameObject finalDemoHypothese;
    public List<GameObject> validateList;
    int index = 0;
    int microIndex = 0;
    public List<GameObject> hypotheseresponses = new List<GameObject>();
    bool finished;
    public List<string> quoteList;
    bool createASticker;

public HypotheseList ListOfPointLists = new HypotheseList();

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

GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Awake()
    {
        player = GameObject.Find("Kenneth");
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
                lineRenderer.SetPosition(i, new Vector3(pinList[i].transform.GetChild(0).position.x, pinList[i].transform.GetChild(0).position.y, pinList[i].transform.GetChild(0).position.z - 1));
            }
        }
        else lineRenderer.enabled = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Loop();
        }
    }

    void Loop()
    {
        for(int i = 0; i < pinList.Count; i++)
            {
                Destroy(pinList[0].transform.GetChild(0).gameObject);
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
        
    }
    
    public void CheckComponent()
    {
        /*bool needSticker;
        bool tooMuchStickers;
        bool oneStickerLess; */
        for(int i = 0; i < ListOfHypLists.list.Count; i++)                                              // Pour chaque élément dans l'hypothèse
        {
            List<int> validateStickersList = new List<int>();
            List<int> notValidateStickersList = new List<int>();

            for(int n = 0; n < pinList.Count; n++)
            {
                if(ListOfHypLists.list[i].list.Contains(pinList[n].GetComponent<Pin_System>().stickerIndex))
                {
                    validateStickersList.Add(pinList[n].GetComponent<Pin_System>().stickerIndex);
                }
                else notValidateStickersList.Add(pinList[n].GetComponent<Pin_System>().stickerIndex);
            }
            if(validateStickersList.Count == ListOfHypLists.list[i].list.Count)
            {
                if(notValidateStickersList.Count == 0)
                {
                    player.GetComponent<PlayerMemory>().stickerIndexCarnetList.Add(hypotheseresponses[i].GetComponent<Pin_System>().stickerIndex);
                    player.GetComponent<PlayerMemory>().allStickers.Add(hypotheseresponses[i].GetComponent<Pin_System>().stickerIndex);
                    Transform boardCanvas = GameObject.Find("BoardCanvas").transform;
                    Camera camera = Camera.main;
                    GameObject newSticker = Instantiate(hypotheseresponses[i], new Vector3(camera.transform.position.x, camera.transform.position.y, boardCanvas.position.z), boardCanvas.rotation, boardCanvas); // Fait apparaitre l'hypothèse crée
                    newSticker.GetComponent<StickerManager>().OnBoard();
                    if(hypotheseresponses[i].GetComponent<Pin_System>().stickerIndex == finalDemoHypothese.GetComponent<Pin_System>().stickerIndex)
                    {
                        GameObject.Find("EndCanvas").GetComponent<EndScreen>().EndDemo();
                    }
                    ListOfHypLists.list.RemoveAt(i);   
                    hypotheseresponses.RemoveAt(i);
                    quoteList.Clear();
                    createASticker = true;
                }
                else if(notValidateStickersList.Count > 0)
                {
                    switch(notValidateStickersList.Count)
                    {
                        case 1:
                        quoteList.Add("1 étiquette en trop");
                        break;

                        default:
                        quoteList.Add("Il y a beaucoup trop d'étiquettes");
                        break;
                    }
                }
            }
            else if(validateStickersList.Count == ListOfHypLists.list[i].list.Count - 1 && pinList.Count == ListOfHypLists.list[i].list.Count - 1)
            {
                quoteList.Add("Il manque 1 étiquette");
            }
            else if(validateStickersList.Count == ListOfHypLists.list[i].list.Count - 1 && pinList.Count == ListOfHypLists.list[i].list.Count)
            {
                quoteList.Add("Il y a une étiquette fausse");
            }
        }
        if(!createASticker && quoteList.Count > 0)
        {
            StartCoroutine("ActivateTime");
        }
        createASticker = false;
    }
    

    /*void OldLoop()
    {
        //CheckMacroList(0);
        for(int i = 0; i < ListOfPointLists.list.Count; i++)
        {
            if(ListOfPointLists.list[i].list.Count == pinList.Count)
            {
                for(int n = 0; n < pinList.Count; n++)                                                // Pour chaque élément dans l'hypothèse
                {
                    for(int j = 0; j < ListOfPointLists.list[i].list.Count; j++)
                    {
                        if(ListOfPointLists.list[i].list[j].GetComponent<Pin_System>().stickerIndex == pinList[n].GetComponent<Pin_System>().stickerIndex)
                        {
                            validateList.Add(ListOfPointLists.list[i].list[j]);
                            if(validateList.Count == ListOfPointLists.list[i].list.Count)
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMemory>().stickerIndexCarnetList.Add(hypotheseresponses[i].GetComponent<Pin_System>().stickerIndex);
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMemory>().allStickers.Add(hypotheseresponses[i].GetComponent<Pin_System>().stickerIndex);
                                Transform boardCanvas = GameObject.Find("BoardCanvas").transform;
                                Camera camera = Camera.main;
                                Instantiate(hypotheseresponses[i], new Vector3(camera.transform.position.x, camera.transform.position.y, boardCanvas.position.z), boardCanvas.rotation, boardCanvas); // Fait apparaitre l'hypothèse crée
                                if(hypotheseresponses[i].GetComponent<Pin_System>().stickerIndex == finalDemoHypothese.GetComponent<Pin_System>().stickerIndex)
                                {
                                    GameObject.Find("EndCanvas").GetComponent<EndScreen>().EndDemo();
                                }
                                ListOfPointLists.list.RemoveAt(i);   
                                hypotheseresponses.RemoveAt(i);                                       // Supprime l'hypothèse de la liste d'hypothèses
                            }
                        }
                    }
                }
                validateList.Clear();
            }
        }
        validateList.Clear();
    } */

    IEnumerator ActivateTime()
    {
        GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).gameObject.SetActive(true);
        //Debug.Log(GameObject.Find("FlottingCanvas").transform.GetChild(GameObject.Find("FlottingCanvas").transform.childCount - 4).transform.GetChild(0).GetComponent<Text>().text = quoteList[quoteList.Count - 1]);
        GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = quoteList[quoteList.Count - 1];
        yield return new WaitForSeconds(1);
        for(int i = 0; i > 0; i--)
        {
            GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).GetComponent<Image>().color = new Vector4(0, 0, 0, i);
        }
        GameObject.Find("Ken_Board_FlCanvas").transform.GetChild(0).gameObject.SetActive(false);
        quoteList.Clear();
    }
}
