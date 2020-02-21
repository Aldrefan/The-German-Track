using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltip;
    bool onDrag = false;

    GameObject tutoObject;
    GameObject characKenneth;



    // Start is called before the first frame update
    void Start()
    {
        tutoObject = GameObject.Find("Tutorial");
        characKenneth = GameObject.Find("Kenneth");
    }

    // Update is called once per frame
    void Update()
    {
        CheckValue();
    }

    void CheckValue()
    {
        if(tutoObject == null)
        {
            tutoObject = GameObject.Find("Tutorial");
        }
        if (characKenneth == null)
        {
            characKenneth = GameObject.Find("Kenneth");

        }
    }

    void OnMouseEnter()
    {
        //check if another tooltip is open
        Tooltip[] tooltips = GameObject.FindObjectsOfType<Tooltip>();
        foreach (Tooltip objet in tooltips)
        {
            objet.GetComponent<Tooltip>().hideTooltip();
        }

        //open this tooltip
        showTooltip();

        if (this.transform.parent.name == "BoardCanvas")
        {
            this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
        }
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
        if (!tutoObject.GetComponent<TutoKenneth>().isInHelp && !characKenneth.GetComponent<Interactions>().isOnTooltip)
        {
            tooltip.SetActive(true);
            characKenneth.GetComponent<Interactions>().isOnTooltip = true;
        }
        //else do nothing
    }

    void hideTooltip()
    {
        tooltip.SetActive(false);
        if (characKenneth != null)
        {
            characKenneth.GetComponent<Interactions>().isOnTooltip = false;

        }
    }



}
