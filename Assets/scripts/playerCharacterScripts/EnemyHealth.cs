using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;  // Reference to the enemy's animator component

    private void Start() 
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();  // Initialize the animator reference
    }

    public void TakeDamage(int damage) 
    {
        Debug.Log("Enemy is taking damage: " + damage);
        currentHealth -= damage;
        
        if(currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        // Disable NavMeshAgent
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent && agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // Set the Die trigger to play the death animation
        animator.SetTrigger("Die");

        // Set the isDead flag in the EnemyAI script to prevent further AI execution
        GetComponent<PlayerEnemyAI>().isDead = true;

        // Optionally, destroy the enemy object after a delay
        Invoke("DestroyEnemy", 5.0f);
    }


    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
