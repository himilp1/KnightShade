using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI: MonoBehaviour{
    public NavMeshAgent agent;//our enemy
    public Transform player;
    public float walkRange;//same as sightRange
    public float attackRange;
    public LayerMask whatIsPlayer;
    public bool inSightRange, inAttackRange;

    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(player == null){
            Debug.Log("player not found");
        }
        if(agent == null){
            Debug.Log("agent not found");
        }
        Debug.Log("Enemy position is: " + agent.transform.position);
    }

    void Update(){
        inSightRange = Physics.CheckSphere(agent.transform.position, walkRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!inSightRange && !inAttackRange){
            Debug.Log("in patrol");
            Patrolling();
        }
        else if(inSightRange && !inAttackRange){
            Debug.Log("in chase");
            ChasePlayer();
        }
        else if(inSightRange && inAttackRange){
            Debug.Log("in attack");
             AttackPlayer();
        }
    }


    private void Patrolling(){
        if(agent.remainingDistance <= agent.stoppingDistance){
            Vector3 point;
            if(WalkPoint(agent.transform.position, walkRange, out point)){
                agent.SetDestination(point);
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

     private void ChasePlayer(){
        agent.destination = player.position;
    }

    private void AttackPlayer(){
        agent.SetDestination(transform.position);//stops enemy from continously running
        transform.LookAt(player);

        
    }
}