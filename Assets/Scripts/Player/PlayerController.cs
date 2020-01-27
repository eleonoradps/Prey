﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 direction;
    [SerializeField] private const int jumpHeight = 5;
    bool canJump = false;
    [SerializeField] private float speed = 2;
    Animator playerAnimator;
    bool facingRight = true;
    bool facingLeft = false;
    float horizontalSpeed;
    [SerializeField] float acceleration = 0;
    float maxSpeed = 4;

    float attackTimer;
    [SerializeField] float attackTime;
    bool attackReady = false;
    [SerializeField] SpriteRenderer angry1;
    [SerializeField] SpriteRenderer angry2;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        facingRight = true;
        facingLeft = false;
    }
enum State
    {
        IDLE,
        MOVING,
        CHARGING,
        DAMAGING_CHARGE
    }
    State state = State.IDLE;
    void FixedUpdate()
    {
        body.velocity = new Vector2(direction.x * speed, body.velocity.y);
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        horizontalSpeed = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("speed", Mathf.Abs(horizontalSpeed));
        switch(state)
        {
            case State.IDLE:
                attackTimer = attackTime;
                attackReady = false;
                angry2.enabled = false;
                angry1.enabled = false;
                speed = 2.5f;
                if(horizontalSpeed !=0)
                {
                    state = State.MOVING;
                }
                break;
            case State.MOVING:
                if (Input.GetKeyDown("x"))
                {
                    state = State.CHARGING;
                }
                break;
            case State.CHARGING:
                speed = 3.75f;
                attackTimer -= Time.deltaTime;
                if(attackTimer<=0)
                {
                    state = State.DAMAGING_CHARGE;
                }
                if (Input.GetKeyUp("x"))
                {
                    state = State.IDLE;
                }
                break;
            case State.DAMAGING_CHARGE:
                attackReady = true;
                angry1.enabled = true;
                angry2.enabled = true;
                if(Input.GetKeyUp("x"))
                {
                    state = State.IDLE;
                }
                break;
        }
        if (Input.GetKeyDown("space") && canJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
        }
        Debug.Log("1");
        if (horizontalSpeed < 0 && !facingLeft)
        {
            facingLeft = true;
            facingRight = false;
            playerAnimator.transform.Rotate(0, 180, 0);
            Debug.Log("IC");
        }
        if (horizontalSpeed > 0 && !facingRight)
        {
            Debug.Log("AH");
            facingRight = true;
            facingLeft = false;
            playerAnimator.transform.Rotate(0, 180, 0);
        }
        //attackAcceleration();
    }

    //void attackAcceleration()
    //{
    //   if(Input.GetKeyDown("x"))
    //    {
    //        speed = 4;
    //    }
    //   if(Input.GetKeyUp("x"))
    //    {
    //        speed = 3;
    //    }
          
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ennemy" && attackReady)
        {

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //TODO : add this to enemy
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerFeet"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}