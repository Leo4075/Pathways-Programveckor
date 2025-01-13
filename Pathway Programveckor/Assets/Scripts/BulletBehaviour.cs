using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletSpeed = 20f; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(bulletSpeed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.gravityScale = 10;
    }
}
