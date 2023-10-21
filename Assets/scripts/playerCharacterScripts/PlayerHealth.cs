using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour{
    public int maxHealth;
    public int currentHealth;

    void Start(){
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth = currentHealth - damage;
    }
}