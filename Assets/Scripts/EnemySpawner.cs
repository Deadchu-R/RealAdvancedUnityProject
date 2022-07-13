using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int surviveTimer;
    [SerializeField] private int amountOfEnemies;
    [SerializeField] private GameObject[] enemyCount;
    [SerializeField] private int maxEnemiesInArray = 50;
    [SerializeField] private int enemiesAtStage1;
    [SerializeField] private int stages = 2;
    private int _enemiesAfterStage1;
    private int _stage = 1;
    private bool _isCreated = false;
    private bool _playerAtSpawner = false;
    private bool _spawnerOn = true;

    private void Start()
    {
        _enemiesAfterStage1 = enemiesAtStage1;
        enemyCount = new GameObject[_enemiesAfterStage1];
        InvokeRepeating(nameof(SpawnEnemy),1,2);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _playerAtSpawner = true;
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
            _playerAtSpawner = false;
        }
    }

    private void Update()
    {
       
        
    }

    void SpawnEnemy()
    {
        if (_playerAtSpawner)
        {
            if (_spawnerOn)
            {
                
                if (_stage <= stages)
                {
                    for (int i = 0; i <=  _enemiesAfterStage1; i++)
                    {
                           enemyCount = new GameObject[_enemiesAfterStage1];
                        enemyCount[i] = Instantiate(enemy, transform.position, Quaternion.identity) as GameObject;
                        if (enemyCount[i] == null)
                        {
                           break;
                        }
                    }
                    _enemiesAfterStage1++;
                    _stage++;
                }

                _spawnerOn = false;
            }


           

            // var skeletonClone = Instantiate(skeleton, transform.position, Quaternion.identity);


            // skeletonClone.transform.parent = this.gameObject.transform;
            // Enemy[] enemies = GetComponentInChildren<enemyBehaviour>();
            // enemies = GetComponentsInChildren<Enemy>();
            // amountOfEnemies = enemies.Length;


        }

    }
    void UpdateTimer()
    {
        surviveTimer--;
                Debug.Log(surviveTimer);
    }
}

