using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TextData
{
    public string Key;
    public string FR;
    public string EN;
}
[CreateAssetMenu(fileName = "New LocalizationData", menuName = "LocalizationData")]
[System.Serializable]
public class LocalizationData : ScriptableObject
{
    [SerializeField]
    public List<TextData> Data;

    //Dictionary<string, TextData> Dico;

    //void Init();

    //string getTrad(enumLang, Key)
}

