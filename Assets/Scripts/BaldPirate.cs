using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    GameObject target;
    SpriteRenderer sprite;

    bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var targetPos = target.transform.position;

        if (pos.x > targetPos.x) {
            body.velocity = new Vector2(-1, body.velocity.y);
            sprite.flipX = true;
        } else {
            body.velocity = new Vector2(1, body.velocity.y);
            sprite.flipX = false;
        }

        animator.SetBool("Running", true);
    }
}
