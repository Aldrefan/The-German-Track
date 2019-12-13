using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSaveButton : MonoBehaviour
{


    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadButtonActive()
    {
        if (Directory.Exists("/Saves/save.txt"))
        {
            this.GetComponent<Text>().color = Color.black;

        }
    }

    void LoadSave()
    {
    }
}
