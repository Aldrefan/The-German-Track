using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInteractionIcon : MonoBehaviour
{
    [SerializeField]
    Sprite interactionIcon;

    Sprite actualIcon;

    void OnEnable()
    {
        actualIcon = GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(Timer());
    }

    void OnDisable()
    {
        GetComponent<SpriteRenderer>().sprite = actualIcon;
        StopAllCoroutines();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        GetComponent<SpriteRenderer>().sprite = interactionIcon;
    }
}
