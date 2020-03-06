using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piles : MonoBehaviour
{
    public Transform pileProfiles;
    Vector2 pileProfilesBounds;

    public Transform pileIndices;
    Vector2 pileIndicesBounds;

    public Transform pileFaits;
    Vector2 pileFaitsBounds;

    public Transform pileHypothèses;
    Vector2 pileHypothesesBounds;

    public enum MouseOverAPile{None, Profiles, Indices, Faits, Hypothèses}
    public MouseOverAPile mouseOverState;

    void OnEnable()
    {
        //pileProfilesBounds = Vector2(pileProfiles.GetComponent<RectTransform>().)
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
    }
    public void UnwrapIndices()
    {
        pileIndices.GetComponent<Animator>().SetBool("Unwrap", !pileIndices.GetComponent<Animator>().GetBool("Unwrap"));
    }
    public void UnwrapFaits()
    {
        pileFaits.GetComponent<Animator>().SetBool("Unwrap", !pileFaits.GetComponent<Animator>().GetBool("Unwrap"));
    }
    public void UnwrapHypotheses()
    {
        pileHypothèses.GetComponent<Animator>().SetBool("Unwrap", !pileHypothèses.GetComponent<Animator>().GetBool("Unwrap"));
    }
}
