using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AmirUI : MonoBehaviour
{
    private Image healthBar;
    public float currentHP;
    private float maxHP = 20f;
    [SerializeField] private GameObject gameOver;



    public int easterEgg = 0;
    [SerializeField] private GameObject easterEgg1;
    [SerializeField] private GameObject easterEgg2;
    [SerializeField] private GameObject easterEgg3;
    [SerializeField] private GameObject easterEgg4;
    //PlayerController_script Player;

    void Start()
    {

        //maxHP = ; 
        healthBar = GetComponent<Image>();
        //Player = FindObjectOfType<PlayerController_script>();
    }

    void Update()
    {
        GameOver();
        //currentHP = Player.Health;
        healthBar.fillAmount = currentHP / maxHP;
    }

    public void LostHP()
    {
        currentHP--;
    }

    public void RecoverHP()
    {
        currentHP++;
    }

    public void GameOver()
    {
        if (currentHP <= 0)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("AmirScene");
        Time.timeScale = 1;
        gameOver.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EasterEgg()
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
