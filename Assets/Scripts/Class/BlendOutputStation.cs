using UnityEngine;
public class BlendOutputStation: OutputStation
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
    }
}


