using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClient", menuName = "Sazon Resource/Client")]
public class SOClients : ScriptableObject
{
    public List<string> Nombre;
    //public GameObject ClientePrefab;
    public List<SORecipe> Recetas;
}
