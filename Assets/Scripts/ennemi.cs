using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemi : MonoBehaviour
{
    [SerializeField]float timer;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    enum State
    {
        ALIVE,
        DEAD,
        DESTROY
    }
    State state = State.ALIVE;
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.ALIVE:
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

    public void takeDamage()
    {
        state = State.DEAD;
    }
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerSides"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
