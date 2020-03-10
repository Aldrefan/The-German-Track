using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnimKurtDoor : MonoBehaviour
{
    GameObject kurtDoor;

    void Start()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        kurtDoor = this.transform.parent.Find("doorKurt").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(kurtDoor == null)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            Destroy(this);
        }
    }
}
