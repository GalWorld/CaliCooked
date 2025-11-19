using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClient", menuName = "Sazon Resource/Client")]
public class SOClients : ScriptableObject
{
    public List<string> names;
    //public GameObject ClientePrefab;
    public List<SORecipe> recipes;
}
