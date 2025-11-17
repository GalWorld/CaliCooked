using UnityEngine;

[System.Serializable]
public class Order
{
    public GameObject client;
    public SORecipe recipe;

    public Order(GameObject client, SORecipe recipe)
    {
        this.client = client;
        this.recipe = recipe;
    }

}
