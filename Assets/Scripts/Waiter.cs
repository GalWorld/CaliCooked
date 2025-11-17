using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class Waiter : MonoBehaviour
{
    private void CompareDishVsRecipes(Dish dish)
    {
        var dishIngredients = dish.currentIngredients;
        var activeOrders = OrderManager.instance.activeOrders;

        if (!dish.isComplete) return;

        foreach (var order in activeOrders)
        {
            bool sameIngredients = order.recipe.Ingredients.Select(ingredient => ingredient.id)
                .OrderBy(x => x).SequenceEqual(dishIngredients.Select(controller => controller.GetIngredientId()).OrderBy(x => x));

            if (sameIngredients)
            {
                Debug.Log("Orden Entregada");
                OrderManager.instance.CompleteOrder(order.recipe.id);
                Destroy(dish.gameObject);
                return;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var dish = collision.body.GetComponent<Dish>();
        if(dish != null)
        {
            CompareDishVsRecipes(dish);
        }
    }
}
