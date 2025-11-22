using UnityEngine;

public class MergeMesh : MonoBehaviour
{
    [ContextMenu("Combinar")]
    void Combinar()
    {
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[filters.Length];

        for (int i = 0; i < filters.Length; i++)
        {
            combine[i].mesh = filters[i].sharedMesh;
            combine[i].transform = filters[i].transform.localToWorldMatrix;
        }

        Mesh newMesh = new Mesh();
        newMesh.CombineMeshes(combine);

        GameObject merged = new GameObject("Combinado");
        merged.AddComponent<MeshFilter>().sharedMesh = newMesh;
        merged.AddComponent<MeshRenderer>();

        Debug.Log("Mesh combinada generada.");
    }
}
