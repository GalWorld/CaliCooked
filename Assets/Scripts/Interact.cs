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
    private IInteractable[] interactables;  
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        interactables = GetComponentsInChildren<IInteractable>();
        if (interactables == null || interactables.Length == 0)
        {
            Debug.LogWarning("Interact: No IInteractable found on this GameObject or its children.");
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
        if (interactables == null) return;

        foreach (var interactable in interactables)
        {
            interactable.Interact();
        }
    }
}
