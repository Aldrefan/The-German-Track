using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    Interactions playerInteractionScript;
    // Start is called before the first frame update
    void Start()
    {
        playerInteractionScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(playerInteractionScript.onBoard == false)
            {
                transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
}
