using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [Header("Camera Settings")]
    public float sensitivity = 1000f;
    float xRotation = 0f;

    [Header("Hands Settings")]
    public Transform hands;
    public float handsMoveSpeed = 5f;
    public float downClampAngle = -30f;
    public float downOffset = -1f;
    public float upOffset = 0.1f;

    public Vector3 handsStartLocPos;

    public Transform playerBody;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        handsStartLocPos = hands.localPosition;
    }
    void Update()
    {
        if (ToggleInventory.isOpen)
            return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (hands != null)
        {
            Vector3 targetPos = handsStartLocPos;
            if (xRotation > 0)
            {
                targetPos.y = handsStartLocPos.y + upOffset;
            }
            else if (xRotation < downClampAngle)
            {
                targetPos.y = handsStartLocPos.y + downOffset;
            }

            targetPos.x = handsStartLocPos.x;
            targetPos.z = handsStartLocPos.z;

            hands.localPosition = Vector3.Lerp(hands.localPosition, targetPos, handsMoveSpeed * Time.deltaTime);
        }
    }
}
