﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public static class GameSaveSystem
{
    public static bool gameToLoad;

    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    //GameDataValue
    static GameObject camObject;
    static ActiveCharacterScript currentCharacters;
    static GameObject actualPlayer;
    static DayNightLight directionalLight;
    static CarnetGoal goalFrame;
    static StickersGivenToPNJ stickerManager;
    static Transform boardTransform;

    //SettingsData
    static AudioMixer musicMixer;
    static AudioMixer effectMixer;
    static LanguageManager langManager;
    static Menu menuSettings;

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void SaveGameData()
    {
        GameData saveObject = new GameData(camObject, currentCharacters, actualPlayer, directionalLight, goalFrame, stickerManager, boardTransform);
        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(SAVE_FOLDER + "/gameSave.tgt", json);
    }

    public static GameData LoadGameData()
    {
        if (File.Exists(SAVE_FOLDER + "/gameSave.tgt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/gameSave.tgt");
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


        SettingsData settingObject = new SettingsData(false, langManager.dialogSpeed, musicVol, effectVol, langManager.language, Screen.fullScreen, new Vector2(Screen.currentResolution.width, Screen.currentResolution.height), menuSettings.isBlackAndWhite);
        string json = JsonUtility.ToJson(settingObject);

        File.WriteAllText(SAVE_FOLDER + "/settingsSave.tgt", json);
    }

    public static SettingsData LoadSettingsData()
    {
        if (File.Exists(SAVE_FOLDER + "/settingsSave.tgt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/settingsSave.tgt");
            SettingsData newSettingsSave = JsonUtility.FromJson<SettingsData>(saveString);
            return newSettingsSave;
        }
        else
        {
            SettingsData defaultSettingsSave = new SettingsData(true);
            if (File.Exists(SAVE_FOLDER))
            {
                string json = JsonUtility.ToJson(defaultSettingsSave);
                File.WriteAllText(SAVE_FOLDER + "/settingsSave.tgt", json);
            }
            return defaultSettingsSave;
        }
    }


    public static string ReturnLevelName()
    {
        string saveString = File.ReadAllText(SAVE_FOLDER + "/gameSave.tgt");
        GameData saveObject = JsonUtility.FromJson<GameData>(saveString);
        string gameActualLevel = saveObject.currentLevel;
        return gameActualLevel;
    }

    public static void GameDataInput(GameObject mainCamera, ActiveCharacterScript newCurrentCharacters, GameObject newPlayer, DayNightLight newDirLight, CarnetGoal newGoalFrame, StickersGivenToPNJ newStickerManager, Transform newBoardCanvas)
    {
        camObject = mainCamera;
        currentCharacters = newCurrentCharacters;
        actualPlayer = newPlayer;
        directionalLight = newDirLight;
        goalFrame = newGoalFrame;
        stickerManager = newStickerManager;
        boardTransform = newBoardCanvas;
    }

    public static void SettingsDataInput(AudioMixer newMusicMixer, AudioMixer newEffectMixer, LanguageManager newLangManager, Menu newMenuSettings)
    {
        musicMixer = newMusicMixer;
        effectMixer = newEffectMixer;
        langManager = newLangManager;
        menuSettings = newMenuSettings;
    }
}

