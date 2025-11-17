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

    //Esto es una prueba para demostrar como ponerle al plato el state de enplatado y agregar el ingrediente al plato;
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
    //Esto es una prueba para demostrar como cambiar el state de un ingrediente
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
