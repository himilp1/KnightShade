using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public void DealDamage()
    {
        int enemyAtk = GetComponent<EnemyAI>().damage;

        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Animator animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if(!animator.GetBool("isDead")){
            playerHealth.TakeDamage(enemyAtk);
        }
    }
}
