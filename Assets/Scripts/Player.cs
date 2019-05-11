using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int playerhealth = 1;
    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    private float moveX;
    private bool isGrounded = true;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;
    private PlayerStats playerStats;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerStats = new PlayerStats();
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

    public void DamagePlayer(int damage)
    {
        playerStats.health -= damage;
        if(playerStats.health <= 0)
        {
            // start death animation
            // load GameOver Screen
            Debug.Log("dead");
          
        }
    }
}
