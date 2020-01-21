﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    public float musicVol;
    public float effectVol;
    public string gameLanguage;
    public bool fullscreenBool;
    public Vector2 screenResolution;

    public SettingsData(bool defaultFile, float newMusicVol = -20, float newEffectVol = -20, string newGameLanguage = "english", bool newFullscreen = true, Vector2 newResolution = default)   {
        Debug.Log(defaultFile);
        if (defaultFile)
        {
            musicVol = newMusicVol;
            effectVol = newEffectVol;
            gameLanguage = newGameLanguage;
            fullscreenBool = true;
            screenResolution = new Vector2(1920, 1080);

        }
        else
        {
            musicVol = newMusicVol;
            effectVol = newEffectVol;
            gameLanguage = newGameLanguage;
            fullscreenBool = newFullscreen;
            screenResolution = newResolution;
        }
    }

}
