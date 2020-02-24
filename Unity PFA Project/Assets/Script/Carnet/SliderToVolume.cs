using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderToVolume : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] AudioMixer mixer = null;
    [SerializeField] string mixerGroup = null;









    // BASE FUNCTIONS
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per graphic frame
    void Update()
    {
        UpdateVolume();
    } 





    // UPDATE VOLUME
    public void UpdateVolume()
    {
        mixer.SetFloat(mixerGroup, slider.value);
    }
}
