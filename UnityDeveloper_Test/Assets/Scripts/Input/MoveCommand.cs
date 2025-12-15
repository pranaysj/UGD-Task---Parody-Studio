using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : IInputCommand
{
    private PlayerController player;

    public MoveCommand(PlayerController playerController) 
    {
        player = playerController;
    }

    public void Execute()
    {
        player.Move();
    }
}
