using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Speed at which the player moves horizontally
    public float jumpForce = 10f;  // Force applied when the player jumps
    public Transform groundCheck;  // Transform used to check if the player is grounded
    public LayerMask groundLayer;  // Layer to detect if the player is on the ground

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Jump if the player presses the space key and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Apply jump force
        }
    }

    void FixedUpdate()
    {
        // Get movement input (A for left, D for right)
        float horizontal = 0f;

        if (Input.GetKey(KeyCode.A))  // Move left when A is pressed
        {
            horizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D))  // Move right when D is pressed
        {
            horizontal = 1f;
        }

        // Apply horizontal movement (no need for smoothing)
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }
}
    