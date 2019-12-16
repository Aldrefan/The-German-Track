using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameSaveSystem
{
    static CameraFollow camScript;
    static ActiveCharacterScript currentCharacters;
    static GameObject actualPlayer;
    static DayNightLight directionalLight;

    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    
    public static void Save()
    {
        GameData saveObject = new GameData(camScript, currentCharacters, actualPlayer, directionalLight);
        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(SAVE_FOLDER + "/save.txt", json);
    }

    public static GameData Load()
    {
        if(File.Exists(SAVE_FOLDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            GameData saveObject = JsonUtility.FromJson<GameData>(saveString);
            return saveObject;
        }
        else
        {
            return null;
        }

    }

    public static void GameDataInput(CameraFollow newCamScript, ActiveCharacterScript newCurrentCharacters, GameObject newPlayer, DayNightLight newDirLight)
    {
        camScript = newCamScript;
        currentCharacters = newCurrentCharacters;
        actualPlayer = newPlayer;
        directionalLight = newDirLight;
    }
}
