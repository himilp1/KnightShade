using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;  // Reference to the enemy's animator component
    private UnityEngine.AI.NavMeshAgent enemy;

    private void Start() 
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();  // Initialize the animator reference
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        
        if(currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        // Disable NavMeshAgent
        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Set the Die trigger to play the death animation
        animator.SetTrigger("Die");

        GetComponent<EnemyAI>().isDead = true;

        // Optionally, destroy the enemy object after a delay
        Invoke("DestroyEnemy", 5.0f);
    }


    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
