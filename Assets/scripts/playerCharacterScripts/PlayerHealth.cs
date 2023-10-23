using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour{
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    void Start(){
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage){
        currentHealth = currentHealth - damage;
        if(currentHealth <= 0){
            playerIsDead();
        }
    }

    public void playerIsDead(){
        animator.SetBool("isDead", true);
        GetComponent<CharacterController>().enabled = false;
    }
}