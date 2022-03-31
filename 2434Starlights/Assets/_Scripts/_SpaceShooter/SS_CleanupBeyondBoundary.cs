using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_CleanupBeyondBoundary : MonoBehaviour
{
    public SS_Boundary boundary;

    private void LateUpdate()
    {
        if (transform.position.x > boundary.DeleteBoundary.center.x + boundary.DeleteBoundary.width/2)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < boundary.DeleteBoundary.center.x - boundary.DeleteBoundary.width / 2)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > boundary.DeleteBoundary.center.y + boundary.DeleteBoundary.height / 2)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < boundary.DeleteBoundary.center.y - boundary.DeleteBoundary.height / 2)
        {
            Destroy(gameObject);
        }
    }
}
