using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
//using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public CinemachineFreeLook freeLook;
    private bool cameraLocked = false;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private float gravity = -9.8f;
    private Vector3 velocity;

    private InteractionText interactionText;
    public CanvasGroup interactTextBackground;
    private PlayerPointsTracker pointsTracker;
    public GameObject HUD;
    public Animator animator;
    public StatTracker statTracker;

    public Vector3 moveDir;

    public AudioSource footstepSound;
    private float footstepTimer = 0f;
    public float footstepDelay = 0.5f;


    private void Start()
    {
        interactionText = HUD.GetComponent<InteractionText>();
        pointsTracker = GetComponent<PlayerPointsTracker>();

        if (pointsTracker == null)
        {
            Debug.LogError("Points tracker component not found.");
        }

        if (animator == null)
        {
            Debug.LogError("animator component not found.");
        }

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
        interactTextBackground.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteractions();
        HandleCameraLock();
    }

    private void HandleCameraLock()
    {
        if (Input.GetKeyDown(KeyCode.V) && cameraLocked == false)
        {
            freeLook.m_YAxis.m_MaxSpeed = 0;
            freeLook.m_XAxis.m_MaxSpeed = 0;
            cameraLocked = true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.V) && cameraLocked == true)
        {
            freeLook.m_YAxis.m_MaxSpeed = 2;
            freeLook.m_XAxis.m_MaxSpeed = 250;
            cameraLocked = false;
            return;
        }
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

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            // Set the IsRunning parameter to true in the Animator
            animator.SetBool("IsRunning", true);

            // Apply gravity to the character's movement.
            controller.Move(velocity * Time.deltaTime);

            // Check if enough time has passed to play another footstep sound
            if (footstepTimer <= 0f)
            {
                footstepSound.Play();
                footstepTimer = footstepDelay; // Reset the timer
            }
            else
            {
                footstepTimer -= Time.deltaTime; // Decrease the timer
            }
        }
        else
        {
            // Set the IsRunning parameter to false in the Animator
            animator.SetBool("IsRunning", false);
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

        // Debug.DrawRay for visualization
        Debug.DrawRay(rayOrigin, rayDirection * maxInteractionDistance, Color.red);

        if (Physics.Raycast(interactionRay, out RaycastHit hit, maxInteractionDistance))
        {
            // Check if the hit object is a castle gate
            if (hit.collider.CompareTag("CastleGate"))
            {
                // This should ideally be moved to a file more related to the castle gate later.
                int castleGateCost = 20;

                // Show the text element with a custom message
                interactionText.SetText("Press 'E' to open castle gate. \n (" + castleGateCost + " Points)");
                interactionText.ShowText();
                interactTextBackground.alpha = 1;

                if (Input.GetKeyDown(KeyCode.E) && pointsTracker.currentPoints >= castleGateCost)
                {
                    // You can get the CastleGate component from the hit object
                    CastleGate castleGate = hit.collider.GetComponent<CastleGate>();
                    castleGate.Open();
                    pointsTracker.SpendPoints(castleGateCost);
                    statTracker.AddPointsSpent(castleGateCost);
                }
            }

            // Check if the hit object is a rare mystery box
            else if (hit.collider.CompareTag("RareMysteryBox"))
            {
                int mysteryBoxCost = 50;

                // Show the text element with a custom message
                interactionText.SetText("Press 'E' to get a random rare weapon. \n (" + mysteryBoxCost + " Points)");
                interactionText.ShowText();
                interactTextBackground.alpha = 1;

                if (Input.GetKeyDown(KeyCode.E) && pointsTracker.currentPoints >= mysteryBoxCost)
                {
                    MysteryBox mysteryBox = hit.collider.GetComponent<MysteryBox>();
                    mysteryBox.Open();
                    pointsTracker.SpendPoints(mysteryBoxCost);
                    statTracker.AddPointsSpent(mysteryBoxCost);
                }
            }

            // Check if the hit object is an uncommon mystery box
            else if (hit.collider.CompareTag("UncommonMysteryBox"))
            {
                int mysteryBoxCost = 30;

                // Show the text element with a custom message
                interactionText.SetText("Press 'E' to get a random uncommon weapon. \n (" + mysteryBoxCost + " Points)");
                interactionText.ShowText();
                interactTextBackground.alpha = 1;

                if (Input.GetKeyDown(KeyCode.E) && pointsTracker.currentPoints >= mysteryBoxCost)
                {
                    MysteryBox mysteryBox = hit.collider.GetComponent<MysteryBox>();
                    mysteryBox.Open();
                    pointsTracker.SpendPoints(mysteryBoxCost);
                    statTracker.AddPointsSpent(mysteryBoxCost);
                }
            }

            else if (hit.collider.CompareTag("HealthPotion"))
            {
                int healthPotionCost = 20;

                // Show the text element with a custom message
                interactionText.SetText("Press 'E' to consume health potion. \n (" + healthPotionCost + " Points)");
                interactionText.ShowText();
                interactTextBackground.alpha = 1;

                if (Input.GetKeyDown(KeyCode.E) && pointsTracker.currentPoints >= healthPotionCost)
                {
                    HealthPotion healthPotion = hit.collider.GetComponent<HealthPotion>();
                    healthPotion.Consume();
                    pointsTracker.SpendPoints(healthPotionCost);
                    statTracker.AddPointsSpent(healthPotionCost);
                }
            }

            else if (hit.collider.CompareTag("RollPotion"))
            {
                int rollPotionCost = 30;

                // Show the text element with a custom message
                interactionText.SetText("Press 'E' to consume potion and gain roll ability. \n (" + rollPotionCost + " Points)");
                interactionText.ShowText();
                interactTextBackground.alpha = 1;

                if (Input.GetKeyDown(KeyCode.E) && pointsTracker.currentPoints >= rollPotionCost)
                {
                    RollPotion rollPotion = hit.collider.GetComponent<RollPotion>();
                    rollPotion.Consume();
                    pointsTracker.SpendPoints(rollPotionCost);
                    statTracker.AddPointsSpent(rollPotionCost);
                }
            }

            else if (hit.collider.CompareTag("Anvil"))
            {
                GameObject weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().currentMeleeWeapon;
                int numOfUpgrades = weapon.GetComponent<WeaponStats>().upgradeNums;
                int anvilCost = 50; //default cost no upgrades

                if (numOfUpgrades == 1)
                {
                    anvilCost = 100;
                    //already has one upgrade
                }
                else if (numOfUpgrades == 2)
                {
                    anvilCost = 200;
                    //already has 2 upgrades
                }

                // Show the text element with a custom message
                interactionText.SetText("Press 'E' to upgrade current weapon. \n (" + anvilCost + " Points)");
                interactionText.ShowText();
                interactTextBackground.alpha = 1;

                if (Input.GetKeyDown(KeyCode.E) && pointsTracker.currentPoints >= anvilCost)
                {
                    Anvil anvil = hit.collider.GetComponent<Anvil>();
                    anvil.Use();
                    pointsTracker.SpendPoints(anvilCost);
                    statTracker.AddPointsSpent(anvilCost);
                }
            }

            else
            {
                // Hide the text element if not interacting with a castle gate
                interactionText.HideText();
                interactTextBackground.alpha = 0;
            }
        }
        else
        {
            // Hide the text element if no object is hit
            interactionText.HideText();
            interactTextBackground.alpha = 0;
        }
    }
}