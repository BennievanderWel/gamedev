using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    SpriteRenderer sprite;
    public Player player;
    float cooldownCounter = -1;

    public float angryDistance = 5;
    public float attackDistance = 1.6f;
    public float attackCooldown = 1;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var targetPos = player.transform.position;

        if (Vector3.Distance(pos, targetPos) < angryDistance && !player.playerStats.isDead)
        {

            if (pos.x > targetPos.x)
            {
                body.velocity = new Vector2(-1, body.velocity.y);
                sprite.flipX = true;
            }
            else
            {
                body.velocity = new Vector2(1, body.velocity.y);
                sprite.flipX = false;
            }


            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
        }

        if (Vector3.Distance(pos, targetPos) < attackDistance && cooldownCounter <= 0 && !player.playerStats.isDead)
        {
            animator.SetTrigger("Attack");

            var body = player.GetComponent<Rigidbody2D>();

            player.DamagePlayer(1);

            cooldownCounter = attackCooldown;
        }

    }
}
