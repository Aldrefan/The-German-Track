using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSaveButton : MonoBehaviour
{

    void Update()
    {
        LoadButtonActive();
    }

    void LoadButtonActive()
    {
        if (File.Exists(Application.dataPath + "/Saves/save.txt"))
        {
            this.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.GetComponent<Button>().interactable = false;
            GameSaveSystem.gameToLoad = false;
        }
    }

    public void LoadSave()
    {
        GameSaveSystem.gameToLoad = true;
        SceneManager.LoadScene("InterScene" + GameSaveSystem.ReturnLevelName());
    }
}
