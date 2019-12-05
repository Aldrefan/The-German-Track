using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            tooltip.SetActive(false);
        }
    }

    /*void OnMouseOver()
    {
        tooltip.SetActive(true);
    }*/

    void OnMouseEnter()
    {
        tooltip.SetActive(true);
    }

    void OnMouseExit()
    {
        tooltip.SetActive(false);
    }

}
