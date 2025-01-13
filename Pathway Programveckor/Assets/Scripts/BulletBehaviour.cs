using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletSpeed = 20f;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(bulletSpeed, 0);

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the object is outside the screen
        if (viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f)
        {
            Destroy(gameObject);
            // You can perform actions when the object is off-screen, like disabling it or triggering an event
        }
    }
}
