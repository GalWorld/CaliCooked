using UnityEngine;

public class StationController : MonoBehaviour
{
    public StateEnum idState;

    public void ApplyState(IngredientController ingController)
    {
        var ingredientState = ingController.GetStateValue(idState);

        if (ingredientState != null && ingredientState != true)
        {
            ingController.SetStateValue(idState, true);
        }
    }


    //Esto es una prueba para demostrar como cambiar el state de un ingrediente
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.body.CompareTag("Ingridient"))
        {
            var ingController = collision.body.GetComponent<IngredientController>();
            ApplyState(ingController);
        }
    }
}
