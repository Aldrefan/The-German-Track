﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    public float dialogSpeed;
    public float musicVol;
    public float effectVol;
    public string gameLanguage;
    public bool fullscreenBool;
    public Vector2 screenResolution;
    public bool isBlAndWht;

    public SettingsData(bool defaultFile,float newDialogSpeed=0.04f, float newMusicVol = -20, float newEffectVol = -20, string newGameLanguage = "english", bool newFullscreen = true, Vector2 newResolution = default, bool newIsBlAndWht= false)
    {
        if (defaultFile)
        {
            dialogSpeed = newDialogSpeed;
            musicVol = newMusicVol;
            effectVol = newEffectVol;
            gameLanguage = newGameLanguage;
            fullscreenBool = true;
            screenResolution = new Vector2(1920, 1080);
            isBlAndWht = newIsBlAndWht;
        }
        else
        {
            dialogSpeed = newDialogSpeed;
            musicVol = newMusicVol;
            effectVol = newEffectVol;
            gameLanguage = newGameLanguage;
            fullscreenBool = newFullscreen;
            screenResolution = newResolution;
            isBlAndWht = newIsBlAndWht;

        }
    }

}
