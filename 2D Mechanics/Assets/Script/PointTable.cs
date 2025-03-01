using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PointTable : MonoBehaviour
{
    [SerializeField]
    public TMP_Text scoreText;
    public float Score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #region Singleton

    public static PointTable Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    void Start()
    {
        Score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = Score.ToString();
    }

    public void AddPointFromTime(float time_spent)
    {
        Score += 4 * (10 - time_spent); // if last longer then 10s, point will decrease
        UpdateScoreText();
    }
}
