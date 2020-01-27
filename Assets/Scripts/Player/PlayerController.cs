using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 direction;

    [SerializeField] private const int jumpHeight = 5;
    [SerializeField] private float speed = 2;
    Animator playerAnimator;
    bool facingRight = true;
    bool facingLeft = false;
    float horizontalSpeed;
    [SerializeField] float acceleration = 0;
    float maxSpeed = 4;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(direction.x * speed, body.velocity.y);
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        horizontalSpeed = Input.GetAxis("Horizontal")*speed;
        playerAnimator.SetFloat("speed", Mathf.Abs(horizontalSpeed));
        if (Input.GetKeyDown("space"))
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
        }
        attackAcceleration();
        if (horizontalSpeed < 0 && !facingLeft)
        {
            facingLeft = true;
            facingRight = false;
            playerAnimator.transform.Rotate(0, 180, 0);
        }
        if (horizontalSpeed > 0 && !facingRight)
        {
            facingRight = true;
            facingLeft = false;
            playerAnimator.transform.Rotate(0, 180, 0);
        }
    }

    void attackAcceleration()
    {
       if(Input.GetKeyDown("x"))
        {
            speed = 4;
        }
       if(Input.GetKeyUp("x"))
        {
            speed = 3;
        }
          
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        //{
        //    Destroy(gameObject);
        //}
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
