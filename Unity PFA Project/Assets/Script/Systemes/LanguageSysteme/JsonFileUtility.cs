using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class JsonFileUtility
{

    public static string LoadJsonFromFile(string path, bool isResource)
    {
        if(isResource)
        {
            return LoadJsonAsResource(path);
        }
        else return LoadJsonAsExternalResource(path);
    }
    public static string LoadJsonAsResource(string path)
    {
        string jsonFilePath = path.Replace(".json", "");
        TextAsset loadedJsonFile = Resources.Load<TextAsset>(jsonFilePath);
        return loadedJsonFile.text;
    }
    
    public static string LoadJsonAsExternalResource(string path)
    {
        path = Application.persistentDataPath + "/" + path;
        if(!File.Exists(path))
        {
            return null;
        }

        StreamReader reader = new StreamReader(path);
        string response = "";
        while(!reader.EndOfStream)
        {
            response += reader.ReadLine();
        }
        return response;
    }
}
