using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDOrdersTest : MonoBehaviour
{
    public static HUDOrdersTest instance;
    public GameObject itemPrefab;
    public GameObject panelItems;

    private List<HUDOrderItemTest> items = new();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void CreateOrderUI(Order newOrder)
    {
        var item = Instantiate(itemPrefab, transform);
        item.transform.SetParent(transform);
        var controller = item.GetComponent<HUDOrderItemTest>();
        controller.SetOrder(newOrder);
        items.Add(controller);
    }

    public void DeleteOrderUI(Order order)
    {
        var o = items.FindLast(hudO => hudO.order.instanceId == order.instanceId);
        if (o == null) return;

        items.Remove(o);
        Destroy(o.gameObject);
    }

    public void OnOrderCompleted(Order order)
    {
        DeleteOrderUI(order);
    }

    public void OnOrderExpired(Order order)
    {
        DeleteOrderUI(order);
    }
}
