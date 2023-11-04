using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;
    [SerializeField] Transform orientation;

    public float groundDrag;

    [Header("Is Grounded")] public float playerHeight;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Jump")] public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool canJump;
    public KeyCode jumpKey = KeyCode.Space;
    
    private float finalSpeed;

    public KeyCode speedUpKey = KeyCode.LeftShift;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
        finalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        InputMovement();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        //Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.25f, groundLayer);
        Debug.Log(isGrounded);
        //Handle Drag
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        PlayerMovement();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "PotionChangeWeapon":
                break;
            case "BuffEnemy":
                break;
            case "Speed":
                break;
            case "Damage":
                break;
            case "GraveyardPointer":
                PlayerController.CanSpawnEnemies = true;
                break;
        }
    }

    private void InputMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void PlayerMovement()
    {
        if (Input.GetKeyDown(speedUpKey))
        {
            finalSpeed *= 1.25f;
        }
        else if (Input.GetKeyUp(speedUpKey))
        {
            finalSpeed = moveSpeed;
        }

        if (Input.GetKeyDown(jumpKey) && canJump && isGrounded)
        {
            canJump = false;
            JumpControl();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * finalSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * finalSpeed * 10f * airMultiplier, ForceMode.Force);
            rb.AddForce(new Vector3(0f, -9.8f, 0f), ForceMode.Acceleration);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Limit Speed if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void JumpControl()
    {
        //Reset Velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }
}