using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InputReader : MonoBehaviour
{
    private MoveCommand moveCommand;
    private JumpCommand jumpCommand;

    private void Start()
    {
        moveCommand = new MoveCommand(PlayerController.Instance);
        jumpCommand = new JumpCommand(PlayerController.Instance);
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            moveCommand.SetInput(horizontal, vertical); 
            moveCommand.Execute();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpCommand.Execute();
        }
    }
}