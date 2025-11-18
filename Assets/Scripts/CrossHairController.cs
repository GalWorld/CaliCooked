using UnityEngine;

public class CrossHairController : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private LayerMask interactableLayers;

    private void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 1.4f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayers))
        {
            Debug.Log("Apuntando a: " + hit.collider.name);
            

            // Si quieres, puedes ejecutar lógica especial cuando mire algo.
            // hit.collider.GetComponent<Interactable>()?.Highlight();
        }
    }
}
