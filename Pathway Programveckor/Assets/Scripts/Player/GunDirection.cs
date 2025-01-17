using UnityEngine;

public class GunDirection : MonoBehaviour
{
    public Rigidbody2D rb;
    public float recoilForce = 25f; // The amount of recoil force

    public Transform gunTransform; // The gun's transform that needs to rotate
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public Transform bulletSpawnPoint; // Where the bullet spawns from
    public float bulletSpeed = 10f; // Speed of the bullet
    public float shootingCooldown = 0.5f; // Time between shots

    public float retardationSpeed = 0.98f;   // Förändringsfaktor som saktar ner spelaren varje FixedUpdate
    public float jumpForce = 10f;

    public float walkSpeed = 10f;

    private float scanRadius = 0.1f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private int walkDirection;

    private Vector2 direction;
    private float lastShotTime = 0f;
    private bool isFacingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastShotTime += 1 * Time.deltaTime;

        AimToCursor();
        if (ShotAllowed() && Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.D) && IsGrounded())
        {
            walkDirection = 1;
        }
        else if (Input.GetKey(KeyCode.A) && IsGrounded())
        {
            walkDirection = -1;
        }
        else { walkDirection = 0; }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x * retardationSpeed, rb.velocity.y);

        float walkVelocity = walkSpeed * walkDirection;

        rb.AddForce(new Vector2(walkVelocity, 0));

        Debug.Log("WalkSpeed=" + walkSpeed);
        Debug.Log("WalkDirection=" + walkDirection);
        Debug.Log(walkVelocity);
    }

    void AimToCursor()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        //Hitta musposition och konvertera till världskoordinater

        if (cursorPos.x < transform.position.x && isFacingRight)                        //Om muspekaren är till vänster om spelaren och vänd mot höger
        {
            Flip();
        }
        else if (cursorPos.x > transform.position.x && !isFacingRight)                  //Om muspekaren är till höger om spelaren och vänd mot vänster
        {
            Flip();
        }

        direction = cursorPos - (Vector2)gunTransform.position;                         //Beräkna vektorn mellan muspekaren och pistolen
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;            //Konvertera direction till radianer och sedan grader

        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));             //Konverterar angle till Quaternion som sedan bestämmer pistolens rotation
    }

    bool ShotAllowed()
    {
        if (lastShotTime >= shootingCooldown)
        {
            return true;
        }
        else { return false; }
    }

    void Shoot()
    {

        lastShotTime = 0;    //Återställ timern

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);  //Instantiata kulan
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();                                      //Hämta kulans rigidbody

        if (bulletRb != null)   //Om kulans rigidbody hittas
        {
            bulletRb.velocity = direction.normalized * bulletSpeed;
        }

        ApplyRecoil();
    }

    void ApplyRecoil()
    {
        rb.AddForce(-direction.normalized * recoilForce, ForceMode2D.Impulse);
    }

    bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, scanRadius, groundLayer))
        {
            return true;
        }
        else { return false; }
    }
    void Flip() //Spegelvänd spelaren och pistolen
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;             //Spegelvänd spelaren
        transform.localScale = scale;


        Vector3 gunScale = gunTransform.localScale;
        gunScale.y = -gunScale.y;   //Spegelvänd pistolen
        gunScale.x = -gunScale.x;   //Spegelvänd pistolen
        gunTransform.localScale = gunScale;


        isFacingRight = !isFacingRight;
    }
}
