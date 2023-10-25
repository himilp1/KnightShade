using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;  // Reference to the enemy's animator component
    private UnityEngine.AI.NavMeshAgent enemy;
    private GameObject player;
    [SerializeField] floatingHealthBar healthBar;
    private void Start() 
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();  // Initialize the animator reference
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null){
            Debug.LogError("No Player Found!");
        }
        healthBar = GetComponentInChildren<floatingHealthBar>();
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if(currentHealth <= 0) 
        {
            Die();
            GetComponent<EnemyHealth>().enabled = false;
        }
    }

    private void Die() 
    {
        // Disable NavMeshAgent
        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Set the Die trigger to play the death animation
        animator.SetTrigger("Die");

        GetComponent<EnemyAI>().isDead = true;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // Optionally, destroy the enemy object after a delay
        Invoke("DestroyEnemy", 3.0f);
    }


    private void DestroyEnemy()
    {
        int points = GetComponent<EnemyAI>().enemyCost;
        player.GetComponent<PlayerPointsTracker>().AddPoints(points);
        Debug.Log("about to add points to player, points to give: " + points);
        Destroy(gameObject);
    }
}
