using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] _audioClips;
    [SerializeField]
    private AudioSource[] audiosources;

    void Awake()
    {
        Instance = this;
    }
}


public static class AudioSound
{ 
    public static void Playsound(int index)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(AudioManager.Instance._audioClips[index]);
    }
}
