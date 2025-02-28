using UnityEngine;



public class PointTable : MonoBehaviour
{
    [SerializeField]
    public float Score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #region Singleton

    public static PointTable Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public void AddPointFromTime(float time_spent)
    {
        Score += 4 * (10 - time_spent); // if last longer then 10s, point will decrease
    }
}
