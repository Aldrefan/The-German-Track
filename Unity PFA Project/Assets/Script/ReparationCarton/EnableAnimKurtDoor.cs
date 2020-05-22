using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnimKurtDoor : MonoBehaviour
{
    GameObject kurtDoor;
    GameObject KurtDoor => kurtDoor = kurtDoor ?? this.transform.parent.Find("doorKurt").gameObject;
    bool getKurt;

    void Start()
    {
        //this.GetComponent<BoxCollider2D>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        /*GetKurtDoor();
        if(kurtDoor != null && getKurt == true)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            Destroy(this);
        }*/
    }

    void GetKurtDoor()
    {
        if(!getKurt && KurtDoor != null)
        {

                getKurt = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(col.GetComponent<EventsCheck>().eventsList.Contains("OuvrirPorteKurt"))
            {
                if(GetComponent<DoorOpening>())
                {
                    GetComponent<DoorOpening>().enabled = true;
                }
                if(GetComponent<OutlineSystem>())
                {
                    GetComponent<OutlineSystem>().enabled = true;
                }
            }
        }
    }
}
