using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private Vector3 velocity;

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
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();
    }

    public void Move(float horizontal, float vertical)
    {

        Vector3 gravityDir = GravityController.Instance.GravityDirection;

        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, gravityDir).normalized;
        if (forward.sqrMagnitude < 0.001f)
            forward = Vector3.ProjectOnPlane(transform.right, gravityDir).normalized;

        Vector3 right = Vector3.Cross(gravityDir, forward).normalized;

        Vector3 move = (right * horizontal + forward * vertical) * moveSpeed;
        controller.Move(move * Time.deltaTime);
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            velocity = -GravityController.Instance.GravityDirection * jumpForce;
        }
    }

    private void ApplyGravity()
    {
        float gravity = GravityController.Instance.GravityStrength;
        Vector3 gravityDir = GravityController.Instance.GravityDirection;


        if (controller.isGrounded && Vector3.Dot(velocity, gravityDir) > 0)
        {
            velocity = gravityDir  *  2f; // keeps player grounded
        }

        velocity += gravityDir * gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    internal void ResetVelocity()
    {
        velocity = Vector3.zero;
    }
}