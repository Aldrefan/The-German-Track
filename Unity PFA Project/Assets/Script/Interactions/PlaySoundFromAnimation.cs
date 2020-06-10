using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFromAnimation : MonoBehaviour
{
    public void Animation_PlaySound(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.PlayOneShot(clip);
        StartCoroutine(Timer(clip.length));
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(GetComponent<AudioSource>());
    }
}
