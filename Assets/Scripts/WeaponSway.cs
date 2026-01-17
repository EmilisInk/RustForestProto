using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.04f;
    public float swayRotAmount = 3f;
    public float smooth = 12f;

    private Vector3 startLocalPos;
    private Quaternion startLocalRot;

    private void Start()
    {
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;
    }

    private void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        Vector3 targetPos = startLocalPos + new Vector3(-mx, -my, 0f) * swayAmount;

        Quaternion targetRot = startLocalRot * Quaternion.Euler(my * swayRotAmount, -mx * swayRotAmount, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * smooth);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);
    }
}
