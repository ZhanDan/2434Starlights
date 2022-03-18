using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VN_PlayerController))]
public class VN_PlayerInput : MonoBehaviour
{
    VN_PlayerController playerController;
    public bool isDisabled = false;

    private void Awake()
    {
        playerController = GetComponent<VN_PlayerController>();
    }

    private void Update()
    {
        if (!isDisabled)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerController.ToggleRunning();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerController.InteractWithObject();
            }

            Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
            playerController.GetInputDirection(ref inputDir);
        }
    }
}
