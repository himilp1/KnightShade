using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int regenRate = 1; // Health regen per second
    public float regenDelay = 5f; // Time without damage before regen starts

    public Animator animator;
    public HealthBar healthBar;

    private bool canRegenerate;
    private float lastDamageTime;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        canRegenerate = false;
    }

    void Update()
    {
        if (currentHealth < maxHealth && canRegenerate)
        {
            RegenerateHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        lastDamageTime = Time.time; // Record the time of the last damage

        if (currentHealth <= 0)
        {
            playerIsDead();
        }
        else
        {
            canRegenerate = true; // The player can start regenerating health
        }
    }

    void RegenerateHealth()
    {
        if (Time.time - lastDamageTime > regenDelay)
        {
            // Eventually change to use regen rate to regenerate over time rather than instantly
            currentHealth = 100;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void playerIsDead()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<ThirdPersonPlayer>().enabled = false;
        animator.SetBool("isDead", true);
    }
}