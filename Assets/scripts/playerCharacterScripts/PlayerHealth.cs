using System.Collections;
using TMPro;
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
    private GameObject player;
    private StatTracker statTracker;
    public GameObject summaryScreen;

    private bool canRegenerate;
    private float lastDamageTime;
    private float lastRegenTime;
    private DeathText deathText;

    public AudioSource hitSound;
    public GameObject bgMusicObject;
    public GameObject lossJingleObject;

    private AudioSource music;
    private AudioSource lossJingle;

    public bool inSummaryScreen = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();
        deathText = HUD.GetComponent<DeathText>();
        deathText.HideText();

        summaryScreen.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        statTracker = player.GetComponent<StatTracker>();

        music = bgMusicObject.GetComponent<AudioSource>();
        lossJingle = lossJingleObject.GetComponent<AudioSource>();

        canRegenerate = false;
    }

    void Update()
    {
        if (currentHealth < maxHealth && canRegenerate)
        {
            RegenerateHealth();
        }

        if (inSummaryScreen && Input.GetKeyDown(KeyCode.Return))
        {
            RetrunToMenu();
        }
    }

    public void TakeDamage(int damage)
    {
        if (inSummaryScreen) return;

        currentHealth -= damage;
        statTracker.AddDamageTaken(damage);
        healthBar.SetHealth(currentHealth);

        hitSound.Play();

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
            if (Time.time - lastRegenTime > 1.0) // Make sure a second has passed
            {
                lastRegenTime = Time.time; // Record the time at which regen happens
                currentHealth = (int)Mathf.Min(currentHealth + regenRate, maxHealth);
                healthBar.SetHealth(currentHealth);
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
        music.enabled = false;
        lossJingle.Play();
        Invoke("DisplaySummary", 2.0f);
        // GetComponent<PlayerHealth>().enabled = false;
    }

    private void DisplaySummary()
    {
        GameObject summaryTextObject = summaryScreen.transform.GetChild(1).gameObject;
        summaryTextObject.GetComponent<TMP_Text>().text =
        "Game Over \n\n\n Waves Survived: " + statTracker.wavesSurvived +
        "\n\nDamage Dealt: " + statTracker.totalDamageDone +
        "\n\nDamage Taken: " + statTracker.totalDamageTaken +
        "\n\nPoints Earned: " + statTracker.totalPointsEarned +
        "\n\nPoints Spent: " + statTracker.totalPointsSpent +
        "\n\nPress Enter to Return to\nMain Menu";

        summaryScreen.SetActive(true);
        inSummaryScreen = true;
    }

    private void RetrunToMenu()
    {
        Debug.Log("returning to menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}