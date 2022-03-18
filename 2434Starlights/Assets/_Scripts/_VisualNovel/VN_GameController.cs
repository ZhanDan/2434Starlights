using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_GameController : MonoBehaviour
{
    public Vector2 playerStart = new Vector2(0f,0f);
    private Vector2[] localMovePathWaypoints; //Unused

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerStart, 2);

        if (localMovePathWaypoints != null)
        {
            Gizmos.color = Color.cyan;
            float size = 0.3f;

            for (int i = 0; i < localMovePathWaypoints.Length; i++)
            {
                Vector2 globalMovePathWaypointPos = localMovePathWaypoints[i] + new Vector2(transform.position.x, transform.position.y);
                Gizmos.DrawLine(globalMovePathWaypointPos - Vector2.up * size, globalMovePathWaypointPos + Vector2.up * size);
                Gizmos.DrawLine(globalMovePathWaypointPos - Vector2.left * size, globalMovePathWaypointPos + Vector2.left * size);

                if(i+1 < localMovePathWaypoints.Length)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(localMovePathWaypoints[i], localMovePathWaypoints[i+1]);
                }
            }
        }
    }

}
