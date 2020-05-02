using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogInterface : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnEnable()
    {
        player = GameObject.Find("Kenneth");
        transform.GetChild(6).gameObject.SetActive(false);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public void HideHelp()
    {
        transform.GetChild(6).gameObject.SetActive(false);
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(!transform.parent.GetChild(transform.parent.childCount - 1).gameObject.activeInHierarchy && player.GetComponent<Interactions>().state != Interactions.State.OnCarnet)
        {
            if(player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().quoteFinished)
            {
                player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().dialogLine++;
                player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().Startdialogue();
            }
            else 
            {
                player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().FullQuote(gameObject);
            }
        }
    }

    public void StartTimer()
    {
        StartCoroutine(TimerBeforeHelpShowing());
    }

    IEnumerator TimerBeforeHelpShowing()
    {
        yield return new WaitForSeconds(2);
        transform.GetChild(6).gameObject.SetActive(true);
    }
}
