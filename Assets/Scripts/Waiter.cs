using UnityEngine;
using System.Collections.Generic;
public class Waiter : MonoBehaviour
{
    private void dishCompleted (Dish dish) 
    {
        if (dish.isComplete)
        {
            Destroy(dish.gameObject);
            Debug.Log("Orden Entregada");
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        var dish = collision.body.GetComponent<Dish>();
        if(dish != null)
        {
            dishCompleted(dish);
        }
    }
}
