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
                player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().Startdialogue();
            }
            else 
            {
                player.GetComponent<Interactions>().PNJContact.GetComponent<PNJ>().FullQuote(gameObject);
            }
        }
    }
}