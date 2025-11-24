using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleItem : MonoBehaviour
{
    private InputSystem_Actions inputSystem_Actions;

    [Header("Target")]
    [Tooltip("GameObject that will be enabled while holding the key.")]
    [SerializeField] private GameObject target;

    private void Awake()
    {
        // Create input actions instance
        inputSystem_Actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        // Enable action map
        inputSystem_Actions.Player.Enable();

        // Subscribe to Input System events
        inputSystem_Actions.Player.Toggle.performed += OnTogglePerformed;
        inputSystem_Actions.Player.Toggle.canceled += OnToggleCanceled;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        inputSystem_Actions.Player.Toggle.performed -= OnTogglePerformed;
        inputSystem_Actions.Player.Toggle.canceled -= OnToggleCanceled;

        inputSystem_Actions.Player.Disable();
    }

    private void OnTogglePerformed(InputAction.CallbackContext context)
    {
        // Hold reached: turn target ON
        if (target == null) return;
        target.SetActive(true);
    }

    private void OnToggleCanceled(InputAction.CallbackContext context)
    {
        // Key released / Hold canceled: turn target OFF
        if (target == null) return;
        target.SetActive(false);
    }
}
