using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnimKurtDoor : MonoBehaviour
{
    GameObject kurtDoor;
    bool getKurt;

    void Start()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        GetKurtDoor();
        if(kurtDoor != null && getKurt == true)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            Destroy(this);
        }
    }

    void GetKurtDoor()
    {
        if(!getKurt)
        {
            if (this.transform.parent.Find("doorKurt"))
            {
                kurtDoor = this.transform.parent.Find("doorKurt").gameObject;
                getKurt = true;
            }
        }
    }
}
