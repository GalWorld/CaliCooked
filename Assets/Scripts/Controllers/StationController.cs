using UnityEngine;
using System.Collections;

public class StationController : MonoBehaviour
{
    public StateEnum idState;
    public OutputStation StationSelfAction;
    private bool isCooking = false;
    private IngredientController currentIngredient = null;
    private GameObject ingredientGO;
    private bool isCurrentCooking= false;

    public int CookingTime;



    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IngredientController ingredient))
            {
                var ingredientState = ingredient.GetStateValue(idState);

                if (ingredientState != null && ingredientState != true && !isCooking)
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
                Debug.LogWarning("Objeto con tag 'Ingridient' no tiene IngredientController.");
            }
    }

    public void Cook()
    {
        if (isCooking && currentIngredient != null&&!isCurrentCooking)
        {
            isCurrentCooking=true;
            Debug.Log("Empezando a cocinar");
            ingredientGO = currentIngredient.gameObject;
            StartCoroutine(TimeForCooking());


        }
        else
        {
            Debug.Log("Aún no puedes cocinar, necesitas un ingresiente pelele");
        }

       
    }

    private IEnumerator TimeForCooking()
    {
        StationSelfAction.Generated(ingredientGO);

        yield return new WaitForSeconds(CookingTime);

            currentIngredient.SetStateValue(idState, true);
            ingredientGO.SetActive(true);
            isCooking = false;
            isCurrentCooking=false;
    }
 
}

