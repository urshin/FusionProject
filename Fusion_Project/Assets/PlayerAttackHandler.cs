using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackHandler : NetworkBehaviour
{

    IngameTeamInfos ingameTeamInfos;

    [Networked] private TickTimer delay { get; set; }


    [SerializeField] private MagicBall magicBall;
    [SerializeField] private MagicLazer magicLazer;

    public Transform SpawnBallPosition;

    //전방 값.
    private Vector3 _forward = Vector3.forward;

    PlayerDataHandler playerDataHandler;
    NetworkPlayer networkPlayer;
    NetworkObject networkObject;

    private void Awake()
    {
        ingameTeamInfos = FindAnyObjectByType<IngameTeamInfos>();
        playerDataHandler = GetComponent<PlayerDataHandler>();
        networkPlayer = GetBehaviour<NetworkPlayer>();
        networkObject = GetComponent<NetworkObject>();
    }


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
           


            if (HasStateAuthority && delay.ExpiredOrNotRunning(Runner))
            {
                if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.2f);

                   
                }




                if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON1))
                {


                }
                else if (!data.buttons.IsSet(NetworkInputData.MOUSEBUTTON1))
                {

                }

            }
        }
    }

    public void FireMagicBall(Vector3 aimForwardVector)
    {
        //Check that we have not recently fired a grenade. 
     
            Runner.Spawn(magicBall, SpawnBallPosition.position , Quaternion.LookRotation(SpawnBallPosition.forward), Object.InputAuthority, (runner, spawnedRocket) =>
            {
                spawnedRocket.GetComponent<MagicBall>().Fire(Object.InputAuthority, networkObject, networkPlayer.nickName.ToString());
            });

    }

    public void FireMagicBall2(Vector3 aimForwardVector)
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 2f), Random.Range(-2f, 2f)); // 랜덤한 오프셋 계산

            Runner.Spawn(magicBall, SpawnBallPosition.position + new Vector3(0, 4, 5) + randomOffset, Quaternion.LookRotation(SpawnBallPosition.up * -1), Object.InputAuthority, (runner, spawnedRocket) =>
            {
                spawnedRocket.GetComponent<MagicBall>().Fire(Object.InputAuthority, networkObject, networkPlayer.nickName.ToString());
            });
        }
    }

    public void FireMagicLazer(Vector3 aimForwardVector)
    {
        
            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 2f), Random.Range(-2f, 2f)); // 랜덤한 오프셋 계산

        Runner.Spawn(magicLazer, SpawnBallPosition.position, Quaternion.LookRotation(SpawnBallPosition.forward), Object.InputAuthority, (runner, spawnedRocket) =>
        {
            spawnedRocket.GetComponent<MagicLazer>().Fire(Object.InputAuthority, networkObject, networkPlayer.nickName.ToString());
            });
        
    }

}
