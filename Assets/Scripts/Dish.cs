using UnityEngine;
using System.Collections.Generic;

public class Dish : MonoBehaviour
{
    public bool isComplete;
    public List<IngredientController> currentIngredients = new();
    private void CheckCurrentOrderIsComplete()
    {
        isComplete = currentIngredients.TrueForAll(ingredient => ingredient.isComplete);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingridient"))
        {
            other.transform.SetParent(transform);
            var ingController = other.GetComponent<IngredientController>();
            ingController?.SetStateValue(StateEnum.plate, true);
            currentIngredients.Add(ingController);
            CheckCurrentOrderIsComplete();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ingridient"))
        {
            other.transform.SetParent(null);
            var ingController = other.GetComponent<IngredientController>();
            ingController?.SetStateValue(StateEnum.plate, false);
            currentIngredients.RemoveAll(ingredients => ingredients.GetIngredientId() == ingController.GetIngredientId());
            CheckCurrentOrderIsComplete();
        }
        //Ingrediente sale
    }
}
