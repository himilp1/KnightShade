using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int regenRate; // Health regen per second
    public float regenDelay; // Time without damage before regen starts

    public Animator animator;
    public HealthBar healthBar;
    public GameObject HUD;

    private bool canRegenerate;
    private float lastDamageTime;
    private float lastRegenTime;
    private DeathText deathText;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();
        deathText = HUD.GetComponent<DeathText>();
        deathText.HideText();

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

    /*
    void RegenerateHealth()
    {
        if (Time.time - lastDamageTime > regenDelay)
        {
            // Eventually change to use regen rate to regenerate over time rather than instantly
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }
    */

    void RegenerateHealth()
    {
        if (Time.time - lastDamageTime > regenDelay)
        {
            if (Time.time - lastRegenTime > regenRate)
            {
                lastRegenTime = Time.time; // Record the time at which regen happens
                currentHealth = (int)Mathf.Min(currentHealth + regenRate, maxHealth);
                healthBar.SetHealth(currentHealth);

                Debug.Log("Regenerating Health");
            }
        }
    }

    public void playerIsDead()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<ThirdPersonPlayer>().enabled = false;
        animator.SetBool("isDead", true);
        deathText.ShowText();
        Invoke("RetrunToMenu", 2.0f);
    }

    private void RetrunToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}