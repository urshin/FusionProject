using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_Attack4 : StateMachineBehaviour
{
    [SerializeField] PlayerAttackHandler attackHandler;
    [SerializeField] NetworkCharacterController cc;
    [SerializeField] float originalSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackHandler = animator.GetComponentInParent<PlayerAttackHandler>();
        cc = animator.GetComponentInParent<NetworkCharacterController>();
        attackHandler.FireMeteo(attackHandler.aimPoint.forward);
        originalSpeed = cc.maxSpeed;
        cc.maxSpeed = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cc.maxSpeed = originalSpeed;
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
