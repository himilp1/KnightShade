using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    public float attackDamage = 25.0f; // Amount of damage each attack deals
    public float attackCooldown = 0.5f; // Cooldown time between attacks
    public Collider attackHitBox; // The collider representing the attack hit area
    public LayerMask enemyLayer;  // Set this in the inspector to the layer where enemies reside

    private Animator animator;
    private float lastAttackTime; // Timestamp of the last attack

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        // Check for "M" key press and the attack cooldown
        if (Input.GetKeyDown(KeyCode.M) && Time.time - lastAttackTime > attackCooldown)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        animator.SetTrigger("Attack");
        lastAttackTime = Time.time;
        StartCoroutine(CheckAttackHit());
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

    public bool IsAttacking() // Accessor for other scripts
    {
        bool isAttackState = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        return isAttackState;
    }


}
