using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOrganiser : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseOver()
    {
        animator.SetBool("MouseOver", true);
    }
    void OnMouseExit()
    {
        animator.SetBool("MouseOver", false);
    }

    void OnMouseDown()
    {
        animator.SetTrigger("MouseClick");
    }

    void ShowPileHider()
    {
        CanvasManager.CManager.GetCanvas("Board_FIX").GetComponent<Piles>().pileHiderButton.gameObject.SetActive(true);
        CanvasManager.CManager.GetCanvas("PilesButtons").gameObject.SetActive(true);
    }

    void HidePiles()
    {
        CanvasManager.CManager.GetCanvas("PilesContainer").transform.GetChild(1).gameObject.SetActive(false);
        CanvasManager.CManager.GetCanvas("PilesContainer").transform.GetChild(2).gameObject.SetActive(false);
        CanvasManager.CManager.GetCanvas("PilesContainer").transform.GetChild(3).gameObject.SetActive(false);
        CanvasManager.CManager.GetCanvas("PilesContainer").transform.GetChild(4).gameObject.SetActive(false);
    }

    void ShowPiles()
    {
        CanvasManager.CManager.GetCanvas("PilesContainer").transform.GetChild(1).gameObject.SetActive(true);
        CanvasManager.CManager.GetCanvas("PilesContainer").transform.GetChild(2).gameObject.SetActive(true);
        CanvasManager.CManager.GetCanvas("PilesContainer").transform.GetChild(3).gameObject.SetActive(true);
        CanvasManager.CManager.GetCanvas("PilesContainer").transform.GetChild(4).gameObject.SetActive(true);
    }
}
