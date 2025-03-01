using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{


    public void OnObjectSpawn()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Target"))
        {
            ObjectPooler.Instance.ReturnToPool(this.GameObject());
        }
    }

}
