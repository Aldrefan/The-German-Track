﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LanguageManager : MonoBehaviour
{
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
        //SaveGameManager.Save();
        string dataAsJson = null;
        if(File.Exists("Assets/Resources/" + filePath))
        {
            dataAsJson = File.ReadAllText("Assets/Resources/" + filePath);
        }
        datas = JsonUtility.FromJson<TextData>("{\"mytexts\":" + dataAsJson + "}");
    }

    public string GetDialog(string key)
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        //save.language = "french";
        if(datas != null)
        {
            for(int i = 0; i < datas.mytexts.Length; i++)
            {
                if(datas.mytexts[i].Key == key)
                {
                    if(save.language == "french")
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
}
