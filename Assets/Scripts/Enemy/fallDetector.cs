using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallDetector : MonoBehaviour
{
    bool grounded = false;
    ennemi ennemi;
    // Start is called before the first frame update
    void Start()
    {
        ennemi = GetComponentInParent<ennemi>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OUI");
        if (grounded && collision.gameObject.tag == "ground")
        {
            grounded = false;
            ennemi.changeDirection();
        }
    }
}
