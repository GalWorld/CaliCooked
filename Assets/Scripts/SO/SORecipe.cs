using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Sazon Resource/Recipe")]
public class SORecipe : ScriptableObject
{
    public string id;
    public string Nombre;
    public AudioClip AudioDelPedido;
    public float Paciencia = 2;
    public List<SOIngredient> Ingredients;
}
