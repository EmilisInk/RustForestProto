using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public float bobFrequency = 8f;
    public float bobAmount = 0.02f;
    public float bobSmooth = 12f;

    private Vector3 startLocalPos;
    private float timer;

    private void Start()
    {
        startLocalPos = transform.localPosition;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        bool moving = (Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveZ) > 0.1f);

        Vector3 target = startLocalPos;

        if (moving)
        {
            timer += Time.deltaTime * bobFrequency;
            float bobY = Mathf.Sin(timer) * bobAmount;
            float bobX = Mathf.Cos(timer / 2f) * bobAmount / 2f;
            target += new Vector3(bobX, bobY, 0f);

        }
        else
        {
            timer = 0f;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * bobSmooth);
    }
}
