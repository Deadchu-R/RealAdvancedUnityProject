using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int i;
    private void Awake()
    {

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
    //method for retry button after losing
    public void Retry()
    {
        if (SceneManager.GetActiveScene().name == "GameOver1") // if loseScene is 1
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
    

    
    
    // Unity Event V
    public delegate void ClickAction();     //delegate - function containers
    public static event ClickAction OnClicked;  //will not work and may cause memory leak if it has no subscribers

    private void OnGUI()   //adds a button on the UI screen
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))  //position & text of the button
        {
            if (OnClicked != null)    //checks if it has subscribers attached to the event.
                OnClicked();        // will activate all subscribers that are attached to it
        }
    }

}
