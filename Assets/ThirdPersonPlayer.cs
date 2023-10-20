using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private float gravity = -9.8f;
    private Vector3 velocity;

    private InteractionText interactionText;
    public GameObject HUD;

    private void Start()
    {
        // Get a reference to the InteractionText script attached to your UI Text element.
        interactionText = HUD.GetComponent<InteractionText>();

        if (interactionText == null)
        {
            Debug.LogError("InteractionText component not found.");
        }
        else
        {
            Debug.Log("InteractionText component found.");
        }

        // Initially hide the text.
        interactionText.HideText();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Apply gravity
        if (controller.isGrounded)  // Check if the character is grounded.
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;  // Apply gravity.
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            // Apply gravity to the character's movement.
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void HandleInteractions()
    {
        // Define the ray's origin and direction
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f;
        Vector3 rayDirection = transform.forward;

        // Set the maximum interaction distance
        float maxInteractionDistance = 3.0f;

        // Create a ray that will be used for raycasting
        Ray interactionRay = new Ray(rayOrigin, rayDirection);

        if (Physics.Raycast(interactionRay, out RaycastHit hit, maxInteractionDistance))
        {
            // Check if the hit object is a castle gate
            if (hit.transform.TryGetComponent(out CastleGate castleGate))
            {
                // Show the text element with a custom message
                interactionText.SetText("Press 'E' to open castle gate.");
                interactionText.ShowText();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    castleGate.Open();
                }
            }
            else
            {
                // Hide the text element if not interacting with a castle gate
                interactionText.HideText();
            }
        }
        else
        {
            // Hide the text element if no object is hit
            interactionText.HideText();
        }
    }
}