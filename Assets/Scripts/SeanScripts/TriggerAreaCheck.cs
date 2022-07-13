using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private enemyBehaviour enemyParant;

    
    private void Awake()
    {
        enemyParant = GetComponentInParent<enemyBehaviour>();


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))

        {
            gameObject.SetActive(false);
            enemyParant.target = collider.transform;
            enemyParant.inRange = true;
            enemyParant.hotZone.SetActive(true);        }
    }
}
