using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Sazon Resource/Recipe")]
public class SORecipe : ScriptableObject
{
    public string id;
    public string name;
    public string description;
    public AudioClip audio;
    public float patience = 20;
    public GameObject model;
    public List<SOIngredient> ingredients;
}
