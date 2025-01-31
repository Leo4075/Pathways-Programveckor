using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyrHealth : MonoBehaviour
{
    private int health;
    public int maxHealth = 5;

    XPmanager XPman;

    // Start is called before the first frame update
    void Start()
    {
        XPman = FindObjectOfType<XPmanager>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        XPman.MyrDeath();
    }
    // This function will be called if the message is received
}
