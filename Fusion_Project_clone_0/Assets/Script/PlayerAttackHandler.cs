using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackHandler : NetworkBehaviour
{
    public GameObject magicBall;
    public Transform aimPoint;

    TickTimer MagicBallFireDelay = TickTimer.None;



    //Hit info
   public List<LagCompensatedHit> hits = new List<LagCompensatedHit>();
   public PlayerRef attackedByPlayerRef;
   public NetworkObject attackedByNetworkObject;
    [Header("Collision detection")]
    public LayerMask collisionLayers;






    //Other components
    public IngameTeamInfos ingameTeamInfos;
    public PlayerDataHandler PlayerDataHandler;
    public NetworkPlayer networkPlayer;
   public  NetworkObject networkObject;

    private void Awake()
    {
        ingameTeamInfos = FindObjectOfType<IngameTeamInfos>();
        PlayerDataHandler = GetComponent<PlayerDataHandler>();
        networkPlayer = GetBehaviour<NetworkPlayer>();
        networkObject = GetComponent<NetworkObject>();
    }









    public void Attak1(string job)
    {
        if(ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
        {
            switch (job)
            {
                case "Mage":
                    FireMagicBall(aimPoint.forward);
                    break;


                case "Warrior":
                    attak1();
                    break;
                case "Archer":
                    attak1();
                    break;
            }

            

        }
        else
        { return; }
       


    }


    public void FireMagicBall(Vector3 aimForwardVector)
    {
        //Check that we have not recently fired a grenade. 
        if (MagicBallFireDelay.ExpiredOrNotRunning(Runner))
        {
            Runner.Spawn(magicBall, aimPoint.position + aimForwardVector * 1.5f, Quaternion.LookRotation(aimForwardVector), Object.InputAuthority, (runner, spawnedRocket) =>
            {
                spawnedRocket.GetComponent<MagicBall>().Fire(Object.InputAuthority, networkObject);
            });

            //Start a new timer to avoid grenade spamming
            MagicBallFireDelay = TickTimer.CreateFromSeconds(Runner, 0.5f);
        }
    }

    public void attak1()
    {
        print( gameObject.name+"공격시도");
    }









}
