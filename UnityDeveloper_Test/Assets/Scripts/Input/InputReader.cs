using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InputReader : MonoBehaviour
{
    private MoveCommand moveCommand;

    private void Start()
    {
        moveCommand = new MoveCommand(PlayerController.Instance);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveCommand.Execute();
        }
    }
}
