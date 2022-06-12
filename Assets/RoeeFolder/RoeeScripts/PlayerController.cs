using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{

   [SerializeField] private int HP;
    [SerializeField] private int attackPower;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private Rigidbody2D rigidBody;
    private int currentHP;
    private float horizontalMove = 0;
    private Vector3 MoveVector3;
    private Vector2 moveDirection = Vector2.zero;

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
        float MoveX = Input.GetAxis("Horizontal");
        moveDirection = new Vector2(MoveX, 0).normalized;

    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, 0);
    }
}
