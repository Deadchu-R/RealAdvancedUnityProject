using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorTest : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.OnClicked += TurnColor;   //subscribes it to the event
    }

    private void OnDisable()
    {
        LevelManager.OnClicked -= TurnColor;  //unsubscribes it to the event
    }

    void TurnColor()
    {
        Color col = new Color(Random.value, Random.value, Random.value);    //RNG of RGB
        GetComponent<Renderer>().material.color = col;
    }

}
