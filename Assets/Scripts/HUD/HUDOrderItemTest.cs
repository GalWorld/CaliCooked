using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDOrderItemTest : MonoBehaviour
{
    public Order order;
    public Slider patienceSlider;
    public TextMeshProUGUI nameClient, descOrder;
    private AudioSource audioSource;

    public void SetOrder(Order order)
    {
        audioSource = GetComponent<AudioSource>();
        this.order = order;
        nameClient.text = order.clientName ?? "";
        descOrder.text = order.recipe.description ?? "";
        patienceSlider.maxValue = order.totalPatience;
        patienceSlider.value = order.totalPatience;
        audioSource.clip = order.recipe.audio;
        audioSource.Play();
    }

    void Update()
    {
        patienceSlider.value = order.currentPatience;
    }
}
