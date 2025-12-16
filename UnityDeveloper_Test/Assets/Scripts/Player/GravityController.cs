using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public static GravityController Instance { get; private set; }

    [SerializeField] private float gravityStrength = 20f;

    private Vector3 gravityDirection = Vector3.down;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    public Vector3 GravityDirection => gravityDirection;
    public float GravityStrength => gravityStrength;


    public void SetGravity(Vector3 gravityDirection, Transform transform)
    {
        this.gravityDirection = gravityDirection.normalized;
        AlignPlayerToGravity(transform);
    }

    private void AlignPlayerToGravity(Transform player)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(player.up, -gravityDirection) * player.rotation;

        player.rotation = targetRotation;
    }

}
