using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class walkingAnimation : StateMachineBehaviour
{

   Transform player;
   public NavMeshAgent agent;
   private float cooldown;
   private float lastAttackedAt = -9999f;

   // // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      agent = animator.GetComponent<NavMeshAgent>();
      cooldown = animator.GetComponent<EnemyAI>().atkCooldown;

      if (player == null)
      {
         Debug.Log("player not found");
      }
      if (agent == null)
      {
         Debug.Log("agent not found");
      }

   }

   // // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      float distance = Vector3.Distance(agent.transform.position, player.position);
      if (distance <= animator.GetComponent<EnemyAI>().attackRange && Time.time > lastAttackedAt + cooldown)
      {
         animator.SetTrigger("Attack");
         lastAttackedAt = Time.time;
      }
   }

   // // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      animator.ResetTrigger("Attack");
   }
}