using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItemsInContact : MonoBehaviour
{
    [SerializeField]
    private int objectIndex;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(col.GetComponent<PlayerMemory>().allStickers.Contains(objectIndex))
            {
                GetComponent<Collider2D>().enabled = false;
            }
            if(transform.childCount > 1)
            {
                GetComponent<OutlineSystem>().HideOutline();
            }
        }
    }
}
