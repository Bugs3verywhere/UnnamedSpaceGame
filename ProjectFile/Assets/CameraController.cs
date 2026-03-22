using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    float horizontalRotation = 0f;
    public Transform playerBody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        horizontalRotation -= mouseY;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(horizontalRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
