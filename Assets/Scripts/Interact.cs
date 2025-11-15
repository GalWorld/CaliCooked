using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

//Este script es un interactor flexible por ejemplo para la toma de objetos o interacción con estaciones. Aún sin definir
public class Interact : MonoBehaviour
{
     [Header("Interaction Settings")]
    public Transform InteractorSource;   
    public float InteractRange = 3f;     
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        if (InteractorSource == null)
        {
            Debug.LogWarning("Interact: InteractorSource is not assigned. Using this.transform as fallback.");
            InteractorSource = transform;
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteractPerformed;

        inputActions.Player.Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        TryInteract();
    }

    private void TryInteract()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
            }
        }
    }
}
