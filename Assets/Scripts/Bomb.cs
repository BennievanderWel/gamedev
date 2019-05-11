using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer sprite;
    private CircleCollider2D collider;
    private Rigidbody2D body;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    void Explode()
    {
        Quaternion rotation = Quaternion.AngleAxis(0, Vector3.forward);
        transform.rotation = rotation;
        Destroy(body);
        Destroy(collider);
        animator.SetTrigger("explode");
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}