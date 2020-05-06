using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIPPhoneCheck : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(col.GetComponent<EventsCheck>().eventsList.Contains("pibPhoneUnlocked"))
            {
                GetComponent<OutlineSystem>().enabled = true;
                transform.tag = "Interaction";
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
