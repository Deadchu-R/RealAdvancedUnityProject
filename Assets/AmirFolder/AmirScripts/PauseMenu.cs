using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // bool the check if game is pause 
    public GameObject pauseMenuUI; // setting gameObject "pauseMenuUI" in the script
    public GameObject SaveLoadSubMenu;
    public Transform optionSubMenu;
    public GameObject optionSub;
    public GameObject subMenu;

    public GameObject masterSlider;
    public GameObject musicSlider;
    public GameObject SFXSlider;

    private bool optionsOn = false;
    private bool masterSlideOn = false;
    private bool musicSlideOn = false;
    private bool SFXSlideOn = false;

    private float animDuration = 1;

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
        pauseMenuUI.SetActive(false); // setting the PauseMenu to not Active
        Time.timeScale = 1; // resuming Time
        GameIsPaused = false; // setting gameIsPaused bool to false 
        Debug.Log("Game is Resuming"); // debuging
    }


    // a method to pause game
    public void Pause()
    {
        pauseMenuUI.SetActive(true); // setting pauseMenu to active
        Time.timeScale = 0; // pausing Time
        GameIsPaused = true; // setting bool GameIsPause to true
        Debug.Log("Game is Paused"); // debugging
    }


    public void OptionsSubMenu()
    {
        if (optionsOn == false)
        {
            optionSub.SetActive(true);
            optionSubMenu.DOMoveX(1270, animDuration).SetUpdate(true);
            optionsOn = true;
        }
        else if (optionsOn == true)
        {
            masterSlider.SetActive(false);
            musicSlider.SetActive(false);
            SFXSlider.SetActive(false);
            Sequence seq2 = DOTween.Sequence();
            seq2.Append(optionSubMenu.DOMoveX(970, animDuration)).SetUpdate(true);
            seq2.OnComplete(ShutDown);
            optionsOn = false;
        }
    }
    private void ShutDown()
    {
        optionSub.SetActive(false);
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

}


