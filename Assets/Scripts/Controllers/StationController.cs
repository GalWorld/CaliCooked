using UnityEngine;

public class StationController : MonoBehaviour
{
    public StateEnum idState;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.body.CompareTag("Ingridient"))
        {
            var ingController = collision.body.GetComponent<IngredientController>();
            var ingredientState = ingController.GetStateValue(idState);

            if (ingredientState != null && ingredientState != true)
            {
                // Aqui yo creo que se hace la accion;
                Debug.Log($"Paso por {gameObject.name}");
                ingController.SetStateValue(idState, true);
            }

        }
    }
}
