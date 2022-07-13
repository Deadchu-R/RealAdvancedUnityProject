using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private enemyBehaviour enemyParent;
    private bool inRange;
    private Animator anim;
    private bool _canFlip = true;

    private void Awake()
    {
        enemyParent = GetComponentInParent<enemyBehaviour>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        {
            if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack-A1"))
            {
                if (_canFlip)
                {
                 enemyParent.Flip();
                }
                if (!_canFlip)
                {
                    Debug.Log("dont flip");
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
           _canFlip = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
       if(collider.gameObject.CompareTag("Player"))
       {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
            _canFlip = true;
       }


    }
}

