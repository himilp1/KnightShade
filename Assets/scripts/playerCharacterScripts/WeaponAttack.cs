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

    void OnTriggerStay(Collider collision){
        if(collision.TryGetComponent<EnemyAI>(out EnemyAI enemy)){
            if(playerAnimator.GetBool("IsAttacking")){
                Debug.Log("about to attack enemy");
                EnemyHealth enemyHP = enemy.GetComponent<EnemyHealth>();
                enemyHP.TakeDamage(weaponStats.atkDmg);
                playerAnimator.SetBool("IsAttacking", false);
            }
        }
    }
}
