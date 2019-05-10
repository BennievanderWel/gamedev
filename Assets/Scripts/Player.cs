using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int playerSpeed = 10;
    private bool facingRight = false;
    public int playerJumpPower = 1250;
    private float moveX;
    private bool isGrounded = true;

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        // Controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Direction
        if (moveX < 0.0f && !facingRight)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight)
        {
            FlipPlayer();
        }

        // Physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
            isGrounded = false;
        }
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isGrounded = true;
        }
    }
}
