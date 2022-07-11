using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeleton;
    [SerializeField] private int surviveTimer;
    [SerializeField] private int amountOfEnemies;
    private int _stage = 1;
    private bool isCreated = false;
    private bool _spawnerOn = false;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),1,2);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _spawnerOn = true;
            Debug.Log("spawner is on");
            //SpawnEnemy();
            // InvokeRepeating(nameof(UpdateTimer), 1, 1);

            if (surviveTimer > 0)
            {

                // InvokeRepeating(nameof(SpawnEnemy),1,1);
                //SpawnEnemy();
                //if ()
                //{

                //}
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _spawnerOn = false;
        }
    }

    private void Update()
    {
        // if (!isCreated)
        // {
        //
        //     GameObject skeletonClone = Instantiate(skeleton, transform.position, Quaternion.identity);
        //     isCreated = true;
        //     if (skeletonClone)
        //     {
        //
        //     }
        // }
        
    }

    void SpawnEnemy()
    {
        if (_spawnerOn)
        {
            if (_stage == 1)
            {
             var skeletonClone = Instantiate(skeleton, transform.position, Quaternion.identity);

                skeletonClone.transform.parent = this.gameObject.transform;
                // Enemy[] enemies = GetComponentInChildren<enemyBehaviour>();
                // enemies = GetComponentsInChildren<Enemy>();
                // amountOfEnemies = enemies.Length;
            }

        }

    }
    void UpdateTimer()
    {
        surviveTimer--;
                Debug.Log(surviveTimer);
    }
}

internal class Enemy
{
}
