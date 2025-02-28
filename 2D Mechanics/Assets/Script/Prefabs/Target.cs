using UnityEngine;

public class Target : MonoBehaviour, IPooledObject
{

    public PointTable pointTable;
    ObjectPooler objectPooler;

    [SerializeField] public float timeCreated;

    public void Awake()
    {
        objectPooler = ObjectPooler.Instance;
        pointTable = PointTable.Instance;
    }

    public void OnObjectSpawn()
    {
        timeCreated = Time.time;

        Debug.Log("Target created in: " + transform.position);
    }
    void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Camera cam = Camera.main;
            
            // Get screen bounds in world space
            Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            // Generate random position within bounds
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);
            Vector2 target_pos = new Vector2(randomX, randomY);

            pointTable.AddPointFromTime(Time.time - timeCreated);
            objectPooler.SpawnFromPool2D("Target", target_pos, Quaternion.Euler(0,0,0));
        }
    }
}


