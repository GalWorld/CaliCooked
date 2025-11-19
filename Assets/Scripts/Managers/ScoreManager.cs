using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private float currentScore = 0;

    private void Awake()
    {
        instance = this;        
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
