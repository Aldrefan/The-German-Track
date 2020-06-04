using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorielV2_Part2 : MonoBehaviour
{
    int actualIndex;
    public List<TutoIndex> tutoList;

    float lerpTime;
    Vector2 finalPos;
    Vector2 originalPos;
    bool notifOpen;
    bool notifNeedToBeOpen;
    GameObject notif;

    TextApparition notifName;
    TextApparition notifDesc;
    Scrollbar notifScrollbar;


    Interactions playerInteractions;
    EventsCheck playerEventsCheck;
    PlayerMemory playerMemory;
    NewStickerDisplay stickerDisplay;
    Rigidbody2D playerRigidBody;

    GameObject MoveIndicator;
    bool moveDone;
    bool moveStart;

    List<string> TutoSentences = new List<string>();
    string actualSentence;

    bool NotifReset;
    bool CoroutineBreaker;


    // Start is called before the first frame update
    void Start()
    {
        notif = this.transform.Find("Notif").gameObject;
        originalPos = notif.GetComponent<RectTransform>().anchoredPosition;
        //Debug.Log(notif + ""+notif.GetComponent<RectTransform>().anchoredPosition);

        finalPos = new Vector3(-originalPos.x, originalPos.y);

        notifName = this.transform.Find("Notif").Find("Content").Find("Name").GetComponent<TextApparition>();
        notifDesc = this.transform.Find("Notif").Find("Content").Find("ScrollArea").Find("DescContainer").Find("Desc").GetComponent<TextApparition>();
        notifScrollbar = this.transform.Find("Notif").Find("Content").Find("ScrollArea").Find("DescContainer").Find("Scrollbar").GetComponent<Scrollbar>();

        playerInteractions = GameObject.FindObjectOfType<Interactions>();
        playerEventsCheck = playerInteractions.GetComponent<EventsCheck>();
        playerMemory = playerInteractions.GetComponent<PlayerMemory>();
        stickerDisplay = CanvasManager.CManager.GetCanvas("Dialogue").transform.Find("Nouvelle Etiquette").GetComponent<NewStickerDisplay>();
        playerRigidBody = playerInteractions.GetComponent<Rigidbody2D>();

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
            if (tutoList[actualIndex].tutoCase == "GetNote")
            {

                if (stickerDisplay.stickersToNotif.Count != 0)
                {
                    if (!notifOpen)
                    {

                        notifNeedToBeOpen = true;
                        OpenCloseNotif(finalPos);
                    }
                }
                else if (playerInteractions.state == Interactions.State.InCinematic && playerRigidBody.velocity.magnitude !=0)
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

            if (tutoList[actualIndex].tutoCase == "Interroger")
            {
                if (playerInteractions.state == Interactions.State.InDialog && playerInteractions.PNJContact.GetComponent<PNJ>().dialogIndex == 2 )
                {
                    if (!notifOpen)
                    {

                        notifNeedToBeOpen = true;
                        OpenCloseNotif(finalPos);
                    }
                }
                else if(playerInteractions.state == Interactions.State.InDialog && playerInteractions.PNJContact.GetComponent<PNJ>().dialogIndex >2)
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

            if (tutoList[actualIndex].tutoCase == "SortirConv")
            {
                if (playerInteractions.state == Interactions.State.InDialog && playerInteractions.PNJContact.GetComponent<PNJ>().dialogIndex == 2)
                {
                    if (!notifOpen)
                    {
                        notifNeedToBeOpen = true;
                        OpenCloseNotif(finalPos);
                    }
                }
                else if (playerInteractions.state == Interactions.State.Normal && playerInteractions.PNJContact ==null)
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

            if (tutoList[actualIndex].tutoCase == "OuvrirCarnet")
            {
                if (!notifOpen)
                {
                    if (playerInteractions.state == Interactions.State.Normal)
                    {
                        notifNeedToBeOpen = true;
                        OpenCloseNotif(finalPos);
                        StartCoroutine(CloseTutoNotif(5));
                    }
                }
                else
                {
                    if (NotifReset || playerInteractions.state == Interactions.State.OnCarnet)
                    {
                        tutoList[actualIndex].active = true;
                        actualIndex++;
                        notifNeedToBeOpen = false;
                        NotifReset = false;
                        CoroutineBreaker = true;

                    }
                }
            }
            else if (notifOpen && !notifNeedToBeOpen)
            {
                OpenCloseNotif(originalPos);
            }

            if (tutoList[actualIndex].tutoCase == "NavCarnet")
            {
                if (!notifOpen)
                {
                    if (playerInteractions.state == Interactions.State.OnCarnet)
                    {
                        notifNeedToBeOpen = true;
                        OpenCloseNotif(finalPos);
                    }
                }
                else
                {
                    if (notifNeedToBeOpen && playerInteractions.state == Interactions.State.Normal)
                    {
                        tutoList[actualIndex].active = true;
                        actualIndex++;
                        notifNeedToBeOpen = false;
                        NotifReset = false;
                        CoroutineBreaker = true;

                    }
                }
                
            }
            else if (notifOpen && !notifNeedToBeOpen)
            {
                OpenCloseNotif(originalPos);
            }

            if (tutoList[actualIndex].tutoCase == "Board")
            {
                if (!notifOpen)
                {
                    if (playerInteractions.state == Interactions.State.OnBoard)
                    {
                        notifNeedToBeOpen = true;
                        OpenCloseNotif(finalPos);
                    }
                }
                else
                {
                    if (notifNeedToBeOpen && playerInteractions.state == Interactions.State.Normal)
                    {
                        tutoList[actualIndex].active = true;
                        actualIndex++;
                        notifNeedToBeOpen = false;
                        NotifReset = false;
                        CoroutineBreaker = true;

                    }
                }

            }
            else if (notifOpen && !notifNeedToBeOpen)
            {
                OpenCloseNotif(originalPos);
            }

        }
        else if (notifOpen && !notifNeedToBeOpen)
        {
            OpenCloseNotif(originalPos);
        }
    }

    void OpenCloseNotif(Vector3 destination)
    {
        if (tutoList.Count > actualIndex)
        {
            Debug.Log("1");
            notifName.text = tutoList[actualIndex].tutoNameKey;
            notifDesc.text = tutoList[actualIndex].tutoDescKey;
            notifScrollbar.value = 1;
        }

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

    IEnumerator CloseTutoNotif(float timeToClose)
    {
        yield return new WaitForSeconds(timeToClose);
        if (!CoroutineBreaker)
        {
            NotifReset = true;

        }
        else
        {
            CoroutineBreaker = false;
            yield return null;
        }
    }
}




