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
        DEAD
    }
    State state = State.ALIVE;
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.ALIVE:
                timer -= Time.deltaTime;
                // Debug.Log(timer);
                if (timer <= 0)
                {
                    WaveManager waveManager = FindObjectOfType<WaveManager>();
                    waveManager.ennemiDeath();
                    state = State.DEAD;
                }
                break;
            case State.DEAD:
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerSides"))
        {
            Destroy(gameObject);
        }
    }
}
