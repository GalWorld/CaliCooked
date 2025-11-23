using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Dish : MonoBehaviour
{
    public bool isComplete;
    public Transform position;
    public string currentOrder;
    public List<IngredientController> currentIngredients = new();

    private void Update() 
    {
        if (isComplete)
        {
            CompareDishVsRecipes();
        }
    }
    private void CheckCurrentOrderIsComplete()
    {
        if (currentIngredients.TrueForAll(ingredient => ingredient.isComplete))
        {
            CompareDishVsRecipes();
        }
    }

    //Esto es una prueba para demostrar como ponerle al plato el state de enplatado y agregar el ingrediente al plato;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingridient"))
        {
            if (other.TryGetComponent(out IngredientController i))
            {
                if (i.CheckIngredientIsPlatable())
                {
                    other.transform.position = position.position;
                    other.transform.localScale*=0.8f; 
                    var ingController = other.GetComponent<IngredientController>();
                    ingController?.SetStateValue(StateEnum.plate, true);
                    currentIngredients.Add(ingController);
                    other.gameObject.tag = "Inplate";

                }
                
            }
        }

    }
    //Esto es una prueba para demostrar como cambiar el state de un ingrediente
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Inplate"))
        {
            other.transform.SetParent(null);
            var ingController = other.GetComponent<IngredientController>();
            ingController?.SetStateValue(StateEnum.plate, false);
            currentIngredients.RemoveAll(ingredients => ingredients.GetIngredientId() == ingController.GetIngredientId());
            CheckCurrentOrderIsComplete();
            other.gameObject.tag = "Ingridient";
        }
        //Ingrediente sale
    }

    private void OnCollisionEnter(Collision collision) {

        var other = collision.gameObject;
        
        if (other.CompareTag("Inplate"))
        {
            other.transform.SetParent(transform);
            CheckCurrentOrderIsComplete();
            Destroy(other.GetComponent<Rigidbody>());
        }
    }

    private void CompareDishVsRecipes()
    {
        if (isComplete)
        {
            return;
        }
        var dishIngredients = currentIngredients;
        var activeOrders = OrderManager.instance.activeOrders;

        foreach (var order in activeOrders)
        {
            bool sameIngredients = order.recipe.ingredients.Select(ingredient => ingredient.id)
                .OrderBy(x => x).SequenceEqual(dishIngredients.Select(controller => controller.GetIngredientId()).OrderBy(x => x));

            if (sameIngredients)
            {
                currentOrder = order.recipe.id;
                isComplete = true;
                //Verificar si se destruye sin animacion, de lo contrario activar animacion trigger de destroy
                ShowRecipe(order.recipe.model);
                return;
            }
        } isComplete = false;
    }

    private void ShowRecipe(GameObject recipe)
    {
        foreach (Transform item in transform)
        {
            if (item.TryGetComponent(out IngredientController i))
            {
                Destroy(item.gameObject);
                
            }
        }
        GameObject newChild = Instantiate(recipe);
        newChild.transform.SetParent(position);
        newChild.transform.localPosition = Vector3.zero;
        //newChild.transform.localRotation = Quaternion.identity;
        //newChild.transform.localScale = Vector3.one;
    }
}
