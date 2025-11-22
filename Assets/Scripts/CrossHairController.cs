using UnityEngine;

public class CrossHairController : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxDistance = 2f;
    [SerializeField] private LayerMask interactableLayers;

    [Header("References")]
    [SerializeField] private PickObject pickSystem;

    private void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        pickSystem.ClearIngredient();
        pickSystem.SetStation(null);
        pickSystem.SetBox(null);

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 2f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayers))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag("Dish"))
            {
                pickSystem.SetIngredient(hitObj);
                return;
            }
        
            if (hitObj.CompareTag("Ingridient"))
            {
                pickSystem.SetIngredient(hitObj);
            }


          
            if (hitObj.CompareTag("Station"))
            {
                if (hitObj.TryGetComponent(out StationController station))
                {
                    pickSystem.SetStation(station);
                }
            }

            if (hitObj.CompareTag("Box"))
            {
                if (hitObj.TryGetComponent(out BoxController box))
                {
                    pickSystem.SetBox(box);
                }
            }
        }
    }
}
