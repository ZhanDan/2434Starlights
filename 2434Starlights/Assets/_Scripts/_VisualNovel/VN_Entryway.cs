using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_Entryway : VN_Interactable
{
    [Header("Entryway")]
    public GameObject destination;
    public string destinationName;

    private void Start()
    {
        if (destination)
        {
            destinationName = destination.GetComponent<VN_Entryway>().interactableName;
        }
    }

    public override void Interact()
    {
        if (isActive)
        {
            if (destination != null)
            {
                user.GetComponent<VN_PlayerController>().ExitRoom();
                user.transform.position = destination.transform.position;
            }
            else
            {
                Debug.Log("There is no destination");
            }
            EndInteraction();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (destination)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(destination.transform.position, 1);

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, destination.transform.position);
        }
    }
}
