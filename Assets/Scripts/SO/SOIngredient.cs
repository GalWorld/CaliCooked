using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Sazon Resource/Ingredient")]
public class SOIngredient : ScriptableObject
{
    public string id;
    public List<StateEnum> States = new List<StateEnum> { StateEnum.plate };

   

}