using System;
using UnityEngine;

[System.Serializable]
public class Order
{
    public string orderId, instanceId;
    public string clientName;
    public SORecipe recipe;

    public float totalPatience;
    public float currentPatience;
    public AudioClip audio;

    public event Action<Order> OnExpired;

    public Order(string clientName, SORecipe recipe, float patience)
    {
        this.instanceId = System.Guid.NewGuid().ToString();
        this.orderId = recipe.id;
        this.recipe = recipe;
        this.clientName = clientName;
        this.totalPatience = patience;
        this.currentPatience = patience;
        this.audio = recipe.audio;
    }

    public void Tick(float deltaTime)
    {
        if (currentPatience <= 0f)
            return;

        currentPatience -= deltaTime;

        if (currentPatience <= 0f)
        {
            currentPatience = 0f;
            OnExpired?.Invoke(this);
        }
    }
}
