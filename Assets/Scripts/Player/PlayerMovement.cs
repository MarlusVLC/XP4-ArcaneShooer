using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // this script goes in the player object
    //--------------------------------------
    
    // gets the character controller component from the player
    [SerializeField] private CharacterController controller;

    // sets the movement speed, gravity and jump height
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    // getting and setting stuff for the ground detection
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    // creates a velocity Vector3 to be used with gravity
    private Vector3 velocity;
    
    private bool isGrounded;
    
    void Update()
    {
        // ground detection stuff
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) { velocity.y = -2f; }
        
        // getting inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // setting movement according to inputs
        Vector3 move = transform.right * x + transform.forward * z;

        // "Move" method from character controller using the movement Vector3 and multiplying by the speed
        controller.Move(move * (speed * Time.deltaTime));

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // making gravity work
        velocity.y += gravity * Time.deltaTime;

        // applying velocity (gravity) the player
        controller.Move(velocity * Time.deltaTime);
    }
}