using UnityEngine;

public class GunController : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    public float cooldownTimer = 1f;
    private float cooldown;
    private bool isCooldownOn = false;

    private void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(cooldown);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(isCooldownOn == false)
            {
                shoot();
            }
        }
        if(isCooldownOn == true)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                isCooldownOn = false;
                cooldown = cooldownTimer;
            }
        }
    }

    void shoot()
    {
        isCooldownOn = true;
        Instantiate(bulletPrefab, bulletSpawnPos);
    }
}
