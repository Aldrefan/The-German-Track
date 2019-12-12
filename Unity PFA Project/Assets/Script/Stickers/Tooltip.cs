using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltip;
    bool onDrag = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        showTooltip();
    }

    void OnMouseDrag()
    {
        showTooltip();
        onDrag = true;
    }

    void OnMouseUp()
    {
        onDrag = false;
    }

    void OnMouseExit()
    {
        if(!onDrag)
        {
            hideTooltip();
        }
    }

    void OnDisable()
    {
        hideTooltip();
    }



    void showTooltip()
    {
        if (!GameObject.Find("Tutorial").GetComponent<TutoKenneth>().isInHelp && !GameObject.Find("Kenneth").GetComponent<Interactions>().isOnTooltip)
        {
            tooltip.SetActive(true);
            GameObject.Find("Kenneth").GetComponent<Interactions>().isOnTooltip = true;
        }
        //else do nothing
    }

    void hideTooltip()
    {
        tooltip.SetActive(false);
        GameObject.Find("Kenneth").GetComponent<Interactions>().isOnTooltip = false;
    }

}
