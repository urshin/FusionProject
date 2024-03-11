using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Archer_Attack2 : StateMachineBehaviour
{
    [SerializeField] PlayerAttackHandler attackHandler;
    [SerializeField] PlayerMovementHandler moveHandler;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackHandler = animator.GetComponentInParent<PlayerAttackHandler>();
        attackHandler.FireArcherAttak2(attackHandler.aimPoint.forward);
        moveHandler = animator.GetComponentInParent<PlayerMovementHandler>();
        moveHandler.isdashing = true;
        moveHandler.dashSpeed = 15;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveHandler.isdashing = false;
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }


  
}
