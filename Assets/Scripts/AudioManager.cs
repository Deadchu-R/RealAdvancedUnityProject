using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private string _fileLocation;
    private string _saveMe;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    private string masterVolume;
    private string musicVolume;
    private string SFXVolume;
    private bool optionsOn = false;

    private void Start()
    {
        _fileLocation = Application.persistentDataPath + "/volume.txt";
        LoadVolumes();
    }

    public void MasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        masterVolume = sliderValue.ToString();
        //Debug.Log("Mastervolume set to : " + masterVolume);
    }

    public void MusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        musicVolume = sliderValue.ToString();
    }

    public void SFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        SFXVolume = sliderValue.ToString();
        
    }

    private void LoadVolumes()
    {
        Debug.Log("loaded" + File.ReadAllText(Application.persistentDataPath + "/volume.txt"));

        if (!File.Exists(_fileLocation))
        {
            return;
        }
        else if (File.Exists(_fileLocation))
        {
            string[] allSavedVolumes = File.ReadAllLines(_fileLocation);
            string masterVolumeValue = allSavedVolumes[0];
            string musicVolumeValue = allSavedVolumes[1];
            string SFXVolumeValue = allSavedVolumes[2];
            masterVolumeSlider.value = float.Parse(masterVolumeValue);
            musicVolumeSlider.value = float.Parse(musicVolumeValue);
            SFXVolumeSlider.value = float.Parse(SFXVolumeValue);
        }
    }

    public void SaveVolumes()
    {
        masterVolume = masterVolumeSlider.value.ToString();
        musicVolume = musicVolumeSlider.value.ToString();
        SFXVolume = SFXVolumeSlider.value.ToString();

        Debug.Log("MasterVolume: " + masterVolume);
        Debug.Log("MusicVolume: " + musicVolume);
        Debug.Log("SFXVolume: " + SFXVolume);

        _saveMe = $"{masterVolume}\n {musicVolume}\n {SFXVolume}\n";
        File.Delete(_fileLocation);
        File.AppendAllText(_fileLocation, _saveMe);
        Debug.Log("Saved: \n" + _saveMe);
    }

    public void OptionsOnOff()
    {
        if (optionsOn == false)
        {
            optionsOn = true;
        }
        else if (optionsOn == true)
        {
            SaveVolumes();
            optionsOn = false;
        }
    }

    void Update()
    {

    }
}
