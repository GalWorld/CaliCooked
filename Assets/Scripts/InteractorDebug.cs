using System;
using Unity.VisualScripting;
using UnityEngine;

public class InteractorDebug : MonoBehaviour
{
    private bool isCooking = false;
    private IngredientTest currentIngredient = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IngredientTest ingredient))
            {
                if (!ingredient.isCooked && !isCooking)
                {
                    Debug.Log("Entró un ingrediente crudo en la estación");
                    
                    currentIngredient = ingredient;     
                    
                    other.gameObject.SetActive(false);   
                    isCooking = true;
                }
                else
                {
                    Debug.Log("Este ingrediente ya está cocinado");
                }
            }
            else
            {
                Debug.LogWarning("Objeto con tag 'Ingridient' no tiene IngredientTest.");
            }
    }

    public void Cook()
    {
        if (isCooking && currentIngredient != null)
        {
            Debug.Log("Puedes cocinar");

            GameObject ingredientGO = currentIngredient.gameObject;

            ingredientGO.SetActive(true);

            currentIngredient.isCooked = true;

            isCooking = false;
        }
        else
        {
            Debug.Log("Aún no puedes cocinar, necesitas un ingresiente pelele");
        }
    }
}
