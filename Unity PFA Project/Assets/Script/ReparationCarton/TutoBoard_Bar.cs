using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoBoard_Bar : MonoBehaviour
{
    GameObject player;
    [SerializeField] Sprite newSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && Input.GetButtonDown("Interaction") && player.GetComponent<Interactions>().state == Interactions.State.Normal)
        {
            player.GetComponent<Interactions>().PNJContact = gameObject;
            player.GetComponent<Interactions>().state = Interactions.State.InDialog;
            if(player.GetComponent<EventsCheck>().eventsList.Contains("HypTime"))
            {
                GetComponent<PNJ>().ChangeDialog(1);
                tag = "Board";
                transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = newSprite;
                Destroy(this);
            }
            else GetComponent<PNJ>().ChangeDialog(0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            player = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            player = null;
        }
    }
}
