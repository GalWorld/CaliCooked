using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct ModelState
{
    public GameObject model;
    public StateEnum state;
}

public class IngredientState : MonoBehaviour
{
    public List<ModelState> models;

    private MeshFilter currentMesh;
    private MeshCollider currentCollider;

    private void Start()
    {
        TryGetComponent(out currentMesh);
        TryGetComponent(out currentCollider);
    }

    /// <summary>
    /// Cambia el modelo visual y el mesh collider según el estado.
    /// </summary>
    public void ChangeMesh(StateEnum newState)
    {
        int index = models.FindIndex(m => m.state == newState);

        if (index == -1)
        {
            return;
        }

        var selectedModel = models[index];

        MeshFilter modelMeshFilter = selectedModel.model.GetComponent<MeshFilter>();
        Mesh modelMesh = modelMeshFilter ? modelMeshFilter.sharedMesh : null;

        if (modelMesh == null)
        {
            Debug.LogError($"El modelo '{selectedModel.model.name}' no tiene MeshFilter o no tiene mesh.");
            return;
        }

        // Cambiar mesh visual
        if (currentMesh != null)
            currentMesh.mesh = modelMesh;

        // Cambiar mesh collider
        UpdateMeshCollider(modelMesh);

        // Eliminar hijo visual anterior
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        // Crear nuevo modelo visual como hijo
        GameObject newChild = Instantiate(selectedModel.model);
        newChild.transform.SetParent(transform);
        newChild.transform.localPosition = Vector3.zero;
        transform.localScale = selectedModel.model.transform.localScale;
        newChild.transform.localScale = Vector3.one;
        //newChild.transform.localRotation = Quaternion.identity;
        //newChild.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Actualiza o crea el MeshCollider según el nuevo mesh asignado.
    /// </summary>
    private void UpdateMeshCollider(Mesh newMesh)
    {
        if (currentCollider == null)
            currentCollider = gameObject.AddComponent<MeshCollider>();

        currentCollider.sharedMesh = null; // importante para refrescar collider
        currentCollider.sharedMesh = newMesh;
    }
}
