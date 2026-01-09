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
    public Vector3 handsNormalPos;
    public Vector3 handsHiddenPos;
    public float handsMoveSpeed = 5f;

    public Transform playerBody;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        handsNormalPos = hands.localPosition;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if(hands != null)
        {
            float t = Mathf.InverseLerp(-30f, -90f, xRotation);
            Vector3 targetPos = Vector3.Lerp(handsNormalPos, handsHiddenPos, t);
            hands.localPosition = Vector3.Lerp(hands.localPosition, targetPos, Time.deltaTime * handsMoveSpeed);
        }

    }
}
