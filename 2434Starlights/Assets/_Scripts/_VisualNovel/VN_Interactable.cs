using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class VN_Interactable : MonoBehaviour
{
    [Header("Interactable")]
    public string interactableName;
    [Header("Interactable Ref")]
    public KeyCode activationInput;
    public bool isActive = false;
    public GameObject user;
    private VN_PlayerController player;

    public virtual void Interact()
    {
        if (isActive)
        {
            Debug.Log(gameObject + " was interacted with Parent");
        }
    }

    public virtual void StartInteraction()
    {
        player.isMoveDisabled = true;
    }

    public virtual void EndInteraction()
    {
        player.isMoveDisabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = true;
            user = collision.gameObject;
            player = user.GetComponent<VN_PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = false;
            user = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
