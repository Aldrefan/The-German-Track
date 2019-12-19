using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

[Serializable]
public class ObjectSaved
{
    public JsonSave[] savesArray = new JsonSave[SaveGameManager.NB_SAVES];
    public int selectedSaveID = 0;

    public ObjectSaved()
    {
        for (int i = 0; i < savesArray.Length; i++)
        {
            savesArray[i] = new JsonSave();
        }
    }
}

[Serializable]
public class JsonSave
{
    public string language = "french";
}

public static class SaveGameManager
{
    public const int NB_SAVES = 1;

    private static readonly string SAVE_FILE_PATH = Path.Combine(Application.persistentDataPath, "save");

    private static ObjectSaved currentSaves = new ObjectSaved();

    static SaveGameManager()
    {
        Load();
    }

    public static void Save()
    {
        String stringSave = JsonUtility.ToJson(currentSaves);

        FileStream stream = new FileStream(SAVE_FILE_PATH, FileMode.Create, FileAccess.Write);

        byte[] savedBytes = Encoding.UTF8.GetBytes(stringSave);
        stream.Write(savedBytes, 0, savedBytes.Length);
        stream.Close();
        stream.Dispose();
            
        Debug.Log("Content file saved = " + stringSave.ToString());
    }

    public static void Load()
    {
        try
        {
            if (File.Exists(SAVE_FILE_PATH))
            {
                FileStream stream = new FileStream(SAVE_FILE_PATH, FileMode.Open, FileAccess.Read);

                int streamLength = (int)stream.Length;
                byte[] bytesArray = new byte[streamLength];
                stream.Read(bytesArray, 0, streamLength);
                stream.Close();
                stream.Dispose();

                string contentFile = Encoding.UTF8.GetString(bytesArray);

                Debug.Log("Content file loaded = " + contentFile);
                    
                currentSaves = JsonUtility.FromJson<ObjectSaved>(contentFile);
            }
            else
            {
                //Debug.Log("File " + SAVE_FILE_PATH + " doesn't exist");

                currentSaves = new ObjectSaved();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Error opening file " + SAVE_FILE_PATH + " : " + e.Message);

            currentSaves = new ObjectSaved();
        }
    }

    public static JsonSave[] GetAllSaves()
    {
        return currentSaves.savesArray;
    }

    public static void DeleteAllSaves()
    {
        currentSaves = new ObjectSaved();
        Save();
            
        Debug.Log("All saves are successfully deleted");
    }

    public static JsonSave GetCurrentSave()
    {
        return currentSaves.savesArray[currentSaves.selectedSaveID];
    }

    public static void ResetCurrentSave()
    {
        currentSaves.savesArray[currentSaves.selectedSaveID] = new JsonSave();
        Save();
            
        Debug.Log("Save id = " + currentSaves.selectedSaveID + " successfully reset");
    }

    public static int GetselectedSaveID()
    {
        return currentSaves.selectedSaveID;
    }

    public static JsonSave SelectSave(int id)
    {
        if(id == currentSaves.selectedSaveID)
        {
            return GetCurrentSave();
        }
        else if (id >= 0 && id < currentSaves.savesArray.Length)
        {
//            currentSaves.savesArray[id].isEmpty = false;
            currentSaves.selectedSaveID = id;
            Save();
                
            Debug.Log("Select save id = " + id);
        }
        else
        {
            Debug.LogError("Save id = " + id + " not found");
        }

        return GetCurrentSave();
    }
}