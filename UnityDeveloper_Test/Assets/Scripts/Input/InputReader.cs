using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting;

public class InputReader : MonoBehaviour
{
    private MoveCommand moveCommand;
    private JumpCommand jumpCommand;
    private GravityCommand gravityCommand;

    [SerializeField] private float gravityConfirmWindow = 1f;

    private bool isWaitingForConfirm;
    private Vector3 selectedGravity;
    private Coroutine gravityConfirmCoroutine;

    private void Start()
    {
        moveCommand = new MoveCommand(PlayerController.Instance);
        jumpCommand = new JumpCommand(PlayerController.Instance);
        gravityCommand = new GravityCommand(GravityController.Instance, PlayerController.Instance);
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleGravityInput();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCommand.Execute();
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            moveCommand.SetInput(horizontal, vertical);
            moveCommand.Execute();
        }
    }

    private void HandleGravityInput()
    {
        if (isWaitingForConfirm)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ExecuteGravity();
            }
            return; // CRITICAL: stop further input while waiting
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
            StartGravitySelection(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            StartGravitySelection(Vector3.back);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            StartGravitySelection(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            StartGravitySelection(Vector3.right);
    }

    private void StartGravitySelection(Vector3 dir)
    {
        if (isWaitingForConfirm)
            return;

        selectedGravity = dir;
        isWaitingForConfirm = true;

        gravityConfirmCoroutine = StartCoroutine(GravityConfirmTimer());
    }

    private IEnumerator GravityConfirmTimer()
    {
        float elapsed = 0f;

        while (elapsed < gravityConfirmWindow)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Cancel");

        // Time expired → cancel
        CleanupGravityState();
    }

    private void ExecuteGravity()
    {
        gravityCommand.SetDirection(selectedGravity);
        gravityCommand.Execute();

        CleanupGravityState();
    }

    private void CleanupGravityState()
    {
        isWaitingForConfirm = false;
        selectedGravity = Vector3.zero;

        if (gravityConfirmCoroutine != null)
        {
            StopCoroutine(gravityConfirmCoroutine);
            gravityConfirmCoroutine = null;
        }
    }
}