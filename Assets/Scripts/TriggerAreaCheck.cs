using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private enemyBehaviour _enemyParent;

    
    private void Awake()
    {
        _enemyParent = GetComponentInParent<enemyBehaviour>();


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))

        {
            gameObject.SetActive(false);
            _enemyParent.target = col.transform;
            _enemyParent.inRange = true;
            _enemyParent.hotZone.SetActive(true);        }
    }
}
