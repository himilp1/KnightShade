using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Animator animator;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            playerIsDead();
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