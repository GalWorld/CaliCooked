using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    private InputSystem_Actions inputSystem_Actions;
    private Rigidbody rb;

    public Vector2 InputMovement { get ; private set;}= Vector2.zero;
    public Transform cameraPivot;

    private float _speed= 4f;


    void Awake()
    {
        inputSystem_Actions= new InputSystem_Actions();
        TryGetComponent(out rb);
    }
    void FixedUpdate()
    {
        inputSystem_Actions.Player.Move.performed += ctx => InputMovement = ctx.ReadValue<Vector2>();
        inputSystem_Actions.Player.Move.canceled += ctx => InputMovement = ctx.ReadValue<Vector2>();
        MovementPlayer(InputMovement.x, InputMovement.y);
        Debug.Log(InputMovement);

        
    }

    void OnEnable()=> inputSystem_Actions.Enable();
    void OnDisable()=> inputSystem_Actions.Disable();


    public void MovementPlayer(float inputX, float inputY)
    {
        Vector3 Direction= transform.forward* inputY+ transform.right * inputX;
        Vector3 movementAmountScaled= Direction* _speed* Time.fixedDeltaTime; 
        rb.MovePosition(rb.position+ movementAmountScaled); 
        
    }

}
