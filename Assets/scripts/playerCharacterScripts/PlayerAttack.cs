using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    private float attackDamage; // Amount of damage each attack deals
    public float attackCooldown; // Cooldown time between attacks
    public Animator animator;

    public float knockbackForce;
    public float knockbackDuration = 1.0f;

    private PlayerInventory playerInventory;

    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 0.75f;

    public AudioSource swingSound;

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.Log("Player Inventory not found");
        }
        else
        {
            Debug.Log(playerInventory.defaultPrimaryWeapon);
        }
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo 1"))
        {
            animator.SetBool("hit1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo 2"))
        {
            animator.SetBool("hit2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo 3"))
        {
            animator.SetBool("hit3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
            animator.SetBool("combo", false);
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("combo", true);
                OnClick();
            }
        }

    }

    private void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            animator.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo 1"))
        {
            animator.SetBool("hit1", false);
            animator.SetBool("hit2", true);
        }
        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo 2"))
        {
            animator.SetBool("hit2", false);
            animator.SetBool("hit3", true);
        }
    }

    public void KnockbackEnemy(Transform enemy)
    {
        StartCoroutine(KnockbackCoroutine(enemy));
    }

    private IEnumerator KnockbackCoroutine(Transform enemy)
    {
        Vector3 originalPosition = enemy.position;
        Vector3 targetPosition = originalPosition + transform.forward * knockbackForce;

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(originalPosition, targetPosition);

        while (Time.time < startTime + knockbackDuration)
        {
            float distanceCovered = (Time.time - startTime) * knockbackForce / journeyLength;
            enemy.position = Vector3.Lerp(originalPosition, targetPosition, distanceCovered);
            yield return null;
        }

        // Ensure the enemy reaches the exact target position.
        enemy.position = targetPosition;
    }

    public void MakeSwingSound()
    {
        Debug.Log("Swing Sound");
        swingSound.Play();
    }
}