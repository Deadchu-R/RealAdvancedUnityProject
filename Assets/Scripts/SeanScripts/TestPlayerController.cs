using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class TestPlayerController : MonoBehaviour
{
    private Vector2 move;
    [SerializeField] private bool isJumping = false;

    private float moveHorizontal;
    private float moveVertical;

    [SerializeField] private int HP;
    [SerializeField] private int attackPower;
    [SerializeField] private float moveSpeed = 200;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] public LayerMask groundLayers;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private PolygonCollider2D polyCol;

    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");


        Inputs();
    }

    private void Inputs()
    {
        Move();
        Jump();
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }

        // if (Input.GetAxisRaw("Jump")) 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Platform")
        {
            isJumping = false;
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            // isJumping = true;
            Debug.Log("Exit");
        }
    }

    private void Move()
    {
        // move = new Vector2(Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);

        // move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"));
    }

    private void FixedUpdate()
    {

        // rigidBody.velocity =  move * moveSpeed * Time.deltaTime;

        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rigidBody.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rigidBody.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }
}
