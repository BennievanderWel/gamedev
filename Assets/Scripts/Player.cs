﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // TODO: Split this code into seperate scripts, eg. player_move, player_shoot, etc
    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    private float moveX;
    private bool isGrounded = true;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;
    public GameObject bomb;

    //Shoot
    public int shootPower;
    public int shootAngle;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");

        // Basic touch movement
        if (Input.touchCount > 0)
        {
            var input = Input.GetTouch(0);
            var relX = input.position.x / Screen.width;

            if (relX > 0.5)
            {
                moveX = 1;
            }
            else
            {
                moveX = -1;
            }
        }

        // Face player to the right direction
        if (moveX < 0.0)
        {
            sprite.flipX = true;
        }
        else if (moveX > 0.0)
        {
            sprite.flipX = false;
        }

        // Move player on X axis
        body.velocity = new Vector2(moveX * playerSpeed, body.velocity.y);

        // Set animation based on movement
        animator.SetFloat("moveX", Mathf.Abs(moveX));

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            animator.SetBool("isJumping", true);
        }

        // Shoot
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerShoot();
        }
    }

    void Jump()
    {
        // Only jump when player is on the ground
        if (isGrounded)
        {
            body.AddForce(Vector2.up * playerJumpPower);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Modify state and animation when touching the floor
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void PlayerShoot()
    {
        // Create a bomb
        bomb = Instantiate(bomb, new Vector2(sprite.flipX ? body.position.x - 1.5f : body.position.x + 1.4f, body.position.y), transform.rotation);
        // Add a fraction from the player velocity to the bomb
        bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(body.velocity.x * 0.6f, body.velocity.y * 0.5f);
        // Throw the bomb in the direction de player is facing
        bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(sprite.flipX ? shootPower * -1 : shootPower, shootAngle));
    }
}
