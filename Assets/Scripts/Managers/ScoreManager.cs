using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private float currentScore = 0;

    private void Awake()
    {
        instance = this;        
    }

    private void Update()
    {
        if (ScoreManager.instance != null)
        {
            pointsText.text = ScoreManager.instance.GetCurrentScore().ToString();
        }
    }

    public void AddToScore(float value = 50f)
    {
        currentScore += value;
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }
}
