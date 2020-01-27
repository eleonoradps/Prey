using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private int enemyDamage = 1;

    private float startPositionX = -2.57f;
    private float startPositionY = -3.65f;
    
    // Start is called before the first frame update
    void Start()
    {
        startPositionX = gameObject.transform.position.x;
        startPositionY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TakeDamage()
    {
        currentHealth -= enemyDamage;
    }
    private void RespawnPlayer()
    {
        transform.position = new Vector3(startPositionX, startPositionY);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage();
            RespawnPlayer();
        }
    }
}
