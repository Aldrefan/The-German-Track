using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickerManager : MonoBehaviour
{
    public void OnBoard()
    {
        //transform.localPosition = Vector3.zero;
        Destroy(GetComponent<CarnetSticker>());
        GetComponent<Pin_System>().enabled = true;
        GetComponent<StickerManager>().enabled = false;
    }

    public void OnCarnet()
    {
        Destroy(GetComponent<Pin_System>());
        Destroy(GetComponent<EventTrigger>());
        GetComponent<CarnetSticker>().enabled = true;
        GetComponent<StickerManager>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}