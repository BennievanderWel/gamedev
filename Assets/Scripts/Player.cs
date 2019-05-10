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

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        prevYLoc = body.position.y;

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

        // Controls
        if (isGrounded)
        {

        }
        if (Input.GetButtonUp("Jump"))
        {
            Jump();
        }

        // Animation
        if (moveX != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        Debug.Log(body.position.y);

        // Direction
        if (moveX < 0.0f)
        {
            sprite.flipX = true;
        }
        else if (moveX > 0.0f)
        {
            sprite.flipX = false;
        }

        // Physics
        body.velocity = new Vector2(moveX * playerSpeed, body.velocity.y);
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
        }
    }

    // =======
    // Helpers
    // =======

    bool IsMovingUp()
    {
        return body.position.y > prevYLoc;
    }

    bool IsMovingDown()
    {
        return body.position.y < prevYLoc;
    }
}
