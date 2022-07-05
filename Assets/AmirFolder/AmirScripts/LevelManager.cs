using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int i;
    private void Awake()
    {
        LoadPrefs();
        i = SceneManager.GetActiveScene().buildIndex;
        //string activeScene = SceneManager.GetActiveScene().name;
    }

    // method for levelLoading
    public void FirstStage() 
    {
         // will get the active scene index and call it 'i'
        SceneManager.LoadScene(i + 1); // will load Next Scene
        Debug.Log("Start game");
    }
    //method for retry button after losin
    public void Retry()
    {
        if (SceneManager.GetActiveScene().name == "GameOver1") // if lostscene is 1
        {
            SceneManager.LoadScene(i - 1); // will load level1
            Debug.Log("Starting stage1");
        }

    }
    // method for a return to menu button
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // returns to main menu
        Debug.Log("returning to menu");
    }
    // a method to Exit the game
    public void ExitGame()
    {
        Application.Quit(); // will quit game 
        Debug.Log("Game Closed"); //debug
        PlayerPrefs.Save();
    }

    public void StageWon()
    {
        SceneManager.LoadScene(i + 1);
        Debug.Log("Next stage!");
    }

    public void LoadSave()
    {

    }

    public void DeleteSave()
    {

    }

    public void LoadPrefs()
    {

    }

    public void SavePrefs()
    {

    }
}
