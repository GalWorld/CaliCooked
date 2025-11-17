using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;
    public List<Order> activeOrders;

    //[SerializeField] private int currentOrdersSpawned;
    [SerializeField] private int maxOrdersSpawned;
    public GameObject ClientPrefab;

    public void CompleteOrder(string id)
    {
       ScoreManager.instance.AddToScore();
        var item = activeOrders.Find(order => order.recipe.id == id);
        if (item != null)
            activeOrders.Remove(item);
    }


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        activeOrders = new();
    }

    void Update()
    {
        
    }
}
