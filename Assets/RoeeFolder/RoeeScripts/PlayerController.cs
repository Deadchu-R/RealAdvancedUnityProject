using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{

    private Vector2 move;
    
   [SerializeField] private int HP;
    [SerializeField] private int attackPower;
    [SerializeField] private float moveSpeed = 200;
    [SerializeField] private Rigidbody2D rigidBody;
 

    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        
    }

    private void Move()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);

    }

    private void FixedUpdate()
    {
        // rigidBody.AddForce(move * moveSpeed * Time.deltaTime);
        rigidBody.velocity =  move * moveSpeed * Time.deltaTime;
    }
}
