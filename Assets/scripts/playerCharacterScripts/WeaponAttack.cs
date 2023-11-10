using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private Animator playerAnimator;
    private WeaponStats weaponStats;

    void Start(){
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        weaponStats = GetComponent<WeaponStats>();
    }

    void OnTriggerExit(Collider collision){
        if(collision.TryGetComponent<EnemyAI>(out EnemyAI enemy)){
            if(playerAnimator.GetBool("IsAttacking")){
                Debug.Log("dealing damage: " + weaponStats.atkDmg);
                EnemyHealth enemyHP = enemy.GetComponent<EnemyHealth>();
                enemyHP.TakeDamage(weaponStats.atkDmg);
                playerAnimator.GetComponent<PlayerAttack>().attackCooldown = weaponStats.atkSpd;
            }
        }
    }
}
