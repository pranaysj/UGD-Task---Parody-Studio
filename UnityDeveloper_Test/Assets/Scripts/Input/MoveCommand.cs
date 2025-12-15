using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : IInputCommand
{
    private PlayerController player;
    private float horizontal;
    private float vertical;

    public MoveCommand(PlayerController playerController) 
    {
        player = playerController;
    }

    public void SetInput(float horizontalInput, float verticalInput)
    {
        horizontal = horizontalInput;
        vertical = verticalInput;
    }

    public void Execute()
    {
        player.Move(horizontal, vertical);
    }
}
