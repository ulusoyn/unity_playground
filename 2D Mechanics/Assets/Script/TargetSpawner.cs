using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    


    #region Singleton

    public static TargetSpawner Instance;
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

        // Generate random position within bounds
        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);
        Vector2 target_pos = new Vector2(randomX, randomY);

        ObjectPooler.Instance.SpawnFromPool2D("Target", target_pos, Quaternion.Euler(0,0,0));
    }

    

}

