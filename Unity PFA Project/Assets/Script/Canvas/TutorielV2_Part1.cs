using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorielV2_Part1 : MonoBehaviour
{
     public int MaxLetterPerSentence;

    int actualIndex;
    public List<TutoIndex> tutoList;

    float lerpTime;
    Vector2 finalPos;
    Vector2 originalPos;
    bool notifOpen;
    bool notifNeedToBeOpen;
    GameObject notif;

    Text notifName;
    Text notifDesc;

    Interactions playerInteractions;

    GameObject MoveIndicator;
    bool moveDone;
    bool moveStart;

    List<string> TutoSentences = new List<string>();
    string actualSentence;


    // Start is called before the first frame update
    void Start()
    {
        notif = this.transform.Find("Notif").gameObject;
        originalPos = notif.GetComponent<RectTransform>().anchoredPosition;
        Debug.Log(notif + ""+notif.GetComponent<RectTransform>().anchoredPosition);

        finalPos = new Vector3(-originalPos.x, originalPos.y);

        notifName = this.transform.Find("Notif").Find("Content").Find("Name").GetComponent<Text>();
        notifDesc = this.transform.Find("Notif").Find("Content").Find("Desc").GetComponent<Text>();

        playerInteractions = GameObject.FindObjectOfType<Interactions>();

        MoveIndicator = this.transform.Find("MoveIndic").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveIndic();
        TutoCase();
    }

    void TutoCase()
    {
        if (tutoList.Count >= actualIndex + 1 && !tutoList[actualIndex].active)
        {
            Debug.Log("0");
            if (tutoList[actualIndex].tutoCase == "dialog")
            {
                Debug.Log("Dialog");

                if (playerInteractions.state == Interactions.State.InCinematic)
                {
                    if (!notifOpen)
                    {
                        notifNeedToBeOpen = true;
                        OpenCloseNotif(finalPos);
                    }
                }
                else
                {
                    tutoList[actualIndex].active = true;
                    actualIndex++;
                    notifNeedToBeOpen = false;
                }
            }
            else if (notifOpen && !notifNeedToBeOpen)
            {
                OpenCloseNotif(originalPos);
            }

            if (tutoList[actualIndex].tutoCase == "movement")
            {
                Debug.Log("Movement");

                MoveIndic();
            }

            if (tutoList[actualIndex].tutoCase == "interaction")
            {
                if (playerInteractions.PNJContact != null)
                {
                    if (!notifOpen)
                    {
                        notifNeedToBeOpen = true;
                        OpenCloseNotif(finalPos);
                    }
                }
                else if(Input.GetButtonDown("Interaction"))
                {
                    tutoList[actualIndex].active = true;
                    actualIndex++;
                    notifNeedToBeOpen = false;
                }
            }
            else if (notifOpen && !notifNeedToBeOpen)
            {
                OpenCloseNotif(originalPos);
            }
        }
    }

    void OpenCloseNotif(Vector3 destination)
    {
        notifName.text = tutoList[actualIndex].tutoNameKey;
        notifDesc.text = tutoList[actualIndex].tutoDescKey;

        if (Vector3.Distance(notif.GetComponent<RectTransform>().anchoredPosition, destination) > 0.1f)
        {
            lerpTime += Time.deltaTime;
            lerpTime = Mathf.Clamp01(lerpTime);
            notif.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(notif.GetComponent<RectTransform>().anchoredPosition, destination, lerpTime);
        }
        else
        {
            notif.GetComponent<RectTransform>().anchoredPosition = destination;
            if (Vector3.Distance(notif.GetComponent<RectTransform>().anchoredPosition, originalPos) <= 0.1f)
            {
                notifOpen = false;
            }
            else if(Vector3.Distance(notif.GetComponent<RectTransform>().anchoredPosition, finalPos) <= 0.1)
            {
                
                notifOpen = true;
            }
            lerpTime = 0;

        }
    }

    void MoveIndic()
    {
        if (!MoveIndicator.activeSelf)
        {
            if (!moveStart)
            {
                moveStart = true;
                StartCoroutine(InputCheck());
            }
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
            {
                moveDone = true;
                tutoList[actualIndex].active = true;
                actualIndex++;
            }
        }

        if (MoveIndicator.activeSelf)
        {
            MoveIndicator.GetComponent<RectTransform>().position = playerInteractions.transform.position;
            if (Input.GetAxis("Horizontal") > 0)
            {
                MoveIndicator.transform.Find("Right").gameObject.SetActive(false);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                MoveIndicator.transform.Find("Left").gameObject.SetActive(false);
            }

            if (!MoveIndicator.transform.Find("Left").gameObject.activeSelf && !MoveIndicator.transform.Find("Right").gameObject.activeSelf)
            {
                tutoList[actualIndex].active = true;
                actualIndex++;

            }
        }
    }

    IEnumerator InputCheck()
    {
        yield return new WaitForSeconds(2);
        if(!moveDone)
        {
            MoveIndicator.GetComponent<RectTransform>().position = playerInteractions.transform.position;

            MoveIndicator.SetActive(true);

        }

    } 
}

[System.Serializable]
public class TutoIndex
{
    public bool active;
    public int index;
    public string tutoCase;
    public string tutoNameKey;
    public string tutoDescKey;

    //public TutoIndex(bool newActive = false, int newIndex = 0, string newTutoNameKey = null, string newTutoDescKey = null)
    //{
    //    active = newActive;
    //    index = newIndex;
    //    tutoNameKey = newTutoNameKey;
    //    tutoDescKey = newTutoDescKey;
    //}
}


