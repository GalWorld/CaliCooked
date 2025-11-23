using UnityEngine;

public class PickObject : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Transform holdPoint;

    private GameObject currentLookIngredient = null;
    private StationController currentLookStation = null;
    private BlendOutputStation blendOutputStation= null;
    private BoxController currentLookBox = null;
    private HingeDoor currentDoor = null;
    private GameObject pickedObject = null;
    private int randomValue;
    private int probabilityBlendFail;

    private void Update()
    {
        if(pickedObject != null && pickedObject.CompareTag("Inplate"))
        {
            DropObject();
        }
    }
    public void Interact()
    {
        // 1. Si el raycast esta sobre una station y no tiene nada en la mano, puede cocinar
        if (currentLookStation != null&& pickedObject==null &&blendOutputStation==null)
        {
            currentLookStation.Cook();
            return;
        }
        else if (currentLookStation != null&& pickedObject==null &&blendOutputStation !=null)
        {
            probabilityBlendFail=8;
            randomValue= Random.Range(0,10);
            Debug.Log(randomValue);
            if(randomValue > probabilityBlendFail)
            {
                Debug.Log("Error de licuadora");
                
                blendOutputStation.FailBlen();
                
            }else
            {
                currentLookStation.Cook();
                Debug.Log("Cocinando ahora sí");
            }
        }

        if (pickedObject != null)
        {
            DropObject();
            return;
        }

        if (currentLookBox != null && pickedObject == null)
        {
            currentLookBox.SpawnIngredient();
            return;
        }

        if(currentDoor != null)
        {
            currentDoor.ToggleDoor();
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
        pickedObject.transform.SetParent(holdPoint, true);
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

    public void SetStationBlend(BlendOutputStation blenOut)
    {
        blendOutputStation= blenOut;
    }
    
    public void SetBox(BoxController box)
    {
        currentLookBox = box;
    }

    public void SetDoor(HingeDoor door)
    {
        currentDoor = door;
    }
}


