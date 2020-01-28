using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LanguageManager : MonoBehaviour
{

    public string language = "english";
    public static LanguageManager Instance;
    [Serializable]
    public class Entry
    {
        public string Key;
        public string FR;
        public string EN;
    }
    public string filePath;

    [Serializable]
    public class TextData
    {
        public Entry[] mytexts;
    }
    
    public TextData datas;

    void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        InitLanguage();
        
        //SaveGameManager.Save();
        string dataAsJson = null;
        if(File.Exists(Application.streamingAssetsPath + "/" + filePath))
        {
            dataAsJson = File.ReadAllText(Application.streamingAssetsPath + "/" + filePath);
        }
        datas = JsonUtility.FromJson<TextData>("{\"mytexts\":" + dataAsJson + "}");

        RefreshTexts();
    }

    void RefreshTexts()
    {
        TextApparition[] textsToRefresh = GameObject.FindObjectsOfType<TextApparition>();
        foreach(TextApparition objet in textsToRefresh)
        {
            objet.gameObject.SetActive(false);
            objet.gameObject.SetActive(true);
        }
    }

    public string GetDialog(string key)
    {
        //save.language = "french";
        if(datas != null)
        {
            for(int i = 0; i < datas.mytexts.Length; i++)
            {
                if(datas.mytexts[i].Key == key)
                {
                    if(language == "french")
                    {
                        return datas.mytexts[i].FR;
                    }
                    else 
                    {
                        return datas.mytexts[i].EN;
                    }
                }
            }
            return null;
        }
        return null;
    }

    public void InitLanguage()
    {
        SettingsData settingsSave = GameSaveSystem.LoadSettingsData();
        if(settingsSave.gameLanguage != language) language = settingsSave.gameLanguage;
    }
}
