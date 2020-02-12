using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetAnim : MonoBehaviour
{
    void OnDisable()
    {
        GetComponent<Image>().color = new Vector4(1,1,1,0);
        Text childText = transform.GetChild(0).GetComponent<Text>();
        childText.color = new Vector4(childText.color.a,childText.color.b,childText.color.g,0);
    }
}
