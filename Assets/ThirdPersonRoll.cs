using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonRoll : MonoBehaviour
{
    ThirdPersonPlayer thirdPersonPlayer;
    public Animator animator;
    public RollCooldown rollCooldown;

    public float rollSpeed;
    public float rollTime;
    public float rollCooldownTime;
    private float lastRollTime;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonPlayer = GetComponent<ThirdPersonPlayer>();
        lastRollTime = -rollCooldownTime;  // Initialize lastRollTime to allow immediate rolling
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastRollTime >= rollCooldownTime)
        {
            StartCoroutine(Dash());
            lastRollTime = Time.time;  // Update the last roll time
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        rollCooldown.StartCooldownAnimation();

        while (Time.time < startTime + rollTime)
        {
            animator.SetBool("isRolling", true);
            thirdPersonPlayer.controller.Move(thirdPersonPlayer.moveDir * rollSpeed * Time.deltaTime);
            yield return null;
        }

        // Roll has completed, set the animator bool to false
        animator.SetBool("isRolling", false);
    }
}
