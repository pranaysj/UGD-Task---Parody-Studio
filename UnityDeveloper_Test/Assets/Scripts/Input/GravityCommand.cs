using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCommand : IInputCommand
{
    private PlayerController player;
    private GravityController gravityController;

    private Vector3 gravityDirection;

    public GravityCommand(GravityController gravityController, PlayerController playerController)
    {
        this.gravityController = gravityController;
        player = playerController;
    }

    public void SetDirection(Vector3 selectedGravity)
    {
        gravityDirection = selectedGravity;
    }

    public void Execute()
    {
        gravityController.SetGravity(gravityDirection, player.transform);
        player.ResetVelocity();
    }
}