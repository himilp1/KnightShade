using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    private float attackDamage; // Amount of damage each attack deals
    public float attackCooldown; // Cooldown time between attacks
    public Collider attackHitBox; // The collider representing the attack hit area
    public LayerMask enemyLayer;  // Set this in the inspector to the layer where enemies reside

    private float lastAttackTime; // Timestamp of the last attack
    public Animator animator;

    public float knockbackForce;
    public float knockbackDuration = 1.0f;

    private PlayerInventory playerInventory;
    private Collider weaponCollider;

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
        //SetWeaponStats();
        HandleAttack();
    }

    private void HandleAttack()
    {
        // Check for "F" press and the attack cooldown
        if (Input.GetKeyDown(KeyCode.F) && Time.time - lastAttackTime > attackCooldown)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        animator.SetBool("IsAttacking", true);
        lastAttackTime = Time.time;
    }

    public void EndAttack(){
        animator.SetBool("IsAttacking", false);
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
}