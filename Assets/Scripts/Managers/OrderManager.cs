using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    public int ordersCompleted = 0;
    public List<Order> activeOrders = new();

    [Header("�rdenes")]
    [SerializeField] private int maxOrdersSpawned = 3;


    [SerializeField] private SOClients clients;

    [Header("Spawn aleatorio (segundos)")]
    [SerializeField] private float minSpawnTime = 3f;
    [SerializeField] private float maxSpawnTime = 8f;

    private Coroutine spawnRoutine;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        if (activeOrders == null)
            activeOrders = new List<Order>();

        spawnRoutine = StartCoroutine(SpawnOrdersRoutine());
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = activeOrders.Count - 1; i >= 0; i--)
        {
            Order order = activeOrders[i];
            order.Tick(dt);
        }
    }


    public void CompleteOrder(string recipeId)
    {
        var order = activeOrders.FindLast(o => o.recipe.id == recipeId);
        if (order == null)
            return;

        ScoreManager.instance.AddToScore();

        order.OnExpired -= HandleOrderExpired;
        ordersCompleted++;
        activeOrders.Remove(order);

        // Aqu� podr�as notificar al HUD:
        // HUDOrderManager.instance.OnOrderCompleted(order);
        //HUDOrdersTest.instance.OnOrderCompleted(order);
    }

    private IEnumerator SpawnOrdersRoutine()
    {
        while (true)
        {
            float wait = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(wait);

            if (activeOrders.Count >= maxOrdersSpawned)
                continue;

            SpawnOrder();
        }
    }

    private void SpawnOrder()
    {
        if (clients.recipes == null || clients.recipes.Count == 0)
        {
            Debug.LogWarning("OrderManager: No hay clientes configurados.");
            return;
        }

        if (clients.names == null || clients.names.Count == 0)
        {
            Debug.LogWarning("OrderManager: No hay nombres configurados.");
            return;
        }


        SORecipe recipe = clients.recipes[UnityEngine.Random.Range(0, clients.recipes.Count)];
        string clientName = clients.names[UnityEngine.Random.Range(0, clients.names.Count)];


        Order newOrder = new (clientName, recipe, recipe.patience);

        newOrder.OnExpired += HandleOrderExpired;

        activeOrders.Add(newOrder);

        // Notificar al HUD para que instancie la UI:
        // HUDOrderManager.instance.CreateOrderUI(newOrder);
        //HUDOrdersTest.instance.CreateOrderUI(newOrder);
    }

    /// <summary>
    /// Handler que se llama cuando una Order dispara su evento OnExpired.
    /// </summary>
    private void HandleOrderExpired(Order order)
    {
        if (!activeOrders.Contains(order))
            return;

        order.OnExpired -= HandleOrderExpired;
        activeOrders.Remove(order);

        Debug.Log($"Order expirada: {order.instanceId}, de {order.orderId}");

        // Penalizaci�n / feedback / HUD:

        // HUDOrderManager.instance.OnOrderExpired(order);
        //HUDOrdersTest.instance.OnOrderExpired(order);
    }
}
