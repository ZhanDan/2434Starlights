using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_AutoMove : MonoBehaviour
{
    public float forwardSpeed;
    [Header("ZigZag")]
    public bool useZigZag = true;
    public bool useHorizontal = true;
    public float horizontalSpeed;
    private int horizontalDirection = 1;
    public float zigzagInterval;
    private float nextZigzagChange;

    [SerializeField]
    private Vector3 horizontalAxis;
    [SerializeField]
    private Vector2 movementVector;

    private void Start()
    {
        nextZigzagChange = Time.time + zigzagInterval;
    }

    private void Update()
    {
        horizontalAxis = transform.right;
        if (useZigZag)
        {
            if (Time.time >= nextZigzagChange)
            {
                horizontalDirection = -1 * horizontalDirection;
                nextZigzagChange = Time.time + zigzagInterval;
            }
        }

        CalculateMovement();
        transform.Translate(movementVector * Time.deltaTime, Space.World);
    }

    void CalculateMovement()
    {
        movementVector = (transform.up * forwardSpeed) + (horizontalAxis * horizontalDirection * horizontalSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up * forwardSpeed);
    }

}
