// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.PlayerLoop;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _grounded = true;
    private bool _canMove = true;
   // private bool _pauseGravity = false;
    private bool _moreJump;
    private bool _doJump ;
    private bool _shieldUp;
    private bool _attack;
    private float _moveHorizontal;
    private int _remainingJumps;
    private int _currentHealth;
    private int _attackNum;
    private float _moveSpeed = 1;


    [Header("PlayerStats:")]
    [SerializeField] private int health;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private int attackDamage;
    [SerializeField] private int jumpTimes = 2;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float fallMulti;

    [Header("Wall Jump Properties:")] 
    [SerializeField] private float wallJumpTime = 0.2f;
    [SerializeField] private float wallSlideSpeed = 0.3f;
    [SerializeField] private float wallDistance = 0.5f;
    private bool _isWallSliding = false;
    private RaycastHit2D _wallCheckHit;
    private float _jumpTime;
    
    [Header("RigidBodies and Colliders:")]
    [SerializeField] private Rigidbody2D rigidBody;

    [Header("Animations Properties:")]
    [SerializeField] private Animator playerAni;
    [SerializeField] private AnimationClip[] attackAnimations;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    private static readonly int AttackState = Animator.StringToHash("AttackState");
    private static readonly int Shield = Animator.StringToHash("Shield");
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Awake()
    {
        
        _remainingJumps = jumpTimes;
        _currentHealth = health;
    }

    private void Update()
    {
        Testing();
        if (_canMove)
        {
         Inputs();
        }
    }

    private void Inputs()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (_attackNum >= attackAnimations.Length)
            {
                _attackNum = 1;
            }
            else
            {
                _attackNum++;
            }
            _attack = true;
        }
        if (Input.GetButtonDown("Shield"))
        {
            _shieldUp = true;
        }
        if (Input.GetButton("Horizontal"))
        {
            playerAni.SetBool(Walking, true);
        }
        if (!Input.GetButton("Horizontal"))
        {
            playerAni.SetBool(Walking, false);
        }
        if (Input.GetButton("Run"))
        {
            _moveSpeed = runSpeed;
            playerAni.SetBool(Running, true);
        }
        if (!Input.GetButton("Run"))
        {
            _moveSpeed = walkSpeed;
            playerAni.SetBool(Running, false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (_remainingJumps > 0)
            { 
                _moreJump = true;
                _remainingJumps--;
            }
            else
            {
                _moreJump = false;
            }
            _doJump = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
         Actions();
        }
    }
    private void Actions()
    {
        Attack(_attackNum);
        ShieldBlock();
        Move();
        Jump();
    }

    private void Attack(int attackNumber)
    {
        if (_attack)
        {
            playerAni.SetTrigger(AttackTrigger);
            playerAni.SetInteger(AttackState, attackNumber);
            _attack = false;
        }

    }

    private void ShieldBlock()
    {
        if (_shieldUp)
        {
            playerAni.SetTrigger(Shield);
         _shieldUp = false;
         Debug.Log("shield");
        }

    }

    private void Move()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        if (_moveHorizontal > 0.1f || _moveHorizontal < -0.1f)
        {
            rigidBody.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
        }
        
    }
    private void Jump()
    {
        if (_doJump)
        {
            if (_grounded)
            {
                rigidBody.velocity = new Vector2(0f, 0f);
                rigidBody.AddForce(new Vector2(0f , jumpForce), ForceMode2D.Impulse);
                playerAni.SetTrigger(Jumping);
            }
            else
            {
                if (_moreJump)
                {
                    // rigidBody.gravityScale = 1;
                    rigidBody.velocity = new Vector2(0f, 0f);
                    rigidBody.AddForce(new Vector2(0f ,jumpForce), ForceMode2D.Impulse);
                  playerAni.SetTrigger(Jumping);
                }
            }
            _doJump = false;
        }
        

        if (rigidBody.velocity.y < 0)
        {
            rigidBody.gravityScale += fallMulti;
        } 
        else if (rigidBody.velocity.y >= 0)
        {
            rigidBody.gravityScale = 1;
        }
    }
    

    private void Testing()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Damage(5);
        }
    }

    public void Damage(int dmg)
    {
        playerAni.SetTrigger(Hit);
        _currentHealth -= dmg;
        Debug.Log($"got:{dmg} dmg");

        if (_currentHealth <= 0)
        {
            Died();
        }
    }

    private void Died()
    {
        Debug.Log("Died");
        playerAni.SetTrigger(Dead);
        _canMove = false;
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
            if (col.gameObject.CompareTag("StickWall"))
            {
                rigidBody.gravityScale = 0;
                Debug.Log("sticky");
            }
            
            if (col.gameObject.CompareTag("Platform"))
            {
                _grounded = true;
                _remainingJumps = jumpTimes;
                Debug.Log($"refilled jumps to {_remainingJumps}");
            }
    }
    

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
             _grounded = false;
        }
        // if (col.gameObject.CompareTag("StickWall"))
        // {
        //     rigidBody.gravityScale = 1;
        //     Debug.Log("notSticky");
        // }
    }


}
