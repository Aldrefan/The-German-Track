using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeInteractionIcon : MonoBehaviour
{
    [SerializeField]
    Sprite interactionIcon;

    Sprite actualIcon;

    void OnEnable()
    {
        actualIcon = GetComponent<Image>().sprite;
        StartCoroutine(Timer());
    }

    void OnDisable()
    {
        GetComponent<Image>().sprite = actualIcon;
        StopAllCoroutines();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        GetComponent<Image>().sprite = interactionIcon;
    }
}
