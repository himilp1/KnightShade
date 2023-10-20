using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour{
    public int maxHealth;
    private int currentHealth;
    void Start(){
        currentHealth = maxHealth;
        Debug.Log("in start function");
    }

    public void TakeDamage(int damage){
        currentHealth = currentHealth - damage;
        Debug.Log("player current health: " + currentHealth);
    }
}