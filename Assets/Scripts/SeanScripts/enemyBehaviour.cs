using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    private bool IsDead = false;
    public float attackDistance;  //minimum distance for attack
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timer; // timer for cooldown attacks
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    public GameObject hotZone;
    public GameObject triggerArea;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; // check if Player is in range;
    private Animator anim;
    private float distance;  // Store the distance betwn enemy & player
    private bool attackMode;
    private bool canAttack = false;
    //private bool _HeavyAttack = false;
    private int attackNum = 1;
    [SerializeField] private int attackMade = 3;
    private bool cooling; // Check if Enemy is cooling after attack
    private float intTimer;
    [SerializeField] private float destroyTimer;

    [SerializeField] private int HP = 5;
    private int currentHP;
    [SerializeField] private float attackDamage;



    private void Awake()
    {
        currentHP = HP;
        SelectTarget();

        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsDead)
        {
            if (!attackMode)
            {
                Move();
            }

        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack-A1"))
        {
            SelectTarget();
        }




        if (inRange)
        {
            EnemyLogic();
        }

        if (currentHP <= 0)
        {
            Died();

        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(5);
        }
    }


    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        Debug.Log($"Enemy current HP is {currentHP}");
        anim.SetBool("isHit", true);

    }

    private void Died()
    {
        IsDead = true;
        //anim.SetBool("IsDead", true);
        //anim.SetBool("Attack1", false);
        //anim.SetBool("Attack2", false);
        anim.SetBool("canWalk", false);
        anim.SetBool("canAttack", false);
        anim.Play("Enemy_Dead");
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
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
            canAttack = true;
            Attack();
            //Invoke(nameof(Attack),1f);
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("CanAttack", false);

            //if (attack2Active==false)
            //{
            //    anim.SetBool("Attack1", false);

            //}
            //else
            //    anim.SetBool("Attack2", false);

        }

    }


    void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack-A1"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        Debug.Log("Enterd attack zone");
        timer = intTimer; //Reset Timer when Player enter attack range
        attackMode = true;  //To check if Enemy can still attack or not
        anim.SetBool("canWalk", false);
        anim.SetBool("canAttack", true);
        Debug.Log("Exit attack zone");
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("canAttack", false);

        //if (attack2Active == false)
        //{
        //    anim.SetBool("Attack1", false);

        //}
        //else
        //    anim.SetBool("Attack2", false);

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

        if (distanceToLeft > distanceToRight)
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
