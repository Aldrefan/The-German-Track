using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTutoFrameWhenEsc : MonoBehaviour
{
    [SerializeField]
    string objectNameHideFrom;
    [SerializeField]
    string objectParentNameHideFrom;
    [SerializeField]
    string objectNameToHide;

    GameObject objectHideFrom;
    GameObject objectToHide;

    private void Start()
    {
        objectHideFrom = GameObject.Find(objectParentNameHideFrom).transform.Find(objectNameHideFrom).gameObject;
        objectToHide = GameObject.Find(objectNameToHide).gameObject;
    }

    void Update()
    {
        CheckVisibily();
    }

    void CheckVisibily()
    {
        if (objectHideFrom.activeInHierarchy)
        {

            objectToHide.SetActive(false);
                    
        }
        else
        {
            objectToHide.SetActive(true);

        }
    }
}
