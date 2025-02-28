using System;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private bool isAlive = false;
    public void OnObjectSpawn()
    {
        isAlive = true;
        // mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Vector3 rotation = mousePos - transform.position;

        // float rot



        // bulletRigidBody = GetComponent<Rigidbody2D>();
        // position = transform.position;
        // mousePos = Input.mousePosition;
        // Debug.Log("Bullet pos: " + position);
        // Debug.Log("Mouse pos: " + mousePos);

        // Vector2 bulletDir = mousePos - position;
        // float direction = Mathf.Atan2(bulletDir.x, bulletDir.y)* Mathf.Rad2Deg;
        // bulletRigidBody.linearVelocity = vec;
    }

}
