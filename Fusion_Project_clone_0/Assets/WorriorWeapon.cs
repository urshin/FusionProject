using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Fusion.NetworkBehaviour;

public class WorriorWeapon : NetworkBehaviour
{

    [SerializeField] PlayerAttackHandler attackHandler;


    public override void Spawned()
    {

    }



    public override void FixedUpdateNetwork()
    {
        WorriorAttack1();
    }

    public void WorriorAttack1()
    {

    }

   

}
