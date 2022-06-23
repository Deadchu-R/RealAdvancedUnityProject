using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    
    private bool _grounded = false;
    private bool _moreJump = false;
    private bool _doJump = false;
    private bool _shieldUp = false;
    private float _moveHorizontal;
    private int _remainingJumps;
    private int _currentHP;
 
     
    [SerializeField] private Animator playerAni;
    [SerializeField] private int HP;
    [SerializeField] private int attackPower;
    [SerializeField] private float moveSpeed = 200;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private int jumpTimes = 2;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private PolygonCollider2D polyCol;

    private void Awake()
    {
        _remainingJumps = jumpTimes;
        _currentHP = HP;
    }

    private void Update()
    {
        Testing();
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetButtonDown("Shield"))
        {
            _shieldUp = true;
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
        Actions();
    }
    private void Actions()
    {
        ShieldBlock();
        Move();
        Jump();
    }

    private void ShieldBlock()
    {
        if (_shieldUp == true)
        {
          
         playerAni.SetTrigger("Shield");
         _shieldUp = false;
         Debug.Log("shield");
            
        }

    }

    private void Move()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        if (_moveHorizontal > 0.1f || _moveHorizontal < -0.1f)
        {
            rigidBody.AddForce(new Vector2(_moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
    }
    private void Jump()
    {
        if (_doJump == true )
        {
            if (_grounded)
            {
                rigidBody.velocity = new Vector2(0f, 0f);
                rigidBody.AddForce(new Vector2(0f , jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                if (_moreJump == true)
                {
                    rigidBody.velocity = new Vector2(0f, 0f);
                    rigidBody.AddForce(new Vector2(0f ,jumpForce), ForceMode2D.Impulse);
                    Debug.Log($"remaining jumps: {_remainingJumps}");
                }
            }
            _doJump = false;
        }

 

        if (_remainingJumps <= 0)
        {
            Debug.Log("out of jumps");
             
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
        _currentHP -= dmg;
        Debug.Log($"got:{dmg} dmg");

        if (_currentHP <= 0)
        {
            Died();
        }
    }

    private void Died()
    {
        Debug.Log("Died");
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
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
    }


}
