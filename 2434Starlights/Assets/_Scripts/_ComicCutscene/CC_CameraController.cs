using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_CameraController : MonoBehaviour
{
    public float defaultCameraSize = 5f;
    public float cameraStepSize = 1f;
    public float cameraMoveSpeed = 5f;

    private float currentCamSize;
    private Vector2 moveVector;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        currentCamSize = defaultCameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            currentCamSize += cameraStepSize;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentCamSize -= cameraStepSize;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentCamSize = defaultCameraSize;
        }


        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        moveVector.x = inputDir.x * cameraMoveSpeed;
        moveVector.y = inputDir.y * cameraMoveSpeed;
        transform.Translate(moveVector * Time.deltaTime);
        cam.orthographicSize = currentCamSize;
    }
}
