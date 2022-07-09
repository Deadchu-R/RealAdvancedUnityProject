using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skelaton;
    [SerializeField] private int surviveTimer;
    [SerializeField] private int amountOfEnemies;
    private bool isCreated = false;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            //SpawnEnemy();
            InvokeRepeating(nameof(UpdateTimer), 1, 1);

            if (surviveTimer > 0)
            {

                //Invoke(nameof(SpawnEnemy),0f);
                //SpawnEnemy();
                //if ()
                //{

                //}
            }
            
           
            
        }
    }

    private void Update()
    {
        if (!isCreated)
        {

            GameObject skelatonClone = Instantiate(skelaton, transform.position, Quaternion.identity);
            isCreated = true;
            if (skelatonClone)
            {

            }
        }
        
    }

    void SpawnEnemy()
    {
      
    }
    void UpdateTimer()
    {
        surviveTimer--;
                Debug.Log(surviveTimer);
    }
}
