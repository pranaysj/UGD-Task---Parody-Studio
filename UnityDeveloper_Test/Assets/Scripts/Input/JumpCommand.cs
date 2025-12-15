using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : IInputCommand
{
    private PlayerController player;

    public JumpCommand(PlayerController playerController)
    {
        player = playerController;
    }

    public void Execute()
    {
        player.Jump();
    }
}
