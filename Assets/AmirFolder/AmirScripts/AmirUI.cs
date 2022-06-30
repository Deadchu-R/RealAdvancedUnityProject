using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmirUI : MonoBehaviour
{
    private Image healthBar;
    public float currentHP;
    private float maxHP = 20f;
    //PlayerController_script Player;




    // Start is called before the first frame update
    void Start()
    {

        //maxHP = ; 
        healthBar = GetComponent<Image>();
        //Player = FindObjectOfType<PlayerController_script>();
    }

    // Update is called once per frame
    void Update()
    {
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

}
