using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    GameObject target;

    bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player");

        StartCoroutine(TestAnim());
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var targetPos = target.transform.position;

        if (pos.x > targetPos.x) {
            body.velocity = new Vector2(-1, body.velocity.y);
        } else {
            body.velocity = new Vector2(1, body.velocity.y);
        }

        animator.SetBool("Running", isRunning);
    }

    IEnumerator TestAnim()
    {
        yield return new WaitForSeconds(2);
        isRunning = true;
    }
}
