using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Move(float horizontal, float vertical)
    {

        Debug.Log("Player Moving :" + horizontal);
        Debug.Log("Vertical : " + vertical);
    }
}
