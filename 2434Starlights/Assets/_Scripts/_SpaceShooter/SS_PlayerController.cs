using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_PlayerController : MonoBehaviour
{
    [Header("Vectors")]
    [SerializeField]
    Vector2 inputDirection;
    [SerializeField]
    Vector2 movementVector;

    [Header("Movement")]
    public float moveSpeed = 5;
    public bool isMoveDisabled = false;

    private void Update()
    {
        if (!isMoveDisabled)
        {
            CalculateMovement();
            transform.Translate(movementVector * Time.deltaTime);
        }
    }

    public void GetInputDirection(ref Vector2 inputDir)
    {
        inputDirection = inputDir;
    }

    void CalculateMovement()
    {
        movementVector.x = inputDirection.x * moveSpeed;
        movementVector.y = inputDirection.y * moveSpeed;
    }
}
