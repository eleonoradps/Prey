using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 direction;

    [SerializeField] private const int jumpHeight = 5;
    [SerializeField] private float speed = 2;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(direction.x * speed, body.velocity.y);
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKeyDown("space"))
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    //TODO : add this to enemy
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerFeet"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
