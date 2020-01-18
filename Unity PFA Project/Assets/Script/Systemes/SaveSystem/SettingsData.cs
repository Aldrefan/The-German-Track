using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    public float musicVol;
    public float effectVol;
    public string gameLanguage;
    public bool fullscreenBool;
    public Vector2 screenResolution;

    public SettingsData(bool defaultFile = true, float newMusicVol = 0.5f, float newEffectVol = 0.5f, string newGameLanguage = "english", bool newFullscreen = true, Vector2 newResolution = default)   {
        Debug.Log(defaultFile);
        if (defaultFile)
        {
            musicVol = newMusicVol;
            effectVol = newEffectVol;
            gameLanguage = newGameLanguage;
            fullscreenBool = true;
            screenResolution = new Vector2(1280, 720);

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
