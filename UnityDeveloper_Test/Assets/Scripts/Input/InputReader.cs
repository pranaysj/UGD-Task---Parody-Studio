using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class InputReader : MonoBehaviour
{
    private MoveCommand moveCommand;
    private JumpCommand jumpCommand;
    private GravityCommand gravityCommand;

    private bool isSelectingGravity;
    private Vector3 selectedGravity;

    private void Start()
    {
        moveCommand = new MoveCommand(PlayerController.Instance);
        jumpCommand = new JumpCommand(PlayerController.Instance);
    }

    private void FixedUpdate()
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
            Debug.Log("Check");
            moveCommand.SetInput(horizontal, vertical);
            moveCommand.Execute();
        }
    }

    private void HandleGravityInput()
    {
        if (Input.GetKey(KeyCode.UpArrow)) SelectGravity(Vector3.forward);
        else if (Input.GetKey(KeyCode.DownArrow)) SelectGravity(Vector3.back);
        else if (Input.GetKey(KeyCode.LeftArrow)) SelectGravity(Vector3.left);
        else if (Input.GetKey(KeyCode.RightArrow)) SelectGravity(Vector3.right);
        else
        {
            isSelectingGravity = false;
        }

        if(isSelectingGravity && Input.GetKeyDown(KeyCode.Space))
        {
            gravityCommand.SetDirection(selectedGravity);
            gravityCommand.Execute();
        }
        
    }

    private void SelectGravity(Vector3 dir)
    {
        isSelectingGravity = true;
        selectedGravity = dir;
    }
}