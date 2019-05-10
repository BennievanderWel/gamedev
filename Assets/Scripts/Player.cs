using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    private float moveX;
    private float moveY;
    private bool isGrounded = true;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;
    private float prevYLoc;
    private float prevXLoc;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        prevXLoc = body.position.x;
        prevYLoc = body.position.y;

    }
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");

        // Face player to the right direction
        if (moveX < 0.0f)
        {
            sprite.flipX = true;
        }
        else if (moveX > 0.0f)
        {
            sprite.flipX = false;
        }

        // Move player on X axis
        body.velocity = new Vector2(moveX * playerSpeed, body.velocity.y);

        // Set animation based on movement
        if (isGrounded)
        {
            if (moveX == 0.0f)
            {
                animator.SetInteger("animation", 0);
            }
            else
            {
                animator.SetInteger("animation", 1);
            }
        }
        else
        {
            animator.SetInteger("animation", 2);
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            body.AddForce(Vector2.up * playerJumpPower);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            animator.SetTrigger("touchGround");
        }
    }
}
