using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Object[] _audioClips;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _audioClips = Resources.LoadAll("Sounds", typeof(AudioClip));
    }

    public void PlaySound(AudioClip clip)
    {
        GameObject audio = Instantiate(Resources.Load("GameObject/TemporaryAudioSource"), Camera.main.transform.position, Camera.main.transform.rotation) as GameObject;
        audio.GetComponent<AudioSource>().clip = clip;
        audio.GetComponent<LifeTimer>().StartCoroutine("Timer", clip.length);
        audio.GetComponent<AudioSource>().Play();
    }

    public void PlaySoundTest(string soundName)
    {
        foreach(object sound in _audioClips)
        {
            AudioClip clip = (AudioClip)sound;
            if(clip.name == soundName)
            {
                GameObject audio = Instantiate(Resources.Load("GameObject/TemporaryAudioSource"), Camera.main.transform.position, Camera.main.transform.rotation) as GameObject;
                audio.GetComponent<AudioSource>().clip = clip;
                audio.GetComponent<LifeTimer>().StartCoroutine("Timer", clip.length);
                audio.GetComponent<AudioSource>().Play();
                return;
            }
        }
        /*GameObject audio = Instantiate(Resources.Load("GameObject/TemporaryAudioSource"), Camera.main.transform.position, Camera.main.transform.rotation) as GameObject;
        audio.GetComponent<AudioSource>().clip = clip;
        audio.GetComponent<LifeTimer>().StartCoroutine("Timer", clip.length);
        audio.GetComponent<AudioSource>().Play();*/
    }
}
