using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour, IPooledObject
{



    [SerializeField] 
    public float timeCreated;

    public void OnObjectSpawn()
    {
        timeCreated = Time.time;
        Debug.Log("Target created in: " + transform.position);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {   
        if (collider.gameObject.CompareTag("Bullet"))
        {
            SpawnandAddPoint();
        }
    }

    void SpawnandAddPoint()
    {
        Debug.Log("Time passed: " + (Time.time - timeCreated));
        TargetSpawner.Instance.Score(Time.time - timeCreated);
        SpawnNewTarget();
    }

    void SpawnNewTarget()
    {
        TargetSpawner.Instance.SpawnTarget();
    }
    
}


