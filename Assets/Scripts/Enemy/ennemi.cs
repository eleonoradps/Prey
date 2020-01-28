using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemi : MonoBehaviour
{
    [SerializeField]float timer;
    SpriteRenderer sprite;
    Rigidbody2D body;
    Vector2 direction;
   [SerializeField] float speed;
    Animator animator;
    float movement = 0;
    bool canMove = false;
    bool switchDirection = false;
    bool startedSwitching = false;
    bool facingRight = true;
    bool facingLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    enum State
    {
        ALIVE,
        DEAD,
        DESTROY
    }
    State state = State.ALIVE;

    private void FixedUpdate()
    {
        body.velocity = new Vector2(direction.x * speed, body.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.ALIVE:
                if (!startedSwitching&& canMove)
                {
                    direction = new Vector2(1f, body.velocity.y);
                    animator.SetBool("run", true);
                }
                if (switchDirection && facingRight)
                {
                    Debug.Log("ICI");
                    direction = new Vector2(-1f, body.velocity.y);
                    facingRight = false;
                    facingLeft = true;
                    switchDirection = false;
                    startedSwitching = true;
                    sprite.flipX = true;
                }
                if (switchDirection && facingLeft)
                {
                    direction = new Vector2(1f, body.velocity.y);
                    facingRight = true;
                    facingLeft = false;
                    switchDirection = false;
                    startedSwitching = true;
                    sprite.flipX = false;
                }
                break;
            case State.DEAD:
                WaveManager waveManager = FindObjectOfType<WaveManager>();
                waveManager.ennemiDeath();
                state = State.DESTROY;
                break;
            case State.DESTROY:
                Destroy(gameObject);
                break;
        }
    }
    private void OnBecameInvisible()
    {
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        waveManager.ennemiDeath();
        Destroy(gameObject);
    }
    public void takeDamage()
    {
        state = State.DEAD;
    }
   public void changeDirection()
    {
        switchDirection = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            canMove = true;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerSides"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
