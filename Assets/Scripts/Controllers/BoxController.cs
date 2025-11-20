using UnityEngine;



public class BoxController : MonoBehaviour
{
    [Header("Box Settings")]
    [SerializeField] public GameObject ingredientPrefab;   
    [SerializeField] public Transform spawnPoint;

    public void SpawnIngredient()
    {
        if (ingredientPrefab == null)
        {
            Debug.LogWarning("BoxController: ingredientPrefab is not assigned.");
            return;
        }

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion rot = spawnPoint != null ? spawnPoint.rotation : transform.rotation;

        Instantiate(ingredientPrefab, pos, rot);
        Debug.Log("BoxController: Ingredient spawned.");
    }
}