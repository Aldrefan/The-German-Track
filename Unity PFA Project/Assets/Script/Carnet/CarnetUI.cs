using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarnetUI : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        /*if(GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().PnjMet.Contains("Clara"))
        {
            animator.SetBool("ClickOn", false);
        }
        else animator.SetBool("ClickOn", true);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(GameObject.Find("Kenneth").GetComponent<Interactions>().state != Interactions.State.InCinematic && GameObject.Find("Kenneth").GetComponent<Interactions>().state != Interactions.State.Pause)
        {
            GameObject.Find("Kenneth").GetComponent<Interactions>().OpenBookExe();
        }
    }

    void OnMouseOver()
    {
        animator.SetBool("CursorOver", true);
    }

    void OnMouseExit()
    {
        animator.SetBool("CursorOver", false);
    }
}
