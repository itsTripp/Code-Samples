using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] public KeyCode primaryFireKey;
    [SerializeField] public KeyCode weaponSwitch1Key;
    [SerializeField] public KeyCode weaponSwitch2Key;
    [SerializeField] public KeyCode meleeAttackKey;
    [SerializeField] private KeyCode slideKey;
    [SerializeField] public KeyCode grapplingHookKey;
    [SerializeField] public KeyCode boomerangKey;

    [Header("Player Settings")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpRaycastDistance;
    [SerializeField] private int maxJump = 2;
    [SerializeField] private int currentJump = 0;

    private Rigidbody playerRigidbody;
    public GameObject boomerang;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Jump();
        /*if( Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject clone;
            clone = Instantiate(boomerang, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),  transform.rotation)as GameObject;
        }*/
    }

    private void FixedUpdate()
    {
        Movement();
    }

    //Jumping Function.
    private void Jump()
    {
        if( Input.GetKeyDown(jumpKey))
        {
            if (isGrounded() || maxJump > currentJump)
            {
                playerRigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
                currentJump++;
            }
        }
    }

    //Check if player is on the ground.
    private bool isGrounded()
    {
        currentJump = 0;
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);        
    }

    //The directional movement of the player.
    private void Movement()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 playerMovement = new Vector3(horizontalAxis, 0, verticalAxis) * playerSpeed * Time.fixedDeltaTime;

        Vector3 newPosition = playerRigidbody.position + playerRigidbody.transform.TransformDirection(playerMovement);

        playerRigidbody.MovePosition(newPosition);
    }
}
