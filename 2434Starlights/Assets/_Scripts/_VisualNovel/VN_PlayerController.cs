using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_PlayerController : MonoBehaviour
{
    [Header("Vectors")]
    [SerializeField]
    Vector2 inputDirection;
    [SerializeField]
    Vector2 movementVector;

    [Header("Movement")]
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public bool isRunning = false;
    public bool isMoveDisabled = false;

    [Header("Current Room")]
    public GameObject activeRoom;
    private VN_Room currentRoom;

    [Header("Interactable")]
    public GameObject interactObject;
    private VN_Interactable interactable;

    private void Start()
    {
        //currentRoom = activeRoom ? activeRoom.GetComponent<VN_Room>() : new VN_Room();
    }

    private void Update()
    {
        if (!isMoveDisabled)
        {
            CalculateMovement();
            transform.Translate(movementVector * Time.deltaTime);
            if (activeRoom != null)
            {
                Vector2 clampedPos = activeRoom.transform.position;
                clampedPos.x = Mathf.Clamp(transform.position.x, clampedPos.x - (currentRoom.roomBoundary.width / 2), clampedPos.x + (currentRoom.roomBoundary.width / 2));
                clampedPos.y = Mathf.Clamp(transform.position.y, clampedPos.y - (currentRoom.roomBoundary.height / 2), clampedPos.y + (currentRoom.roomBoundary.height / 2));
                transform.position = clampedPos;
            }
        }
    }

    public void GetInputDirection(ref Vector2 inputDir)
    {
        inputDirection = inputDir;
    }

    void CalculateMovement()
    {
        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        movementVector.x = inputDirection.x * moveSpeed;
        movementVector.y = inputDirection.y * moveSpeed;
    }

    public void ToggleRunning()
    {
        isRunning = !isRunning;
    }

    public void InteractWithObject()
    {
        if (interactObject)
        {
            interactable.Interact();
        }
    }

    public void EnterRoom(GameObject room)
    {
        activeRoom = room.gameObject;
        currentRoom = activeRoom.GetComponent<VN_Room>();
    }

    public void ExitRoom()
    {
        activeRoom = null;
        currentRoom = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            EnterRoom(collision.gameObject);
            Debug.Log("entered room");
        }

        if (collision.CompareTag("Interactable"))
        {
            interactObject = collision.gameObject;
            interactable = interactObject.GetComponent<VN_Interactable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            ExitRoom();
            Debug.Log("exit room room");
        }

        if (collision.CompareTag("Interactable"))
        {
            interactObject = null;
            interactable = null;
        }
    }
}
