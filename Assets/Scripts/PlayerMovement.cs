using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public Transform cameraPivot;                
    [SerializeField] private float _speed = 4f;   

    public Vector2 InputMovement { get; private set; } = Vector2.zero;

    private InputSystem_Actions inputSystem_Actions;
    private Rigidbody rb;

    private void Awake()
    {
        inputSystem_Actions = new InputSystem_Actions();

        if (!TryGetComponent(out rb))
        {
            Debug.LogError("PlayerMovement: No Rigidbody found on this GameObject.");
        }
    }

    private void OnEnable()
    {
        inputSystem_Actions.Player.Enable();

        inputSystem_Actions.Player.Move.performed += OnMovePerformed;
        inputSystem_Actions.Player.Move.canceled  += OnMoveCanceled;
    }

    private void OnDisable()
    {
        inputSystem_Actions.Player.Move.performed -= OnMovePerformed;
        inputSystem_Actions.Player.Move.canceled  -= OnMoveCanceled;

        inputSystem_Actions.Player.Disable();
    }

    private void FixedUpdate()
    {
        MovementPlayer(InputMovement.x, InputMovement.y);
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        InputMovement = ctx.ReadValue<Vector2>();
        // Debug.Log($"Move Input: {InputMovement}");
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        InputMovement = Vector2.zero;
    }


    public void MovementPlayer(float inputX, float inputY)
    {
        if (rb == null) return;

        Vector3 forward = cameraPivot ? cameraPivot.forward : transform.forward;
        Vector3 right   = cameraPivot ? cameraPivot.right   : transform.right;

        forward.y = 0f;
        right.y   = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * inputY + right * inputX;

        Vector3 movementAmountScaled = direction * _speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movementAmountScaled);
    }

}
