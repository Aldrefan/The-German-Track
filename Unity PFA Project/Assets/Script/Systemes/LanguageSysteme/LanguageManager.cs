using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LanguageManager : MonoBehaviour
{
    public float dialogSpeed = 0.04f;
    public string language = "english";
    public static LanguageManager Instance;

    [Serializable]
    public class Entry
    {
        public string Key;
        public string FR;
        public string EN;
        public string FRName;
        public string ENName;
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
        if (File.Exists(Application.streamingAssetsPath + "/" + filePath))
        {
            dataAsJson = File.ReadAllText(Application.streamingAssetsPath + "/" + filePath);
        }
        datas = JsonUtility.FromJson<TextData>("{\"mytexts\":" + dataAsJson + "}");

        RefreshTexts();
    }

    void RefreshTexts()
    {
        TextApparition[] textsToRefresh = GameObject.FindObjectsOfType<TextApparition>();
        foreach (TextApparition objet in textsToRefresh)
        {
            objet.gameObject.SetActive(false);
            objet.gameObject.SetActive(true);
        }
    }

    public string GetDialog(string key)
    {
        //save.language = "french";
        if (datas != null)
        {
            for (int i = 0; i < datas.mytexts.Length; i++)
            {
                if (datas.mytexts[i].Key == key)
                {
                    if (language == "french")
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

    public List<string> GetListOfCharacters(int position, int length)
    {
        if (datas != null)
        {
            List<string> characterNames = new List<string>();
            string[] referenceKey = datas.mytexts[position].Key.Split("_"[0]);
            for (int i = position; i < position + length; i++)
            {
                if (!characterNames.Contains(datas.mytexts[i].FRName))
                {
                    characterNames.Add(datas.mytexts[i].FRName);
                }
            }
            return characterNames;
        }
        return null;
    }

    public string GetDialogKeyfromIndex(int startIndex)
    {
        //save.language = "french";
        if (datas != null)
        {
            return datas.mytexts[startIndex].Key;
        }
        return null;
    }

    public string GetNameOfTheSpeaker(string key)
    {
        if (datas != null)
        {
            for (int i = 0; i < datas.mytexts.Length; i++)
            {
                if (datas.mytexts[i].Key == key)
                {
                    if (language == "french")
                    {
                        return datas.mytexts[i].FRName;
                    }
                    else
                    {
                        return datas.mytexts[i].ENName;
                    }
                }
            }
            return null;
        }
        return null;
    }

    public Sprite GetCharacterSprite(string key)
    {
        if (datas != null)
        {
            for (int i = 0; i < datas.mytexts.Length; i++)
            {
                if (datas.mytexts[i].Key == key)
                {
                    return CharactersBank.CharBank.GetSprite(datas.mytexts[i].FRName);
                }
            }
            return null;
        }
        return null;
    }

    public int GetDialogPosition(string key)
    {
        if (datas != null)
        {
            string[] actualKeyPart = key.Split("_"[0]);
            //Debug.Log("key : " + key);
            //Debug.Log(datas.mytexts.Length);
            for (int i = 0; i < datas.mytexts.Length; i++)
            {
                string[] keyParts = datas.mytexts[i].Key.Split("_"[0]);
                //Debug.Log(keyParts.Length);
                //if(datas.mytexts[i].Key == key)
                //{
                //return i;
                if (keyParts.Length > 1)
                {
                    /*foreach(string n in keyParts)
                    {Debug.Log(n);}*/
                    if (keyParts[0] + "_" + keyParts[1] == key)
                    {
                        //Debug.Log(datas.mytexts[i].Key);
                        return i;
                    }
                }
                //}
            }
            return 0;
        }
        return 0;
    }

    public int DialogQuoteLength(int startIndex)
    {
        if (datas != null)
        {
            string[] referenceKey = datas.mytexts[startIndex].Key.Split("_"[0]);
            for (int i = startIndex; i < datas.mytexts.Length; i++)
            {
                string[] actualKey = datas.mytexts[i].Key.Split("_"[0]);
                if (actualKey[0] + "_" + actualKey[1] != referenceKey[0] + "_" + referenceKey[1])
                {
                    return i - startIndex;
                }
            }
        }
        return 0;
    }

    public List<string> GetAWholeDialog(string key)
    {
        string[] actualKeyPart = key.Split("_"[0]);
        List<string> list2trucs = new List<string>();

        for (int i = 0; i < datas.mytexts.Length; i++)
        {
            string[] keyParts = datas.mytexts[i].Key.Split("_"[0]);
            Debug.Log(keyParts[0]);
            if (keyParts[0] == actualKeyPart[0])
            {
                Debug.Log(actualKeyPart[0]);
                for (int x = i; x < datas.mytexts.Length; x++)
                {
                    if (keyParts[1] == actualKeyPart[1])
                    {
                        list2trucs.Add(datas.mytexts[x].Key);
                        Debug.Log(list2trucs.Count);
                    }
                    else return list2trucs;
                }
            }
        }
        return null;
    }

    public void InitLanguage()
    {
        SettingsData settingsSave = GameSaveSystem.LoadSettingsData();
        if (settingsSave.gameLanguage != language) language = settingsSave.gameLanguage;
    }
}
