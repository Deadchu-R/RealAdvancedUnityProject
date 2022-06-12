using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // bool the check if game is pause 

    public GameObject pauseMenuUI; // setting gameObject "pauseMenuUI" in the script

    public GameObject SaveLoadSubMenu;

    public GameObject OptionsSubMenu;

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
        Debug.Log("Game is Paused"); // debuging
    }

    // a method to ExitGame
    public void ExitGame()
    {
        Application.Quit(); // will exit the game
        Debug.Log("Game Closed"); //Debug
        SceneManager.LoadScene("MainMenu"); // will load the MainMenu Scene
    }

    public void SaveAndLoadSubMenu()
    {
        if (!SaveLoadSubMenu.activeSelf)
        {
            SaveLoadSubMenu.SetActive(true);
        }
        else
        {
            SaveLoadSubMenu.SetActive(false);
        }
    }

    public void OptionSubMenu()
    {
        if (!OptionsSubMenu.activeSelf)
        {
            OptionsSubMenu.SetActive(true);
        }
        else
        {
            OptionsSubMenu.SetActive(false);
        }
    }
}


