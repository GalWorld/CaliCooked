using UnityEngine;
using UnityEngine.Events;

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
    public bool isTiming = false;
    private bool isFinished = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void StartTimer()
    {
        timer = 0f;
        isTiming = true;
        isFinished = false;

        // If the game might already be paused, restore
        Time.timeScale = 1f;
    }

    public void PauseTimer(bool state)
    {
        isTiming = !state;

        if (freezeOnPause)
        {
            Time.timeScale = state ? 0f : 1f;
        }
    }

    private void Update()
    {
        if (!isTiming || isFinished)
            return;

        timer += Time.deltaTime;

        if (timer >= timeGame)
        {
            timer = timeGame;
            isFinished = true;
            isTiming = false;

            if (freezeOnFinish)
            {
                Time.timeScale = 0f;
            }
            onFinishGame?.Invoke();

            Debug.Log("Timer finished! Game frozen.");
        }
    }
}
