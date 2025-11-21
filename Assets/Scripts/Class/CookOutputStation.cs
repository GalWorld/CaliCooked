using UnityEngine;

public class CookOutputStation : OutputStation
{
    [SerializeField] private Transform IngredientPosition;
    private AudioSource Audio;

    void Start()
    {
        TryGetComponent(out Audio);
    }

    public override void Generated(GameObject ingredientProccesed)
    {
        ingredientProccesed.transform.position= IngredientPosition.transform.position;
        Audio.Play();
        if (ingredientProccesed.TryGetComponent(out IngredientState ingredient))
        {
            ingredient.ChangeMesh(StateEnum.cook);
            
        };
        
    }

    public override void Degenerated()
    {

    }
}
