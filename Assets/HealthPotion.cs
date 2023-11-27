using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public HealthBar healthBar;
    public ThirdPersonPlayer thirdPersonPlayer;
    public PlayerHealth playerHealth;

    public AudioSource potionSound;

    void Start()
    {
        playerHealth = thirdPersonPlayer.GetComponent<PlayerHealth>();
    }
    public void Consume()
    {
        potionSound.Play();
        playerHealth.maxHealth = 400;
        healthBar.SetMaxHealth(playerHealth.maxHealth);
        Debug.Log("Consumed Potion");
        gameObject.SetActive(false);
    }
}
