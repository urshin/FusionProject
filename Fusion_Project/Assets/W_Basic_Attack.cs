using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Basic_Attack : StateMachineBehaviour
{
   [SerializeField] PlayerMovementHandler playerMovementHandle;
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovementHandle = animator.GetComponentInParent<PlayerMovementHandler>();
        playerMovementHandle._cc.maxSpeed = 0;
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       playerMovementHandle._cc.maxSpeed = playerMovementHandle._dataHandler.characterInfo.Speed;
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
