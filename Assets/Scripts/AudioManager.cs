using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private string _fileLocation;
    private string _saveMe;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [FormerlySerializedAs("SFXVolumeSlider")] [SerializeField] private Slider sfxVolumeSlider;
    private string _masterVolume;
    private string _musicVolume;
    private string _sfxVolume;
    private bool _optionsOn = false;

    private void Start()
    {
        _fileLocation = Application.persistentDataPath + "/volume.txt";
        LoadVolumes();
    }

    public void MasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        _masterVolume = sliderValue.ToString();
        
    }

    public void MusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        _musicVolume = sliderValue.ToString();
    }

    public void SFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        _sfxVolume = sliderValue.ToString();
        
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
            string sfxVolumeValue = allSavedVolumes[2];
            masterVolumeSlider.value = float.Parse(masterVolumeValue);
            musicVolumeSlider.value = float.Parse(musicVolumeValue);
            sfxVolumeSlider.value = float.Parse(sfxVolumeValue);
        }
    }

    public void SaveVolumes()
    {
        _masterVolume = masterVolumeSlider.value.ToString();
        _musicVolume = musicVolumeSlider.value.ToString();
        _sfxVolume = sfxVolumeSlider.value.ToString();

        Debug.Log("MasterVolume: " + _masterVolume);
        Debug.Log("MusicVolume: " + _musicVolume);
        Debug.Log("SFXVolume: " + _sfxVolume);

        _saveMe = $"{_masterVolume}\n {_musicVolume}\n {_sfxVolume}\n";
        File.Delete(_fileLocation);
        File.AppendAllText(_fileLocation, _saveMe);
        Debug.Log("Saved: \n" + _saveMe);
    }

    public void OptionsOnOff()
    {
        if (_optionsOn == false)
        {
            _optionsOn = true;
        }
        else if (_optionsOn)
        {
            SaveVolumes();
            _optionsOn = false;
        }
    }

}
