using UnityEngine;

public class LightsMenuController : MonoBehaviour
{
    [SerializeField] private Light proyectorCruces;

    [Header("Color Settings")]
    public Color[] colors;   
    private float colorChangeTime = 2f;

    private int currentIndex = 0;
    private float timer = 0f;

    void Update()
    {
        SwitchColorProjector();
    }

    public void SwitchColorProjector()
    {
        if (proyectorCruces == null || colors.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= colorChangeTime)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % colors.Length; 
            proyectorCruces.color = colors[currentIndex];
        }
    }
}
