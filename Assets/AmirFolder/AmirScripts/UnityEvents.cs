using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEvents : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.OnClicked += Teleport;   //subscribes it to the event
    }

    private void OnDisable()
    {
        LevelManager.OnClicked -= Teleport;  //unsubscribes it to the event
    }

    void Teleport()
    {
        Vector3 pos = transform.position;
        pos.y = Random.Range(.3f, 1.0f);
        transform.position = pos;
    }

}
