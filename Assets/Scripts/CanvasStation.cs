using UnityEngine;
using UnityEngine.UI;

public class CanvasStation : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private StationController station;
    
    void Update()
    {
        if (station.IsCooking())
        {
            progressImage.fillAmount = station.GetCurrentCoookingTime();
        }
        else
        {
            progressImage.fillAmount = 0;
        }
    }
}
