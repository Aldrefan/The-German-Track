using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piles : MonoBehaviour
{
    public Transform pileProfiles;

    public Transform pileIndices;

    public Transform pileFaits;

    public Transform pileHypothèses;

    [SerializeField]
    Transform profilesButton;
    [SerializeField]
    Transform indicesButton;
    [SerializeField]
    Transform faitsButton;
    [SerializeField]
    Transform hypothesesButton;

    void Update()
    {
        /*if(Input.mousePosition.x > pileProfiles.GetComponent<BoxCollider2D>().bounds.min.x && Input.mousePosition.y > pileProfiles.GetComponent<BoxCollider2D>().bounds.min.y && Input.mousePosition.x < pileProfiles.GetComponent<BoxCollider2D>().bounds.max.x && Input.mousePosition.y < pileProfiles.GetComponent<BoxCollider2D>().bounds.max.y)
        {
            mouseOverState = MouseOverAPile.Profiles;
        }
        else mouseOverState = MouseOverAPile.None;*/
    }

    void OnEnable()
    {
        profilesButton.parent.gameObject.SetActive(true);
        profilesButton.localScale = new Vector3(1,1,1);
        indicesButton.localScale = new Vector3(1,1,1);
        faitsButton.localScale = new Vector3(1,1,1);
        hypothesesButton.localScale = new Vector3(1,1,1);
    }

    public void HidePileHider()
    {
        HidePiles();
        pileHypothèses.parent.GetComponent<Animator>().SetBool("Hide", !pileHypothèses.parent.GetComponent<Animator>().GetBool("Hide"));
        if(!pileHypothèses.parent.GetComponent<Animator>().GetBool("Hide"))
        {
            StartCoroutine(TimerButton());
        }
        else
        {
            CanvasManager.CManager.GetCanvas("PilesButtons").SetActive(!CanvasManager.CManager.GetCanvas("PilesButtons").activeInHierarchy);
        }
    }

    IEnumerator TimerButton()
    {
        yield return new WaitForSeconds(0.2f);
        CanvasManager.CManager.GetCanvas("PilesButtons").SetActive(!CanvasManager.CManager.GetCanvas("PilesButtons").activeInHierarchy);
    }

    public void HidePiles()
    {
        profilesButton.transform.localScale = new Vector3(1,1,1);
        indicesButton.transform.localScale = new Vector3(1,1,1);
        faitsButton.transform.localScale = new Vector3(1,1,1);
        hypothesesButton.transform.localScale = new Vector3(1,1,1);
        pileProfiles.GetComponent<Animator>().SetBool("Unwrap", false);
        pileIndices.GetComponent<Animator>().SetBool("Unwrap", false);
        pileFaits.GetComponent<Animator>().SetBool("Unwrap", false);
        pileHypothèses.GetComponent<Animator>().SetBool("Unwrap", false);
    }

    public void CheckTypeAndSort(GameObject stickerToCheckAndSort)
    {
        switch(stickerToCheckAndSort.GetComponent<Sticker_Display>().sticker.type)
        {
            case Sticker.Type.Profile:
            SortInProfiles(stickerToCheckAndSort.transform);
            break;

            case Sticker.Type.Clue:
            SortInIndices(stickerToCheckAndSort.transform);
            break;

            case Sticker.Type.Fact:
            SortInFaits(stickerToCheckAndSort.transform);
            break;

            case Sticker.Type.Hypothesis:
            SortInHypothèses(stickerToCheckAndSort.transform);
            break;
        }
    }
    void SortInProfiles(Transform stickerToSort)
    {
        stickerToSort.parent = pileProfiles;
        stickerToSort.localPosition = Vector3.zero;
    }
    void SortInIndices(Transform stickerToSort)
    {
        stickerToSort.parent = pileIndices;
        stickerToSort.localPosition = Vector3.zero;
        //stickerToSort.position = new Vector3(pileIndices.position.x, pileIndices.position.y, pileIndices.position.z);
    }
    void SortInFaits(Transform stickerToSort)
    {
        stickerToSort.parent = pileFaits;
        stickerToSort.localPosition = Vector3.zero;
        //stickerToSort.position = new Vector3(pileFaits.position.x, pileFaits.position.y, pileFaits.position.z);
    }
    void SortInHypothèses(Transform stickerToSort)
    {
        stickerToSort.parent = pileHypothèses;
        stickerToSort.localPosition = Vector3.zero;
        //stickerToSort.position = new Vector3(pileHypothèses.position.x, pileHypothèses.position.y, pileHypothèses.position.z);
    }

    public void UnwrapProfiles()
    {
        pileProfiles.GetComponent<Animator>().SetBool("Unwrap", !pileProfiles.GetComponent<Animator>().GetBool("Unwrap"));
        pileProfiles.transform.parent.SetAsLastSibling();
        profilesButton.localScale *= -1;
    }
    public void UnwrapIndices()
    {
        pileIndices.GetComponent<Animator>().SetBool("Unwrap", !pileIndices.GetComponent<Animator>().GetBool("Unwrap"));
        pileIndices.transform.parent.SetAsLastSibling();
        indicesButton.localScale *= -1;
    }
    public void UnwrapFaits()
    {
        pileFaits.GetComponent<Animator>().SetBool("Unwrap", !pileFaits.GetComponent<Animator>().GetBool("Unwrap"));
        pileFaits.transform.parent.SetAsLastSibling();
        faitsButton.localScale *= -1;
    }
    public void UnwrapHypotheses()
    {
        pileHypothèses.GetComponent<Animator>().SetBool("Unwrap", !pileHypothèses.GetComponent<Animator>().GetBool("Unwrap"));
        pileHypothèses.transform.parent.SetAsLastSibling();
        hypothesesButton.localScale *= -1;
    }
}
