using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDOrderItemTest : MonoBehaviour
{
    public Order order;
    public Slider patienceSlider;
    public TextMeshProUGUI nameClient, descOrder;
    public void SetOrder(Order order)
    {
        this.order = order;
        nameClient.text = order.clientName ?? "";
        descOrder.text = order.recipe.description ?? "";
        patienceSlider.maxValue = order.totalPatience;
        patienceSlider.value = order.totalPatience;
    }

    void Update()
    {
        patienceSlider.value = order.currentPatience;
    }
}
