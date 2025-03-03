using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{


    public void OnObjectSpawn()
    {
        PointTable.Instance.AddPoint(-20);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ObjectPooler.Instance.ReturnToPool(this.GameObject());    
    }



}
