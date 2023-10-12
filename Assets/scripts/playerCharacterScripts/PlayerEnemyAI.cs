using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerEnemyAI : MonoBehaviour
{
    public Transform target;
    public float chaseRange = 5.0f;
    public float attackRange = 1.5f;
    public float attackCooldown = 3.0f;
    private float timeSinceLastAttack = 0f;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public bool isDead = false;  // Add this line

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;  // Add this line. It prevents further AI execution if the enemy is dead.

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        timeSinceLastAttack += Time.deltaTime;

        if (distanceToTarget <= chaseRange)
        {
            Vector3 lookAtTarget = new Vector3(target.position.x, this.transform.position.y, target.position.z);
            transform.LookAt(lookAtTarget);

            if (distanceToTarget >= navMeshAgent.stoppingDistance)
            {
                ChasePlayer();
            }
            else if (distanceToTarget <= attackRange && timeSinceLastAttack >= attackCooldown)
            {
                AttackPlayer();
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("isRunning", true);
        if(navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Debug.LogError("NavMeshAgent is not active.");
        }

    }

    private void AttackPlayer()
    {
        timeSinceLastAttack = 0f;
        animator.SetTrigger("Attack");
    }
}
