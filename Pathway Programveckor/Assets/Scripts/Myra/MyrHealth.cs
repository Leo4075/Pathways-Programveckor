using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyrHealth : MonoBehaviour
{
    private int health;
    public int maxHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
        }
    }
}
