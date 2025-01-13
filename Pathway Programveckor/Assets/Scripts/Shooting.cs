using UnityEngine;

public class GunController : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shoot();
        }
    }

    void shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPos);
    }
}
