using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottVerification : MonoBehaviour
{
    [SerializeField]
    private GameObject _scott;
    [SerializeField]
    private GameObject _nelsonSpot;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Timer());
    }

    void CheckEvent()
    {
        if(GetComponent<Interactions>().PnjMet.Contains("dialog_williamscott"))
        {
            _scott.transform.parent = _nelsonSpot.transform.parent;
            _scott.transform.position = _nelsonSpot.transform.position;
            _scott.tag = "PNJinteractable";
            _scott.GetComponent<PNJ>().enabled = true;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        CheckEvent();
    }
}
