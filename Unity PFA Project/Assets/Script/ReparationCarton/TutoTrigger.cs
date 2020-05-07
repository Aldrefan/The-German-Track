using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTrigger : MonoBehaviour
{
    bool casier;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {casier = true;}
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {casier = false;}
    }

    void Update()
    {
        if(Input.GetButtonDown("Interaction") && casier)
        {
            GetComponent<Clara_Cinematic>().ExecuteCommand();
            this.enabled = false;
        }
    }
}
