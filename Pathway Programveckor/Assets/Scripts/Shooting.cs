using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform gunTransform; // The gun's transform that needs to rotate
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public Transform bulletSpawnPoint; // Where the bullet spawns from
    public float bulletSpeed = 10f; // Speed of the bullet
    public float shootingCooldown = 0.5f; // Time between shots

    private float lastShotTime = 0f;

    void Update()
    {
        // Get the position of the cursor
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the gun to the cursor
        Vector2 direction = cursorPos - (Vector2)gunTransform.position;

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
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
    }
}
