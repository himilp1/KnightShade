using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    private float attackDamage; // Amount of damage each attack deals
    public float attackCooldown = 0.3f; // Cooldown time between attacks
    public Collider attackHitBox; // The collider representing the attack hit area
    public LayerMask enemyLayer;  // Set this in the inspector to the layer where enemies reside

    private float lastAttackTime; // Timestamp of the last attack
    public Animator animator;

    public float knockbackForce;
    public float knockbackDuration = 1.0f;

    private PlayerInventory playerInventory;

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
        SetWeaponStats();
        HandleAttack();
    }

    private void SetWeaponStats()
    {
        if (playerInventory.defaultPrimaryWeapon.name == "OHS10_Axe")
        {
            attackCooldown = 0.5f;
            attackDamage = 75.0f;
        }
        else if (playerInventory.defaultPrimaryWeapon.name == "THS05_Sword")
        {
            attackCooldown = 0.1f;
            attackDamage = 40.0f;
        }
        else
        {
            attackDamage = 34.0f;
        }
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
        StartCoroutine(CheckAttackHit());
        StartCoroutine(ResetIsAttacking()); // Start a coroutine to reset "IsAttacking"
        Debug.Log("Attack initiated.");
    }

    private IEnumerator CheckAttackHit()
    {
        // Wait for the optimal time in the animation to detect hit, e.g., halfway through the animation
        yield return new WaitForSeconds(0.3f); // Adjust this time based on your animation

        Collider[] hits = Physics.OverlapBox(attackHitBox.bounds.center, attackHitBox.bounds.extents, transform.rotation, enemyLayer);
        Debug.Log("Hits detected: " + hits.Length);
        foreach (Collider hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy)
            {
                Debug.Log("Dealt Damage: " + attackDamage);
                enemy.TakeDamage((int)attackDamage);
                KnockbackEnemy(enemy.transform);
            }
        }
    }

    private IEnumerator ResetIsAttacking()
    {
        // Wait for the duration of the attack animation, then reset "IsAttacking" to false
        yield return new WaitForSeconds(attackCooldown);
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