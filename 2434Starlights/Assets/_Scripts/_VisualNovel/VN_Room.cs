using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class VN_Room : MonoBehaviour
{
    public Vector2 defaultStartPos;//For when the player starts in this room
    public Boundary roomBoundary;//represents the player's moveable area
    public bool followCameraMode = false;
    public float cameraOffset = 0f;
    public float cameraSize = 5f;

    private void Start()
    {
        BoxCollider2D roomCollider = gameObject.GetComponent<BoxCollider2D>();
        roomBoundary.width = roomCollider.size.x * transform.localScale.x;
        roomBoundary.height = roomCollider.size.y * transform.localScale.y;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(defaultStartPos, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), new Vector3(roomBoundary.width, roomBoundary.height, 0));
    }

    [System.Serializable]
    public class Boundary
    {
        public float height, width;
    }
}
