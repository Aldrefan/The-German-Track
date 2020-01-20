using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public static class GameSaveSystem
{
    public static bool gameToLoad;

    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    //GameDataValue
    static CameraFollow camScript;
    static ActiveCharacterScript currentCharacters;
    static GameObject actualPlayer;
    static DayNightLight directionalLight;

    //SettingsData
    static AudioMixer musicMixer;
    static AudioMixer effectMixer;
    static LanguageManager langManager;



    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void SaveGameData()
    {
        GameData saveObject = new GameData(camScript, currentCharacters, actualPlayer, directionalLight);
        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(SAVE_FOLDER + "/gameSave.txt", json);
    }

    public static GameData LoadGameData()
    {
        if (File.Exists(SAVE_FOLDER + "/gameSave.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/gameSave.txt");
            GameData saveObject = JsonUtility.FromJson<GameData>(saveString);
            return saveObject;
        }
        else
        {
            return null;
        }

    }

    public static void SaveSettingsData()
    {
        float musicVol = 1;
        float effectVol = 1;
        musicMixer.GetFloat("musicVolume", out musicVol);
        effectMixer.GetFloat("fxVolume", out effectVol);


        SettingsData settingObject = new SettingsData(false, musicVol, effectVol, langManager.language, Screen.fullScreen, new Vector2(Screen.currentResolution.width, Screen.currentResolution.height));
        string json = JsonUtility.ToJson(settingObject);

        File.WriteAllText(SAVE_FOLDER + "/settingsSave.txt", json);
    }

    public static SettingsData LoadSettingsData()
    {
        if (File.Exists(SAVE_FOLDER + "/settingsSave.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/settingsSave.txt");
            SettingsData newSettingsSave = JsonUtility.FromJson<SettingsData>(saveString);
            return newSettingsSave;
        }
        else
        {
            
            SettingsData defaultSettingsSave = new SettingsData(true);
            string json = JsonUtility.ToJson(defaultSettingsSave);
            File.WriteAllText(SAVE_FOLDER + "/settingsSave.txt", json);
            return defaultSettingsSave;
        }
    }


    public static string ReturnLevelName()
    {
        string saveString = File.ReadAllText(SAVE_FOLDER + "/gameSave.txt");
        GameData saveObject = JsonUtility.FromJson<GameData>(saveString);
        string gameActualLevel = saveObject.currentLevel;
        return gameActualLevel;
    }

    public static void GameDataInput(CameraFollow newCamScript, ActiveCharacterScript newCurrentCharacters, GameObject newPlayer, DayNightLight newDirLight)
    {
        camScript = newCamScript;
        currentCharacters = newCurrentCharacters;
        actualPlayer = newPlayer;
        directionalLight = newDirLight;
    }

    public static void SettingsDataInput(AudioMixer newMusicMixer, AudioMixer newEffectMixer, LanguageManager newLangManager)
    {
        musicMixer = newMusicMixer;
        effectMixer = newEffectMixer;
        langManager = newLangManager;
    }
}

