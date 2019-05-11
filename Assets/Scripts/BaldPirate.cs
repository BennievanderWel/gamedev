using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    SpriteRenderer sprite;
    Player player;
    float cooldownCounter = -1;

    public float moveSpeed;
    public float angryDistance;
    public float attackDistance;
    public float attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var targetPos = player.transform.position;

        var angry = Vector3.Distance(pos, targetPos) < angryDistance && !player.playerStats.isDead;

        if (angry)
        {


            if (pos.x > targetPos.x)
            {
                body.velocity = new Vector2(-1 * moveSpeed, body.velocity.y);
                sprite.flipX = true;
            }
            else
            {
                body.velocity = new Vector2(1 * moveSpeed, body.velocity.y);
                sprite.flipX = false;
            }
        }

        gameObject.transform.Find("Alert").gameObject.SetActive(angry);

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

        animator.SetBool("Running", Mathf.Abs(body.velocity.x) > 0.25f);
    }
}
