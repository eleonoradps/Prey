using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 4;
    [SerializeField] private int currentHealth;
    private int enemyDamage = 1;
    Animator anim;
    private float startPositionX = -2.57f;
    private float startPositionY = -3.65f;

    [SerializeField] private TextMeshProUGUI textPlayerHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        startPositionX = gameObject.transform.position.x;
        startPositionY = gameObject.transform.position.y;
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        textPlayerHealth.text = currentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage() //ici le player prend vraiment des degats
    {
        currentHealth -= enemyDamage;
        textPlayerHealth.text = currentHealth.ToString();

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("DeathScene");
        }

    }
    public void RespawnPlayer()
    {
        anim.SetBool("isDead", false);
        transform.position = new Vector3(startPositionX, startPositionY);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        TakeDamage();
    //        RespawnPlayer();
    //    }
    //}
}
