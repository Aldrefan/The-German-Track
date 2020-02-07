using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;

public class Duck : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unPaused;

    void OnEnable()
    {
        paused.TransitionTo(0.01f);
    }

    void OnDisable()
    {
        unPaused.TransitionTo(0.01f);
    }
}
