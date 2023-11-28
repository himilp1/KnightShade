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
   private int currentHit;

   private float maxComboDelay;

   // // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      agent = animator.GetComponent<NavMeshAgent>();
      cooldown = animator.GetComponent<EnemyAI>().atkCooldown;
      maxComboDelay = animator.GetComponent<EnemyAI>().atkCooldown + 1.5f;

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
      if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo 1")){
         Debug.Log("reseting hit1");
         animator.SetBool("hit1", false);
      }
      if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo 2")){
         animator.SetBool("hit2", false);
      }
      if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo 3")){
         animator.SetBool("hit3", false);
         currentHit = 0;
      }
      if(Time.time - lastAttackedAt > maxComboDelay){
         currentHit = 0;
      }

      if (distance <= animator.GetComponent<EnemyAI>().attackRange && Time.time > lastAttackedAt + cooldown)
      {
          HitFunction(animator);
      }
   }
   public void HitFunction(Animator animator){
      lastAttackedAt = Time.time;
      currentHit++;
      Debug.Log("in hit function, currentHit: " + currentHit);
      if(currentHit == 1){
         animator.SetBool("hit1", true);
      }
      currentHit = Mathf.Clamp(currentHit, 0, 3);
      if(currentHit == 2){
         animator.SetBool("hit2", true);
      }
      if(currentHit == 3){
         animator.SetBool("hit3", true);
         currentHit = 0;
      }
      if(Time.time - lastAttackedAt > cooldown){
         currentHit = 0;
      }
   }
   // // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if(animator.GetBool("hit1")){
         animator.SetBool("hit1", false);
      }
      if(animator.GetBool("hit2")){
         animator.SetBool("hit2", false);
      }
      if(animator.GetBool("hit3")){
         animator.SetBool("hit3", false);
         currentHit = 0;
      }
   }
}
