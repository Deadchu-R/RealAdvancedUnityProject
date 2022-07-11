using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using DG.Tweening;
using System.IO;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private string _fileLocation;
    private string _saveMe;
    public static bool GameIsPaused = false; // bool the check if game is pause 
    public GameObject pauseMenu; // setting gameObject "pauseMenuUI" in the script
    public GameObject SaveLoadSubMenu;
    public GameObject optionSub;
    public GameObject controlMenu;
    public GameObject keyboardControls;
    public GameObject controllerControls;

    public GameObject masterSlider;
    public GameObject musicSlider;
    public GameObject SFXSlider;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    private string masterVolume;
    private string musicVolume;
    private string SFXVolume;

    private bool optionsOn = false;
    private bool masterSlideOn = false;
    private bool musicSlideOn = false;
    private bool SFXSlideOn = false;

    private float animDuration = 1;


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
    public void EscapeButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // checking if player pressed Escape
        {
            if (GameIsPaused == true) // if game is paused 
            {
                Resume(); // will resume game through resume Method
            }
            else if (GameIsPaused == false) // if game is not paused
            {
                Pause(); // will pause game through Pause method
            }
        }
    }

    // a method to Resume Game
    public void Resume()
    {
        pauseMenu.SetActive(false); // setting the PauseMenu to not Active
        Time.timeScale = 1; // resuming Time
        GameIsPaused = false; // setting gameIsPaused bool to false 
        optionSub.transform.DOMoveX(970, animDuration).SetUpdate(true);
        ShutDown();
        optionsOn = false;
        Debug.Log("Game is Resuming"); // debuging

        easterEgg = 0;
        easterEgg1.SetActive(false);
        easterEgg2.SetActive(false);
        easterEgg3.SetActive(false);
        easterEgg4.SetActive(false);
    }


    // a method to pause game
    public void Pause()
    {
        pauseMenu.SetActive(true); // setting pauseMenu to active
        Time.timeScale = 0; // pausing Time
        GameIsPaused = true; // setting bool GameIsPause to true
        Debug.Log("Game is Paused"); // debugging
    }


    public void OptionsSubMenu()
    {
        if (optionsOn == false)
        {
            optionSub.SetActive(true);
            optionSub.transform.DOMoveX(1270, animDuration).SetUpdate(true);
            optionsOn = true;
        }
        else if (optionsOn == true)
        {
            masterSlider.SetActive(false);
            musicSlider.SetActive(false);
            SFXSlider.SetActive(false);
            Sequence seq2 = DOTween.Sequence();
            seq2.Append(optionSub.transform.DOMoveX(970, animDuration)).SetUpdate(true);
            seq2.OnComplete(ShutDown);
            optionsOn = false;
        }
    }
    private void ShutDown()
    {
        optionSub.SetActive(false);
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

    public void MasterSlide()
    {
        if (masterSlideOn == false)
        {
            masterSlider.SetActive(true);
            masterSlideOn = true;
        }
        else if (masterSlideOn == true)
        {
            masterSlider.SetActive(false);
            masterSlideOn = false;
        }

    }

    public void MusicSlide()
    {
        if (musicSlideOn == false)
        {
            musicSlider.SetActive(true);
            musicSlideOn = true;
        }
        else if (musicSlideOn == true)
        {
            musicSlider.SetActive(false);
            musicSlideOn = false;
        }
    }

    public void SFXSlide()
    {
        if (SFXSlideOn == false)
        {
            SFXSlider.SetActive(true);
            SFXSlideOn = true;
        }
        else if (SFXSlideOn == true)
        {
            SFXSlider.SetActive(false);
            SFXSlideOn = false;
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


