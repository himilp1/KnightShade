using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI: MonoBehaviour{
    public NavMeshAgent agent;//our enemy
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake(){
        player = GameObject.Find("MC01").transform;
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned.");
        }
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing.");
        }
    }

    private void Update(){
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if(!playerInSightRange && !playerInAttackRange){
            Patrolling();
        }
        if(playerInSightRange && !playerInAttackRange){
            ChasePlayer();
        }
        if(playerInSightRange && playerInAttackRange){
            AttackPlayer();
        }
    }

    private void Patrolling(){
        Debug.Log("in Patrolling");
        if(!walkPointSet){
            SearchWalkPoint();
        }

        if(walkPointSet){
            agent.SetDestination(walkPoint);
            Debug.Log("should be moving right now");
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkPoint reached
        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
            walkPointSet = true;
        }
    }
    private void ChasePlayer(){
        Debug.Log("in Chase Player");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(){
        Debug.Log("in attack player");
        agent.SetDestination(transform.position);//stops enemy from continously running
        transform.LookAt(player);


        if(!alreadyAttacked){
            //still need to fill actual attack animations and such

            //end of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }
}