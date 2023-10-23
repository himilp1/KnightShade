using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    public float attackDamage = 25.0f; // Amount of damage each attack deals
    public float attackCooldown = 0.3f; // Cooldown time between attacks
    public Collider attackHitBox; // The collider representing the attack hit area
    public LayerMask enemyLayer;  // Set this in the inspector to the layer where enemies reside

    private float lastAttackTime; // Timestamp of the last attack
    public Animator animator;

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        // Check for LeftClick press and the attack cooldown
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time - lastAttackTime > attackCooldown)
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
                enemy.TakeDamage((int)attackDamage);
            }
        }
    }

    private IEnumerator ResetIsAttacking()
    {
        // Wait for the duration of the attack animation, then reset "IsAttacking" to false
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("IsAttacking", false);
    }
}