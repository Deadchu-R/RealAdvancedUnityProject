using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{

    [SerializeField] private float attackDamage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy 1"))
        {
            enemyBehaviour enemyScript = col.gameObject.GetComponent<enemyBehaviour>(); 
            enemyScript.TakeDamage(attackDamage);
            Debug.Log("damage to enemy:" + attackDamage);
        }
    }
}
