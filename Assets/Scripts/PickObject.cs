using UnityEngine;

public class PickObject : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Transform holdPoint;

    private GameObject currentLookIngredient = null;
    private StationController currentLookStation = null;

    private GameObject pickedObject = null;

    public void Interact()
    {
        // 1. Si el raycast esta sobre una station y no tiene nada en la mano, puede cocinar
        if (currentLookStation != null&& pickedObject==null)
        {
            currentLookStation.Cook();
            return;
        }

        if (pickedObject != null)
        {
            DropObject();
            return;
        }

        if (currentLookIngredient != null)
        {
            PickObjectFromRay();
        }
    }

    private void PickObjectFromRay()
    {
        pickedObject = currentLookIngredient;
        pickedObject.transform.SetParent(holdPoint);
        pickedObject.transform.localPosition = Vector3.zero;
        pickedObject.transform.localRotation = Quaternion.identity;
        if (pickedObject.TryGetComponent(out Rigidbody rb))
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    private void DropObject()
    {
        pickedObject.transform.SetParent(null);

        if (pickedObject.TryGetComponent(out Rigidbody rb))
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        pickedObject = null;
    }
    public void SetIngredient(GameObject ingredient)
    {
        currentLookIngredient = ingredient;
    }

    public void ClearIngredient()
    {
        currentLookIngredient = null;
    }

    public void SetStation(StationController station)
    {
        currentLookStation = station;
    }
}
