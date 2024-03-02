using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Fusion.NetworkBehaviour;

public class WorriorWeapon : NetworkBehaviour
{


    //Hit info
    List<LagCompensatedHit> hits = new List<LagCompensatedHit>();
    [Header("Collision detection")]
    public LayerMask collisionLayers;


    [SerializeField] NetworkObject firedByNetworkObject;



    public override void Spawned()
    {

    }



    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                print(Object.InputAuthority);
            }

            if (Object.InputAuthority != null)
            {

                int hitCount = Runner.LagCompensation.OverlapSphere(transform.position, 1, Object.InputAuthority, hits, collisionLayers);

                bool isValidHit = false;

                //We've hit something, so the hit could be valid
                if (hitCount > 0)
                    isValidHit = true;

                //check what we've hit
                for (int i = 0; i < hitCount; i++)
                {
                    //Check if we have hit a Hitbox
                    if (hits[i].Hitbox != null && hits[i].Hitbox.Root != null && firedByNetworkObject != null)
                    {
                        //Check that we didn't fire the rocket and hit ourselves. This can happen if the lag is a bit high.
                        if (hits[i].Hitbox.Root.GetBehaviour<NetworkObject>() == firedByNetworkObject)
                            isValidHit = false;
                    }
                }

                //We hit something valid
                if (isValidHit)
                {
                    //Now we need to figure out of anything was within the blast radius
                    hitCount = Runner.LagCompensation.OverlapSphere(transform.position, 1, Object.InputAuthority, hits, collisionLayers, HitOptions.None);

                    //Deal damage to anything within the hit radius
                    for (int i = 0; i < hitCount; i++)
                    {
                        PlayerDataHandler playerDataHandler = hits[i].Hitbox.transform.root.GetComponent<PlayerDataHandler>();

                        if (playerDataHandler != null)
                            playerDataHandler.OnTakeDamage(1);
                    }


                }
                else
                {
                    return;
                }

            }



        }
    }



}
