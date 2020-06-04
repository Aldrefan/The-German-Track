using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextApparition : MonoBehaviour
{
    public string text;

    private string realText;

    LanguageManager langManager;


    // Start is called before the first frame update
    void Start()
    {
        langManager = GameObject.FindObjectOfType<LanguageManager>();
        TransfersTrad();
    }

    void OnEnable()
    {
        TransfersTrad();
    }

    // Update is called once per frame
    void Update()
    {
        if(realText != langManager.GetDialog(text))
        {
            TransfersTrad();
        }
    }

    void TransfersTrad()
    {
        if(langManager!= null)
        {
            realText = langManager.GetDialog(text);

        }

        if(realText != this.GetComponent<Text>().text)
        {
            this.GetComponent<Text>().text = realText;

        }
    }
}
