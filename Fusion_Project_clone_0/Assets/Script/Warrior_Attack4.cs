using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Attack4 : StateMachineBehaviour
{
    [SerializeField] PlayerAttackHandler attackHandler;
    [SerializeField] PlayerMovementHandler moveHandler;
    [SerializeField] float originalSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackHandler = animator.GetComponentInParent<PlayerAttackHandler>();
        attackHandler.FireWarriorAttack4(attackHandler.aimPoint.forward);
        moveHandler = attackHandler.playerMovementHandler;
        originalSpeed = moveHandler._cc.maxSpeed;
        moveHandler._cc.maxSpeed = 0;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveHandler._cc.maxSpeed = originalSpeed;
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
