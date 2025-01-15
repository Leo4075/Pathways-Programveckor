using UnityEngine;

public class GunDirection : MonoBehaviour
{
    public Rigidbody2D rb;
    public float recoilForce = 25f; // The amount of recoil force
    public float recoilCooldown = 0.5f; // Cooldown time between recoils (in seconds)

    public Transform gunTransform; // The gun's transform that needs to rotate
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public Transform bulletSpawnPoint; // Where the bullet spawns from
    public float bulletSpeed = 10f; // Speed of the bullet
    public float shootingCooldown = 0.5f; // Time between shots

    private float lastShotTime = 0f;
    private float lastRecoilTime = 0f; // Timer to track the last recoil time

    private bool isFacingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get the position of the cursor
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the cursor is on the left or right side of the screen
        if (cursorPos.x < transform.position.x && isFacingRight)
        {
            Flip();  // Flip when cursor is on the left
        }
        else if (cursorPos.x > transform.position.x && !isFacingRight)
        {
            Flip();  // Flip when cursor is on the right
        }

        // Calculate the direction from the gun to the cursor
        Vector2 direction = cursorPos - (Vector2)gunTransform.position;

        // Check if enough time has passed since the last recoil
        if (Input.GetKey(KeyCode.Mouse0) && Time.time - lastRecoilTime >= recoilCooldown)
        {
            // Trigger recoil
            rb.AddForce(-direction * recoilForce, ForceMode2D.Impulse);
            lastRecoilTime = Time.time; // Reset recoil cooldown timer
        }

        // Calculate the angle the gun needs to rotate to
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the gun to face the cursor
        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Check if the player clicks the mouse and enough time has passed since the last shot
        if (Input.GetButton("Fire1") && Time.time - lastShotTime > shootingCooldown)
        {
            ShootBullet(direction);
            lastShotTime = Time.time; // Update the last shot time
        }
    }

    void ShootBullet(Vector2 direction)
    {
        // Instantiate the bullet at the bullet spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Set the bullet's velocity in the direction of the cursor
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction.normalized * bulletSpeed;
        }
    }

    // The Flip method that changes the direction the player is facing
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;  // Negate the x scale to flip
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }
}
