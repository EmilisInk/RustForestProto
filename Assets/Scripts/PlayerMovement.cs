using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float sprintMultiplier = 2f;
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public bool isStanding = false;
    public bool isGrounded = true;

    private CapsuleCollider col;

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        Lazertronas();
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;

        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector3(move.x * moveSpeed * sprintMultiplier, rb.velocity.y, move.z * moveSpeed * sprintMultiplier);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            }
        }
    }

    private void Lazertronas()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }
}
