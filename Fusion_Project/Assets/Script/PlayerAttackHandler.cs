using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackHandler : NetworkBehaviour
{

    [Header("Mage")]
    public GameObject magicBall;
    public GameObject magicRain;
    public GameObject magicBeam;
    public GameObject magicMeteo;

    [Header("Archer")]
    public GameObject archerAttak1;
    public GameObject archerAttak2;
    public GameObject archerAttak3;
    public GameObject archerAttak4;

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
    public NetworkObject networkObject;

    public override void Spawned()
    {

        ingameTeamInfos = FindObjectOfType<IngameTeamInfos>();
        PlayerDataHandler = GetComponent<PlayerDataHandler>();
        networkPlayer = GetBehaviour<NetworkPlayer>();
        networkObject = GetComponent<NetworkObject>();
    }









    public void Attak1(string job)
    {
        if (ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
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
        if (ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
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
    }
    public void FireMagicRain(Vector3 aimForwardVector)
    {
        if (ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
        {
            Vector3 point30UnitsFromCamera = RaySystem(30);
            //Check that we have not recently fired a grenade. 
            if (MagicBallFireDelay.ExpiredOrNotRunning(Runner))
            {
                Runner.Spawn(magicRain, point30UnitsFromCamera, Quaternion.LookRotation(aimForwardVector), Object.InputAuthority, (runner, spawnedRocket) =>
                {
                    spawnedRocket.GetComponent<MagicRain>().Fire(Object.InputAuthority, networkObject);
                });

                //Start a new timer to avoid grenade spamming
                MagicBallFireDelay = TickTimer.CreateFromSeconds(Runner, 2f);
            }

        }
    }
    public void FireBeam(Vector3 aimForwardVector)
    {
        if (ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
        {
           // Vector3 point30UnitsFromCamera = RaySystem(30);
            //Check that we have not recently fired a grenade. 
            if (MagicBallFireDelay.ExpiredOrNotRunning(Runner))
            {
                Runner.Spawn(magicBeam, aimPoint.position + aimForwardVector * 1.5f, Quaternion.LookRotation(aimForwardVector), Object.InputAuthority, (runner, spawnedRocket) =>
                {
                    spawnedRocket.GetComponent<MagicBeam>().Fire(Object.InputAuthority, networkObject);
                });

                
                //Start a new timer to avoid grenade spamming
                MagicBallFireDelay = TickTimer.CreateFromSeconds(Runner, 2f);
            }

        }
    }

    public void FireMeteo(Vector3 aimForwardVector)
    {
        if (ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
        {
            Vector3 point30UnitsFromCamera = RaySystem(50);
            //Check that we have not recently fired a grenade. 
            if (MagicBallFireDelay.ExpiredOrNotRunning(Runner))
            {
                Runner.Spawn(magicMeteo, point30UnitsFromCamera, Quaternion.LookRotation(aimForwardVector), Object.InputAuthority, (runner, spawnedRocket) =>
                {
                    spawnedRocket.GetComponent<AttackSystem>().Fire(Object.InputAuthority, networkObject);
                });

                //Start a new timer to avoid grenade spamming
                MagicBallFireDelay = TickTimer.CreateFromSeconds(Runner, 2f);
            }

        }
    }






    //Archer

    public void FireArcherAttak1(Vector3 aimForwardVector)
    {
        if (ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
        {
            //Check that we have not recently fired a grenade. 
            if (MagicBallFireDelay.ExpiredOrNotRunning(Runner))
            {
                Runner.Spawn(archerAttak1, aimPoint.position + aimForwardVector * 1.5f, Quaternion.LookRotation(aimForwardVector), Object.InputAuthority, (runner, spawnedRocket) =>
                {
                    spawnedRocket.GetComponent<AttackSystem>().Fire(Object.InputAuthority, networkObject);
                });

                //Start a new timer to avoid grenade spamming
                MagicBallFireDelay = TickTimer.CreateFromSeconds(Runner, 0.5f);
            }

        }
    }
    public void FireArcherAttak2(Vector3 aimForwardVector)
    {
        if (ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
        {
            //Check that we have not recently fired a grenade. 
            if (MagicBallFireDelay.ExpiredOrNotRunning(Runner))
            {
                Runner.Spawn(archerAttak2, aimPoint.position + aimForwardVector * 1.5f, Quaternion.LookRotation(aimForwardVector), Object.InputAuthority, (runner, spawnedRocket) =>
                {
                    spawnedRocket.GetComponent<AttackSystem>().Fire(Object.InputAuthority, networkObject);
                });

                //Start a new timer to avoid grenade spamming
                MagicBallFireDelay = TickTimer.CreateFromSeconds(Runner, 0.5f);
            }

        }
    }


    private static Vector3 RaySystem(float far)
    {
        // 카메라의 위치와 방향을 기반으로 레이를 생성합니다.
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // 레이의 시작점
        Vector3 rayOrigin = ray.origin;

        // 레이의 방향
        Vector3 rayDirection = ray.direction;

        // 레이를 far만큼 발사한 지점을 구합니다.
        Vector3 point30UnitsFromCamera = rayOrigin + rayDirection * far;
        point30UnitsFromCamera.y = 0;


        return point30UnitsFromCamera;
    }

    public void attak1()
{
    print(gameObject.name + "공격시도");
}









}
