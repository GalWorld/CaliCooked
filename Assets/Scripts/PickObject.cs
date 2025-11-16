using UnityEngine;
using UnityEngine.XR;

public class PickObject : MonoBehaviour, IInteractable
{

   public GameObject handPoint;
   private GameObject pickedObject = null;
   private Rigidbody candidateRb = null; 


    public void Interact()
    {
        // If we already have an object, Interact will DROP it
        if (pickedObject != null)
        {
            Rigidbody pickedRb = pickedObject.GetComponent<Rigidbody>();
            if (pickedRb != null)
            {
                // Re-enable physics
                pickedRb.useGravity = true;
                pickedRb.isKinematic = false;
            }

            // Detach from hand
            pickedObject.transform.SetParent(null);

            // Clear reference
            pickedObject = null;
            return;
        }

        // If we do NOT have an object, Interact will PICK the candidate (if any)
        if (candidateRb != null)
        {
            candidateRb.useGravity = false;
            candidateRb.isKinematic = true;

            candidateRb.transform.position = handPoint.transform.position;
            candidateRb.transform.SetParent(handPoint.transform);

            pickedObject = candidateRb.gameObject;

            // Optional: clear candidate because it's now in hand
            candidateRb = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Only consider objects with the proper tag and if we don't already hold something
        if (other.CompareTag("Ingridient") && pickedObject == null)
        {
            if (other.TryGetComponent(out Rigidbody rb))
            {
                candidateRb = rb;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the current candidate leaves the trigger, clear the reference
        if (candidateRb != null && other.gameObject == candidateRb.gameObject)
        {
            candidateRb = null;
        }
    }

}