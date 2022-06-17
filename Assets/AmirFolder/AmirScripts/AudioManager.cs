using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void MasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void MusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        
    }

}
