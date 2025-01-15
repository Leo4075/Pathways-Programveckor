using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletSpeed = 20f;

    public Camera mainCamera;

    public SimplePlayerMovement PlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;

        PlayerMovement = FindObjectOfType<SimplePlayerMovement>();

        if (PlayerMovement.isFacingRight == true)
        {
            rb.velocity = new Vector2(bulletSpeed, 0);
        }
        if (PlayerMovement.isFacingRight == false)
        {
            rb.velocity = new Vector2(-bulletSpeed, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f)
        {
            Destroy(gameObject);
        }
    }
}
