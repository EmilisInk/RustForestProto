using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float sprintMultiplier = 2f;

    [Header("Camera crouch")]
    public Transform playerCamera;
    public float crouchCamOffset = 0.3f;
    public float camSmooth = 12f;

    [Header("Crouch Settings")]
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchSpeedMultiplier = 0.5f;
    public float crouchHeight = 1f;

    public bool isGrounded = true;
    public bool isCrouching;

    private CapsuleCollider col;
    public Rigidbody rb;

    private float standHeight;
    private Vector3 standCenter;

    private float standCamY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        standHeight = col.height;
        standCenter = col.center;

        standCamY = playerCamera.localPosition.y;

        isCrouching = false;
        col.height = standHeight;
        col.center = standCenter;

        Vector3 p = playerCamera.localPosition;
        p.y = standCamY;
        playerCamera.localPosition = p;
    }

    private void Update()
    {
        Lazertronas();

        if (Input.GetKeyDown(crouchKey))
        {
            if(isCrouching) TryStand();
            else Crouch();
        }

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;

        float speed = moveSpeed;

        if(isCrouching)
            speed *= crouchSpeedMultiplier;

        if(isRunning)
            speed *= sprintMultiplier;

        rb.velocity = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded && !isCrouching)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            }
        }

        float targetY = isCrouching ? (standCamY - crouchCamOffset) : standCamY;

        Vector3 p = playerCamera.localPosition;
        p.y = Mathf.Lerp(p.y, targetY, Time.deltaTime * camSmooth);
        playerCamera.localPosition = p;
    }

    void Crouch()
    {
        if(isCrouching) return;
        isCrouching = true;

        float newHeight = crouchHeight;
        float heightDiff = standHeight - newHeight;

        col.height = newHeight;

        col.center = standCenter - new Vector3(0f, heightDiff / 2f, 0f);
    }

    void TryStand()
    {
        if(!isCrouching) return;

        float heightDiff = standHeight - col.height;
        Vector3 origin = transform.position + Vector3.up * (col.height / 2f);

        if(Physics.Raycast(origin, Vector3.up, heightDiff + 0.1f))
        {
            return;
        }

        isCrouching = false;
        col.height = standHeight;
        col.center = standCenter;
    }

    private void Lazertronas()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }
}
