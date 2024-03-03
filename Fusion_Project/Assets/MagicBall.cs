using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicBall : NetworkBehaviour
{


    [SerializeField] float Speed;
    [SerializeField] float LifeTimer;


    [Header("Prefabs")]
   // public GameObject explosionParticleSystemPrefab;

    [Header("Collision detection")]
    public Transform checkForImpactPoint;
    public LayerMask collisionLayers;

    //Timing
    TickTimer maxLiveDurationTickTimer = TickTimer.None;

    //Hit info
    List<LagCompensatedHit> hits = new List<LagCompensatedHit>();

    //Fired by info
    PlayerRef firedByPlayerRef;
 
    NetworkObject firedByNetworkObject;

    //Other components
    NetworkObject networkObject;



    //LifeTimer
    [Networked] private TickTimer life { get; set; }

    public void Init()
    {
        life = TickTimer.CreateFromSeconds(Runner, LifeTimer);
    }


    public override void Spawned()
    {
        Speed = 20f;
        LifeTimer = 5f;
    }

    public void Fire(PlayerRef firedByPlayerRef, NetworkObject firedByNetworkObject)
    {
        this.firedByPlayerRef = firedByPlayerRef;
        
        this.firedByNetworkObject = firedByNetworkObject;

        networkObject = GetComponent<NetworkObject>();

        maxLiveDurationTickTimer = TickTimer.CreateFromSeconds(Runner, LifeTimer);
    }

    public override void FixedUpdateNetwork()
    {
            transform.position += Speed * transform.forward * Runner.DeltaTime;



        if (Object.HasStateAuthority)
        {
            //Check if the rocket has reached the end of its life
            if (maxLiveDurationTickTimer.Expired(Runner))
            {
                Runner.Despawn(networkObject);

                return;
            }

            print(Object.HasStateAuthority);
            print(firedByPlayerRef);
            print(hits);
            print(collisionLayers);
         
            //Check if the rocket has hit anything
            int hitCount = Runner.LagCompensation.OverlapSphere(checkForImpactPoint.position, 0.5f, firedByPlayerRef, hits, collisionLayers, HitOptions.None, clearHits: true);

        
            bool isValidHit = false;

            //We've hit something, so the hit could be valid
            if (hitCount > 0)
                isValidHit = true;

            //check what we've hit
            for (int i = 0; i < hitCount; i++)
            {
                //Check if we have hit a Hitbox
                if (hits[i].Hitbox != null)
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
                hitCount = Runner.LagCompensation.OverlapSphere(checkForImpactPoint.position, 1, firedByPlayerRef, hits, collisionLayers, HitOptions.None);

                //Deal damage to anything within the hit radius
                for (int i = 0; i < hitCount; i++)
                {
                    PlayerDataHandler playerDataHandler = hits[i].Hitbox.transform.root.GetComponent<PlayerDataHandler>();

                    if (playerDataHandler != null)
                        playerDataHandler.OnTakeDamage(1);
                }

               Runner.Despawn(networkObject);
            }
        }


    }




}



