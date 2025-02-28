using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firePoint;
    public float bulletVel = 10f;
    Vector3 mousePos;
    private Camera mainCam;
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
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Shoot()
    {
        // shooting logic
        // Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// to spawn an object, Instantiate is used
        bulletPrefab = objectPooler.SpawnFromPool2D("Bullet", firePoint.position, firePoint.rotation);
            
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePos - firePoint.position;

        Vector2 unit_direction = direction / direction.magnitude;

        Debug.Log("bullet vector: " + direction);
        Debug.Log("vector length: " + direction.magnitude);

        Rigidbody2D bullet = bulletPrefab.GetComponent<Rigidbody2D>();
        
        bullet.linearVelocity = unit_direction*bulletVel;
    }
}
