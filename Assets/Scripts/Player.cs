using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    private float moveX;
    private bool isGrounded = true;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;
    public PlayerButton leftButton;
    public PlayerButton rightButton;
    public Button jumpButton;

    bool buttonLeft;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        jumpButton.onClick.AddListener(Jump);
    }

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");

        if (leftButton.IsPressed)
        {
            moveX = -1;
        }
        else if (rightButton.IsPressed)
        {
            moveX = 1;
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
        }
    }

    void Jump()
    {

        // Only jump when player is on the ground
        if (isGrounded)
        {

            animator.SetBool("isJumping", true);

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
}
