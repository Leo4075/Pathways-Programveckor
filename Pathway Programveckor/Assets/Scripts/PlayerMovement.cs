using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundlayer;

    [SerializeField] private Transform gunTransform;  // Reference to the gun's transform
    private bool isFacingRight = true;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }

        // Update character's facing direction based on the gun's position
        UpdateFacingDirection();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }

    // Method to update character's facing direction based on the gun's position relative to the player
    private void UpdateFacingDirection()
    {
        // Check if the gun is to the left or right of the player
        if (gunTransform.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (gunTransform.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    // Flip the character's direction
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localscale = transform.localScale;
        localscale.x *= -1f; // Flip the local scale
        transform.localScale = localscale;
    }
}
