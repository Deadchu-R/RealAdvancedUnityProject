using UnityEngine;
using DG.Tweening;
using System.IO;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private string _fileLocation;
    private string _saveMe;
    private static bool _gameIsPaused = false; // bool the check if game is pause 
    [Header("PauseMenuElements")]
    [SerializeField] private GameObject pauseMenu; // setting gameObject "pauseMenuUI" in the script
    [SerializeField] private GameObject optionSub;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject keyboardControls;
    [SerializeField] private GameObject controllerControls;

    [SerializeField] private GameObject theUICanvas;
    
    [Header("VolumeElements")]
    [SerializeField] private GameObject masterSliderObject;
    [SerializeField] private GameObject musicSliderObject;
    [SerializeField] private GameObject specialEffectsSliderObject;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider specialEffectsVolumeSlider;
    private string _masterVolume;
    private string _musicVolume;
    private string _theSfxVolume;
    private bool _masterSliderOn = false;
    private bool _musicSliderOn = false;
    private bool _sfxSliderOn = false;


    private bool _optionsOn = false;
    private float animDuration = 1;

[Header("EasterEggs")]
    public int easterEgg = 0;
    [SerializeField] private GameObject easterEgg1;
    [SerializeField] private GameObject easterEgg2;
    [SerializeField] private GameObject easterEgg3;
    [SerializeField] private GameObject easterEgg4;

    void Update()
    {
        EscapeButton(); // method to use Escape Button in order to pause or unPause the game
    }

    // method to pause or unpause the game
    private void EscapeButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // checking if player pressed Escape
        {
            if (_gameIsPaused == true) // if game is paused 
            {
                theUICanvas.SetActive(true);
                Resume(); // will resume game through resume Method
            }
            else if (_gameIsPaused == false) // if game is not paused
            {
                theUICanvas.SetActive(false);
                Pause(); // will pause game through Pause method
            }
        }
    }

    // a method to Resume Game
    public void Resume()
    {
        pauseMenu.SetActive(false); // setting the PauseMenu to not Active
        Time.timeScale = 1; // resuming Time
        _gameIsPaused = false; // setting gameIsPaused bool to false 
        optionSub.transform.DOMoveX(970, animDuration).SetUpdate(true);
        ShutDown();
        _optionsOn = false;
        Debug.Log("Game is Resuming"); // debugging

        easterEgg = 0;
        easterEgg1.SetActive(false);
        easterEgg2.SetActive(false);
        easterEgg3.SetActive(false);
        easterEgg4.SetActive(false);
    }


    // a method to pause game
    private void Pause()
    {
        pauseMenu.SetActive(true); // setting pauseMenu to active
        Time.timeScale = 0; // pausing Time
        _gameIsPaused = true; // setting bool GameIsPause to true
        Debug.Log("Game is Paused"); // debugging
    }


    public void OptionsSubMenu()
    {
        if (_optionsOn == false)
        {
            optionSub.SetActive(true);
            optionSub.transform.DOMoveX(1270, animDuration).SetUpdate(true);
            _optionsOn = true;
        }
        else if (_optionsOn == true)
        {
            masterSliderObject.SetActive(false);
            musicSliderObject.SetActive(false);
            specialEffectsSliderObject.SetActive(false);
            Sequence seq2 = DOTween.Sequence();
            seq2.Append(optionSub.transform.DOMoveX(970, animDuration)).SetUpdate(true);
            seq2.OnComplete(ShutDown);
            _optionsOn = false;
        }
    }
    private void ShutDown()
    {
        optionSub.SetActive(false);
    }

    public void SaveVolumes()
    {
        _masterVolume = masterVolumeSlider.value.ToString();
        _musicVolume = musicVolumeSlider.value.ToString();
        _theSfxVolume = specialEffectsVolumeSlider.value.ToString();

        Debug.Log("MasterVolume: " + _masterVolume);
        Debug.Log("MusicVolume: " + _musicVolume);
        Debug.Log("SFXVolume: " + _theSfxVolume);

        _saveMe = $"{_masterVolume}\n {_musicVolume}\n {_theSfxVolume}\n";
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
        else if (_optionsOn == true)
        {
            SaveVolumes();
            _optionsOn = false;
        }
    }

    public void MasterSlider()
    {
        if (_masterSliderOn == false)
        {
            masterSliderObject.SetActive(true);
            _masterSliderOn = true;
        }
        else if (_masterSliderOn == true)
        {
            masterSliderObject.SetActive(false);
            _masterSliderOn = false;
        }

    }

    public void MusicSlider()
    {
        if (_musicSliderOn == false)
        {
            musicSliderObject.SetActive(true);
            _musicSliderOn = true;
        }
        else if (_musicSliderOn == true)
        {
            musicSliderObject.SetActive(false);
            _musicSliderOn = false;
        }
    }

    public void SpecialEffectsSlider()
    {
        if (_sfxSliderOn == false)
        {
            specialEffectsSliderObject.SetActive(true);
            _sfxSliderOn = true;
        }
        else if (_sfxSliderOn == true)
        {
            specialEffectsSliderObject.SetActive(false);
            _sfxSliderOn = false;
        }
    }

    public void EnterControlMenu()
    {
        pauseMenu.SetActive(false);
        controlMenu.SetActive(true);
    }

    public void ExitControlMenu()
    {
        controlMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void KeyboardControls()
    {
        keyboardControls.SetActive(true);
        controllerControls.SetActive(false);
    }

    public void ControllerControls()
    {
        keyboardControls.SetActive(false);
        controllerControls.SetActive(true);
    }

    public void EasterEgg()
    {
        if (pauseMenu == true)
        {
            if (easterEgg == 0)
            {
                easterEgg++;
                easterEgg1.SetActive(true);
            }
            else if (easterEgg == 1)
            {
                easterEgg++;
                easterEgg2.SetActive(true);
            }
            else if (easterEgg == 2)
            {
                easterEgg++;
                easterEgg3.SetActive(true);
            }
            else if (easterEgg == 3)
            {
                easterEgg++;
                easterEgg4.SetActive(true);
            }
        }
    }

}


