using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideRespawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform leftSide;
    [SerializeField] Transform rightSide;
    Vector2 leftRespawn;
    Vector2 rightRespawn;


    // Start is called before the first frame update
    void Start()
    {
        leftRespawn = new Vector2(leftSide.position.x, player.transform.position.y);
        rightRespawn = new Vector2(rightSide.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player" && gameObject.tag == "rightSide")
        {
            player.transform.position = leftRespawn;
        }
        else
        {
            player.transform.position = rightRespawn;
        }
    }
}
