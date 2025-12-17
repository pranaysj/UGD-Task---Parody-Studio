using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 1.5f;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 input;
    private bool jumpRequested;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
        HandleJump();

        Debug.DrawRay(transform.position,
    GravityController.Instance.GravityDirection,
    Color.red);

    }

    public void Move(float horizontal, float vertical)
    {
        input = new Vector3(horizontal, 0f, vertical);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            jumpRequested = true;
        }
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    private void HandleMovement()
    {
        Vector3 velocity = rb.velocity;
        Vector3 targetVelocity = input * moveSpeed;

        Vector3 changeVelocity = targetVelocity - new Vector3(rb.velocity.x, 0 , rb.velocity.z);
        rb.AddForce(changeVelocity, ForceMode.VelocityChange);
        
        if(input == Vector3.zero)
        {
            rb.velocity = new Vector3(
                targetVelocity.x,
                velocity.y,
                targetVelocity.z);
        }
    }

    private void HandleJump()
    {
        if (!jumpRequested) return;

        Vector3 gravityDir = GravityController.Instance.GravityDirection;
        rb.AddForce(-gravityDir * jumpForce, ForceMode.Impulse);

        jumpRequested = false;
    }

    private void ApplyGravity()
    {
        Vector3 gravityDir = GravityController.Instance.GravityDirection;
        float gravityStrength = GravityController.Instance.GravityStrength;

        rb.AddForce(gravityDir * gravityStrength, ForceMode.Acceleration);
    }

    private bool IsGrounded()
    {
        Vector3 gravityDir = GravityController.Instance.GravityDirection;

        return Physics.Raycast(
            transform.position,
            gravityDir,
            groundCheckDistance,
            groundLayer
        );
    }
}
 