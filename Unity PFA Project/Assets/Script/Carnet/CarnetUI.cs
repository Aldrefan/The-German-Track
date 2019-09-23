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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Interactions>().OpenBookExe();
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
