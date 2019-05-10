using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    GameObject target;
    SpriteRenderer sprite;
    float cooldownCounter = -1;

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

        if (cooldownCounter > 0) {
            cooldownCounter -= Time.deltaTime;
        }

        if (Vector3.Distance(pos, targetPos) < 1.6 && cooldownCounter <= 0) {
            animator.SetTrigger("Attack");

            var body = target.GetComponent<Rigidbody2D>();

            body.AddForce(new Vector2(0, 1000));
            
            cooldownCounter = 1;
        }

        animator.SetBool("Running", true);
    }
}
