using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRain : NetworkBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float LifeTimer;
    [SerializeField] Transform Size;

    [SerializeField] int damage;
    [SerializeField] bool multipleDamage;
    [SerializeField] int multipleRate;
    [SerializeField] bool penetrate;

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
        LifeTimer = 3f;
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




        if (Object.HasStateAuthority)
        {
            //Check if the rocket has reached the end of its life
            if (maxLiveDurationTickTimer.Expired(Runner))
            {
                Runner.Despawn(networkObject);

                return;
            }



            //Check if the rocket has hit anything
            int hitCount = Runner.LagCompensation.OverlapSphere(checkForImpactPoint.position, Size.localScale.x, firedByPlayerRef, hits, collisionLayers, options: HitOptions.SubtickAccuracy, clearHits: true);
            //int hitCount = Runner.LagCompensation.OverlapBox(checkForImpactPoint.position, gameObject.transform.GetChild(0).transform.localScale, Quaternion.identity, firedByPlayerRef, hits, collisionLayers, options: HitOptions.SubtickAccuracy, clearHits: true);


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

                //Deal damage to anything within the hit radius
                for (int i = 0; i < hitCount; i++)
                {

                    PlayerDataHandler playerDataHandler = hits[i].Hitbox.transform.root.GetComponent<PlayerDataHandler>();

                    if (playerDataHandler != null)
                    {

                        if (!multipleDamage)
                            playerDataHandler.OnTakeDamage(damage);
                        else if (multipleDamage)
                        {
                            StartCoroutine(TakemultipleDamage(playerDataHandler));


                        }

                    }
                }
                if (!penetrate)
                {
                    Runner.Despawn(networkObject);

                }
                
            }
        }


    }

    IEnumerator TakemultipleDamage(PlayerDataHandler playerDataHandler)
    {
        playerDataHandler.OnTakeDamage(damage);
        yield return new WaitForSeconds(1 / multipleRate);
        StartCoroutine(TakemultipleDamage(playerDataHandler));
    }

}
