using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class MagicBall : NetworkBehaviour
{
   
    [Header("Prefabs")]
    //public GameObject explosionParticleSystemPrefab;

    [Header("Collision detection")]
    public Transform checkForImpactPoint;
    public LayerMask collisionLayers;

    //Timing
    TickTimer maxLiveDurationTickTimer = TickTimer.None;

    //Rocket info
    int magicBallSpeed = 40;

    //Hit info
    [SerializeField] List<LagCompensatedHit> hits = new List<LagCompensatedHit>();

    //Fired by info
    [SerializeField] PlayerRef firedByPlayerRef;
    string firedByPlayerName;
    [SerializeField] NetworkObject firedByNetworkObject;

    //Other components
    [SerializeField] NetworkObject networkObject;

    public void Fire(PlayerRef firedByPlayerRef, NetworkObject firedByNetworkObject, string firedByPlayerName)
    {
        this.firedByPlayerRef = firedByPlayerRef;
        this.firedByPlayerName = firedByPlayerName;
        this.firedByNetworkObject = firedByNetworkObject;

        networkObject = GetComponent<NetworkObject>();

        maxLiveDurationTickTimer = TickTimer.CreateFromSeconds(Runner, 3);
    }

    public override void FixedUpdateNetwork()
    {
        transform.position += transform.forward * Runner.DeltaTime * magicBallSpeed;

        if (Object.HasStateAuthority)
        {
            //Check if the rocket has reached the end of its life
            if (maxLiveDurationTickTimer.Expired(Runner))
            {
                Runner.Despawn(networkObject);

                return;
            }

            //Check if the rocket has hit anything
            int hitCount = Runner.LagCompensation.OverlapSphere(checkForImpactPoint.position, 0.5f, firedByPlayerRef, hits, collisionLayers, HitOptions.IncludePhysX);
            print(hitCount);
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
                    Debug.Log(hits[i].Hitbox.name);
                    //Check that we didn't fire the rocket and hit ourselves. This can happen if the lag is a bit high.
                    if (hits[i].Hitbox.Root.GetBehaviour<NetworkObject>() == firedByNetworkObject)
                        isValidHit = false;
                   
                }
               
                
                

            }

            //We hit something valid
            if (isValidHit)
            {
                //Now we need to figure out of anything was within the blast radius
                hitCount = Runner.LagCompensation.OverlapSphere(checkForImpactPoint.position,0.3f, firedByPlayerRef, hits, collisionLayers, HitOptions.None);

                //Deal damage to anything within the hit radius
                for (int i = 0; i < hitCount; i++)
                {
                    PlayerDataHandler playerDataHandler = hits[i].Hitbox.transform.root.GetComponent<PlayerDataHandler>();

                    if (playerDataHandler != null)
                        playerDataHandler.OnTakeDamage( 1);
                 

                }



               // Runner.Despawn(networkObject);
            }
        }
    }

    //When despawning the object we want to create a visual explosion
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
       // Instantiate(explosionParticleSystemPrefab, checkForImpactPoint.position, Quaternion.identity);
    }



}

