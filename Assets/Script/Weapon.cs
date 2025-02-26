using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firePoint;
    public GameObject bulletPrefab;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // shooting logic
        // Instantiate(bulletPrefab, );// to spawn an object, Instantiate is used
    }
}
