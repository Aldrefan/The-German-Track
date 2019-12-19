using System.Collections;
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

    string french = "french";
    public string filePath;
    static string textPath;

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
        textPath = Application.dataPath + "/Language/";
        //Debug.Log(Application.dataPath);
        //SaveGameManager.Save();
        string dataAsJson = null;
        if (!Directory.Exists(textPath))
        {
            Directory.CreateDirectory(textPath);
        }
        if(File.Exists(Application.dataPath + "/StreamingAssets/" + filePath))
        {
            dataAsJson = File.ReadAllText(Application.dataPath + "/StreamingAssets/" + filePath);
            Debug.Log(dataAsJson);
            //File.Move(dataAsJson, textPath + filePath);
        }
        datas = JsonUtility.FromJson<TextData>("{\"mytexts\":" + dataAsJson + "}");
    }

    public string GetDialog(string key)
    {
        JsonSave save = SaveGameManager.GetCurrentSave();
        //save.language = "english";
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
