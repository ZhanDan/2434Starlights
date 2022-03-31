using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_CameraController : MonoBehaviour
{
    public enum CameraMode { follow, locked };
    public CameraMode cameraMode = CameraMode.locked;
    public float verticalOffset = 0f;
    public float zDistance = -10f;

    [Header("Follow Mode")]
    public GameObject followTarget;


    [Header("Locked Mode")]
    private Vector3 lockedPosition;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (cameraMode == CameraMode.follow)
        {
            transform.position = followTarget.transform.position + new Vector3(0f, verticalOffset, zDistance);
        }
    }

    public void ChangeRoom(bool isFollowMode)
    {
        if (isFollowMode)
        {
            cameraMode = CameraMode.follow;
        }
        else
        {
            cameraMode = CameraMode.locked;
            transform.position = lockedPosition + new Vector3(0f, verticalOffset, zDistance);
        }
    }

    public void EditCameraProperties(float roomOffset, float camSize)
    {
        verticalOffset = roomOffset;
        cam.orthographicSize = camSize;
    }

    public void GetRoomDimension(Vector2 pos)
    {
        lockedPosition = pos + new Vector2(0f, verticalOffset);
    }

}
