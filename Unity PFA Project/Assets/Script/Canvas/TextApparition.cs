using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextApparition : MonoBehaviour
{
    public string text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        GetComponent<Text>().text = LanguageManager.Instance.GetDialog(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
