using UnityEngine;
using UnityEngine.XR;

public class PickObject : MonoBehaviour, IInteractable
{

   public GameObject handPoint;
   private GameObject pickedObject = null;
   private Rigidbody candidateRb = null; 
   private bool isInStation = false;
   private StationController currentStation = null;

    public void Interact()
    {
        if (isInStation && currentStation != null)
        {
            currentStation.Cook();
            // Si NO quieres que en estación haga pick/drop, puedes hacer: return;
            // return;
        }
        if (pickedObject != null)
        {
            Rigidbody pickedRb = pickedObject.GetComponent<Rigidbody>();
            if (pickedRb != null)
            {
                pickedRb.useGravity = true;
                pickedRb.isKinematic = false;
            }

            pickedObject.transform.SetParent(null);

            pickedObject = null;
            return;
        }

        if (candidateRb != null)
        {
            candidateRb.useGravity = false;
            candidateRb.isKinematic = true;

            candidateRb.transform.position = handPoint.transform.position;
            candidateRb.transform.SetParent(handPoint.transform);

            pickedObject = candidateRb.gameObject;

            candidateRb = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ingridient") && pickedObject == null)
        {
            if (other.TryGetComponent(out Rigidbody rb))
            {
                candidateRb = rb;
                //Debug.Log("Cogí una pera");
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
          if (other.CompareTag("Station"))
        {
            isInStation = true;

            if (other.TryGetComponent(out StationController station))
            {
                currentStation = station;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (candidateRb != null && other.gameObject == candidateRb.gameObject)
        {
            candidateRb = null;
        }

        if (currentStation != null && other.gameObject == currentStation.gameObject)
        {
            currentStation = null;
        }
    }


}