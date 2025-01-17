using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private int health;
    public int maxHealth = 10;

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
            SceneManager.LoadScene(3); // loads the death scene on death (make and choose the right scene)
            print("Player died");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mygga"))
        {
            Debug.Log("Hit by mygga");
            health -= 1;
        }
        if (collision.gameObject.CompareTag("Myra"))
        {
            Debug.Log("Hit by myra");
            health -= 3;
        }
    }
}
