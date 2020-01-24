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
        TransfersTrad();
    }

    void OnEnable()
    {
        TransfersTrad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TransfersTrad()
    {
        LanguageManager langManager = GameObject.FindObjectOfType<LanguageManager>();
        if(langManager!= null)
        {
            this.GetComponent<Text>().text = langManager.GetDialog(text);

        }
    }
}
