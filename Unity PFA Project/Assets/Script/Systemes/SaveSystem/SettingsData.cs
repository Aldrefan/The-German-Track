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

    public SettingsData(float newMusicVol, float newEffectVol, string newGameLanguage)
    {
        musicVol = newMusicVol;
        effectVol = newEffectVol;
        gameLanguage = newGameLanguage;
        fullscreenBool = Screen.fullScreen;
        screenResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
    }

}
