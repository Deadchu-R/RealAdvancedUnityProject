using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpUI : MonoBehaviour
{

    [SerializeField] private int jumpCount = 2;
    [SerializeField] private Text jumpCounter;
    [SerializeField] private int enemyCount = 5;
    [SerializeField] private Text enemyCounter;
    [SerializeField] private int loseTimerSecs = 50; // used for loseCondition
    [SerializeField] private Text loseTimerText;
    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("updateTimer", 1, 1);
        enemyCounter.text = "Remaining enemys: " + enemyCount;
        jumpCounter.text = "Jumps : " + jumpCount;
        //jumpCount = ;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCounter.text = "Remaining enemys: " + enemyCount;
        jumpCounter.text = "Jumps : " + jumpCount;
        JumpTest();
        EnemyCount();
        //jumpCount = 2;
    }
                                                                   
    public void JumpTest()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount != 0)
            {
                Debug.Log("Jumping");
                jumpCount--;
            }
            else if (jumpCount == 0)
            {
                Debug.Log("No jumps remaining");
                jumpCount =+ 2;
            }
        }
        return;
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
        loseTimerText.text = ":" + loseTimerSecs;
    }
}
