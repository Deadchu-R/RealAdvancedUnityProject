using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{

    private bool _isJumping = false;
    private float _moveHorizontal;
    private float _moveVertical;
    private int _remainingJumps;
    private bool _jumpInUse = false;
    
    [SerializeField] private int health;
    [SerializeField] private int attackPower;
    [SerializeField] private float moveSpeed = 200;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private int jumpTimes = 2;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private PolygonCollider2D polyCol;

    private void Awake()
    {
        _remainingJumps = jumpTimes;
    }

    private void FixedUpdate()
    {
        Inputs();
        
    }
    private void Inputs()
    {
        Move();
        Jump();
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
        _moveVertical = Input.GetAxisRaw("Jump") ;

        if (Input.GetAxisRaw("Jump") == 1)
        {
            if (_jumpInUse == false)
            {
                _jumpInUse = true;
            }
            else if (Input.GetAxisRaw("Jump") == -1)
            {
                if (_jumpInUse == false)
                {
                    _jumpInUse = true;
                }
            }
        }
        if (  _moveVertical >= 0.1f )
        {
            if (!_isJumping)
            {
                rigidBody.velocity = new Vector2(0f, 0f);
             rigidBody.AddForce(new Vector2(0f ,_moveVertical * jumpForce), ForceMode2D.Impulse);
                _moveVertical = 0f;
                
            }
            else
            {
                if (_remainingJumps > 0 )
                {
                    _remainingJumps--;
                    rigidBody.velocity = new Vector2(0f, 0f);
                    rigidBody.AddForce(new Vector2(0f ,_moveVertical * jumpForce), ForceMode2D.Impulse);
              
                }
            }

        }

        if (_remainingJumps <= 0)
        {
            Debug.Log("out of jumps");
            // rigidBody.AddForce(new Vector2(0f ,_moveVertical * jumpForce), ForceMode2D.Impulse);      
        }

      

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Platform"))
        {
            _isJumping = false;
            _remainingJumps = jumpTimes;
            Debug.Log($"refilled jumps to {_remainingJumps}");
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
             _isJumping = true;
            Debug.Log("Exit");
        }
    }


}
