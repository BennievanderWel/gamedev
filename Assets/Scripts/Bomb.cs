using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer sprite;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Explode()
    {
        Quaternion rotation = Quaternion.AngleAxis(0, Vector3.forward);
        transform.rotation = rotation;
        Destroy(GetComponent<Rigidbody2D>());
        animator.SetTrigger("explode");
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
    // 0.50968998670578
    // 0.324863582849503
}