using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(1, body.velocity.y);
    }
}
