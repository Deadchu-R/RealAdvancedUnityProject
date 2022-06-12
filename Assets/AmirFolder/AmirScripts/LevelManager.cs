using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // method for levelLoading
    public void LevelLoader() 
    {
        int i = SceneManager.GetActiveScene().buildIndex; // will get the active scene index and call it 'i'
        SceneManager.LoadScene(i + 1); // will load Next Scene
    }
    //method for retry button after losin
    public void Retry()
    {
        if (SceneManager.GetActiveScene().name == "GameOver") // if lostscene is 1
        {
            SceneManager.LoadScene("Roee's Scene 1"); // will load level1
        }
    }
    // method for a return to menu button
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // returns to main menu
    }
    // a method to Exit the game
    public void ExitGame()
    {
        Application.Quit(); // will quit game 
        Debug.Log("Game Closed"); //debug
    }
}
