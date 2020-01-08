using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    float musicVol;
    float effectVol;
    string gameLanguage;
    bool fullscreenBool;
    Resolution screenResolution;

    public SettingsData(float newMusicVol, float newEffectVol, string newGameLanguge)
    {
        musicVol = newMusicVol;
        effectVol = newEffectVol;
        gameLanguage = newGameLanguge;
        fullscreenBool = Screen.fullScreen;
        screenResolution = Screen.currentResolution;
    }

}
