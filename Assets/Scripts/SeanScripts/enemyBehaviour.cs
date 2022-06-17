using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    #region Public var
    
    public float attackDistance;  //minimum distance for attack
    public float moveSpeed;
    public float timer; // timer for cooldown attacks
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; // check if Player is in range;
    public GameObject hotZone;
    public GameObject triggerArea;

    #endregion

    #region Private var
    private Animator anim;
    private float distance;  // Store the distance betwn enemy & player
    private bool attackMode;
    private bool cooling; // Check if Enemy is cooling after attack
    private float intTimer;
    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!attackMode)
        {
            Move();
        }

        if(!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack-A1"))
        {
            SelectTarget();
        }

        

        
        if (inRange)
        {
            EnemyLogic();
        }
    }


    




    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }

    }


    void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("attack-A1"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter attack range
        attackMode = true;  //To check if Enemy can still attack or not

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);

    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if(timer<= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }




   

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }


    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft>distanceToRight)
        {
            target = leftLimit;

        }
        else
        {
            target = rightLimit;
        }

        Flip();

    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;

        }
        else
        {
            rotation.y = 0;
        }
        transform.eulerAngles = rotation;
    }
}
