using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    playerHealth Health;
    private Vector2 direction;
    [SerializeField] private int jumpHeight = 5;
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
    float attackDuration;
    [SerializeField] float durationTime;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        Health = GetComponent<playerHealth>();
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
                attackDuration = durationTime;
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
                attackDuration -= Time.deltaTime;
                if(attackDuration<=0)
                {
                    state = State.IDLE;
                }
                if(Input.GetKeyUp("x"))
                {
                    state = State.IDLE;
                }
                angry1.enabled = true;
                angry2.enabled = true;
                break;
        }
        if (Input.GetKeyDown("space") && canJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
        }

        if (horizontalSpeed < 0 && !facingLeft)
        {
            facingLeft = true;
            facingRight = false;
            playerAnimator.transform.Rotate(0, 180, 0);
        }
        if (horizontalSpeed > 0 && !facingRight)
        {
            Debug.Log("AH");
            facingRight = true;
            facingLeft = false;
            playerAnimator.transform.Rotate(0, 180, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy" && attackReady)
        {
            ennemi ennemi = collision.gameObject.GetComponent<ennemi>();
            ennemi.takeDamage();
        }
        else if(collision.gameObject.tag == "enemy" && !attackReady) // le player se prend les degats ici
        {
            Health.TakeDamage();
            playerAnimator.SetBool("isDead", true);
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
}
