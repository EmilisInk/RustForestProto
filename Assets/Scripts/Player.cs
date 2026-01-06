using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Crouching")]
    public float crouchHeight = 1f;
    private float standingHeight = 1.6f;

    [Header("Camera")]
    public Camera playerCamera;

    [Header("Checks")]
    private Rigidbody rb;
    private bool isCrouching = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }

        if(isCrouching)
        {
            playerCamera.transform.localPosition = new Vector3(0, crouchHeight, 0);
        }
        else
        {
            playerCamera.transform.localPosition = new Vector3(0, standingHeight, 0);
        }
    }

    private void FixedUpdate()
    {
        float speed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

}
