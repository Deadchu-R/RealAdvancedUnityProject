using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpUI : MonoBehaviour
{


    [SerializeField] private int enemyCount = 5;
    [SerializeField] private Text enemyCounter;
    [SerializeField] private int loseTimerSecs = 50; // used for loseCondition
    [SerializeField] private Text loseTimerText;
    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("updateTimer", 1, 1);
        enemyCounter.text = "Remaining enemys: " + enemyCount;

        //jumpCount = ;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCounter.text = "Remaining enemys: " + enemyCount;

        EnemyCount();
        //jumpCount = 2;
    }
                                                                   


    public void EnemyCount()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (enemyCount != 0)
            {
                Debug.Log("1 less enemy!");
                enemyCount--;
            }
            else if (enemyCount == 0)
            {
                Debug.Log("no enemys remaining");
                
            }
            return;
        }
    }

    void updateTimer() // method for updating in-game timer
    {
        loseTimerSecs--;
        loseTimerText.text =  ":" + loseTimerSecs;
    }
}
