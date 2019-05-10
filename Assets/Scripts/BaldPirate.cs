using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(TestAnim());
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) {
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
