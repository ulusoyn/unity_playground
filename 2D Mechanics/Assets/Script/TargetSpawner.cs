using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #region Singleton

    public static TargetSpawner Instance;
    [SerializeField] private float checkRadius = 0.5f;
    private void Awake()
    {
        Instance = this;
    }

    #endregion


    void Start()
    {
        Invoke("SpawnTarget", 0.5f);
    }

    public void Score(float time)
    {
        PointTable.Instance.AddPointFromTime(time);
    }

    public void SpawnTarget()
    {
        Camera cam = Camera.main;
        // Get screen bounds in world space
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Vector2 target_pos = new Vector3(0,0); // initialization

        bool is_found = false;
        while (!is_found)
        {
            // Generate random position within bounds
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);
            target_pos = new Vector2(randomX, randomY);


            if (!Physics2D.OverlapCircle(target_pos, checkRadius))
            {
                is_found = true;
            }
        }

        ObjectPooler.Instance.SpawnFromPool2D("Target", target_pos, Quaternion.Euler(0,0,0));
    }

    

}

