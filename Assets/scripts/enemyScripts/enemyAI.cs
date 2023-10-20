using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI: MonoBehaviour{
    public NavMeshAgent agent;//our enemy
    public Transform player;
    public float walkRange, attackRange;//same as sightRange
    public LayerMask whatIsPlayer;
    public bool inSightRange, inAttackRange;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    Animator animator;
    public float speed;
    public int damage;
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        if(player == null){
            Debug.Log("player not found");
        }
        if(agent == null){
            Debug.Log("agent not found");
        }
    }

    void Update(){
        inSightRange = Physics.CheckSphere(agent.transform.position, walkRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!inSightRange && !inAttackRange){
            speed = 2.0f;
            Patrolling();
        }
        else if(inSightRange && !inAttackRange){
            speed = 4.5f;
            ChasePlayer();
        }
        else if(inSightRange && inAttackRange){
            speed = 0.0f;
            AttackPlayer();
        }
    }


    public void Patrolling(){
        agent.speed = speed;
        if(agent.remainingDistance <= agent.stoppingDistance){
            Vector3 point;
            if(WalkPoint(agent.transform.position, walkRange, out point)){
                agent.SetDestination(point);
                animator.SetFloat("Speed", speed);
            }
        }
    }

    private bool WalkPoint(Vector3 center, float range, out Vector3 result){
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)){

            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

     public void ChasePlayer(){
        agent.speed = speed;
        agent.destination = player.position;
        animator.SetFloat("Speed", speed);
    }

    public void AttackPlayer(){
        float distance = Vector3.Distance(player.position, agent.transform.position);
        if(distance <= attackRange){
            agent.SetDestination(transform.position);//stops enemy from continously running
        }
        agent.speed = speed;
        animator.SetFloat("Speed", speed);
        transform.LookAt(player);

        if(!alreadyAttacked){
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(damage);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

}