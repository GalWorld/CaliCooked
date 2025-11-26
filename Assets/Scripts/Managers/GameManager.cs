using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("References")]
    public GameObject player;

    [Header("Timer Settings")]
    public float timeGame = 10f;

    [Header("Pause Settings")]
    public bool freezeOnPause = false;     // If true, timeScale = 0 when paused

    [Header("Finish")]
    public bool freezeOnFinish = false;
    public UnityEvent onFinishGame;

    public float timer = 0f;
    [SerializeField] private TextMeshProUGUI timerText;
    public bool isTiming = false;
    private bool isFinished = false;
    [Header("Feedbacks")]
    public UIPanelAnimator Feedback;
    public TextMeshProUGUI scoreText;
    public ScoreManager scoreManager;

    private SceneManager sceneManager;

    private void Awake()
    {
        instance = this;
        StartTimer();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sceneManager = GetComponent<SceneManager>();
    }

    public void StartTimer()
    {
        timer = timeGame;  // Inicia full
        isTiming = true;
        isFinished = false;
        Time.timeScale = 1f;
    }

    public void PauseTimer(bool state)
    {
        isTiming = !state;

        if (freezeOnPause == true)
        {
            Time.timeScale = state ? 0f : 1f;
        }
    }

    private void Update()
    {
        UpdateTimerUI();

        if (!isTiming || isFinished)
            return;

        // Gilberto Santarosa
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 0f;
            isFinished = true;
            isTiming = false;

            GameFinished();
            onFinishGame?.Invoke();

            Debug.Log("Timer reached ZERO! Game finished.");
        }

        
    }
    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        // Format: MM:SS
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    public void GameFinished()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        scoreText.text = "Puntaje: " + scoreManager.GetCurrentScore().ToString();
        Feedback.ActivateGO();
        DOVirtual.DelayedCall(5f, () =>
        {
            sceneManager.LoadSceneInPause("MainScene");
            
        
        });


    }
    public float GetCurrentTime()
    {
        return timer;
    }
}
