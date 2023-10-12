using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float dashSpeed = 15.0f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;
    public float doubleClickTime = 0.2f;
    public float jumpHeight = 3.0f; // New
    public float gravity = -9.81f; // New

    private CharacterController controller;
    private Animator animator;
    private PlayerAttack playerAttack; 

    private float lastDashTime;
    private bool isDashing;
    private bool isSprinting = false;
    private Vector3 dashDirection;
    private bool keyWasReleased = true;
    private float lastMoveTime;
    private Vector3 velocity; // New for vertical movement (gravity and jump)


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        // Jumping logic
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump"); // Assuming you've added a Jump trigger in the Animator
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - lastDashTime > dashCooldown && !isDashing)
        {
            StartDash();
        }

        // Check if any movement key is released
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            keyWasReleased = true;
        }

        // Check for a distinct movement key press
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && keyWasReleased)
        {
            keyWasReleased = false;  // Set flag to false since a key is now pressed
            if (Time.time - lastMoveTime < doubleClickTime)
            {
                isSprinting = true;
            }
            lastMoveTime = Time.time;
        }

        Move();
    }

    private void Move()
    {
        if (isDashing || playerAttack.IsAttacking()) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        float currentSpeed = isSprinting ? sprintSpeed : speed;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, currentSpeed * Time.deltaTime);

            Vector3 moveDirection = direction;
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
            animator.SetBool("IsRunning", true);
            if (isSprinting) animator.SetBool("IsSprinting", true);  // Set this in your Animator
            else animator.SetBool("IsSprinting", false);
        }
        else
        {
            isSprinting = false;
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsSprinting", false);
        }
    }

    private void StartDash()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        dashDirection = new Vector3(horizontal, 0, vertical).normalized;

        isDashing = true;
        animator.SetBool("IsDashing", true);
        lastDashTime = Time.time;
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        float dashEndTime = Time.time + dashDuration;

        while (Time.time < dashEndTime)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
        animator.SetBool("IsDashing", false);
        if (dashDirection.magnitude >= 0.1f)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);
    }
}
