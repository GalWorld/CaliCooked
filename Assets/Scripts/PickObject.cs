using UnityEngine;
using UnityEngine.XR;

public class PickObject : MonoBehaviour, IInteractable
{

   public GameObject handPoint;
   private GameObject pickedObject = null;

   private Rigidbody candidateRb = null;

    public void Interact()
    {
        if (pickedObject == null && candidateRb != null)
        {
            // Disable physics for the picked object
            candidateRb.useGravity = false;
            candidateRb.isKinematic = true;

            // Move it to the hand point and parent it
            candidateRb.transform.position = handPoint.transform.position;
            candidateRb.transform.SetParent(handPoint.transform);

            pickedObject = candidateRb.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
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
        if (candidateRb != null && other.gameObject == candidateRb.gameObject)
        {
            candidateRb = null;
        }
    }
}
