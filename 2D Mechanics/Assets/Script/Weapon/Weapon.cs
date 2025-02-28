using System;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firePoint;
    public float bulletVel = 10f;
    public GameObject bulletPrefab;

    ObjectPooler objectPooler;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void Shoot()
    {
        // shooting logic
        // Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// to spawn an object, Instantiate is used
        bulletPrefab = objectPooler.SpawnFromPool2D("Bullet", firePoint.position, firePoint.rotation);

        Vector2 angle = new Vector2(Mathf.Cos(Mathf.Deg2Rad * firePoint.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * firePoint.rotation.eulerAngles.z));
        
        Rigidbody2D bullet = bulletPrefab.GetComponent<Rigidbody2D>();
        
        bullet.linearVelocity = angle*bulletVel;
    }
}
