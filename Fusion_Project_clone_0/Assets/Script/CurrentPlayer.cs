using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Fusion.NetworkBehaviour;
using static InGameUIHandler;

public class CurrentPlayer : NetworkBehaviour
{
    [Header("UI")]
    CharacterSelectHandler characterSelectPanel;
    WaitingPanelHandler waitingPanelHandler;
    PlayerStatePanelHandler playerStatePanelHandler;
    [SerializeField] TextMeshProUGUI progressTMP;
    [SerializeField] GameObject canvas;
    InGameUIHandler ingameUIHandler;
    public IngameTeamInfos ingameTeamInfos;
    PlayerMovementHandler playerMovementHandler;

    [SerializeField] GameObject WaitingPanel;
    [SerializeField] GameObject PlayerStatePanel;



    [Header("PlayerBodyThings")]
    public NetworkObject playerBody;

    [SerializeField] PlayerDataHandler playerDataHandler;
    [SerializeField] LayerMask playerlayer;

    [Header("Cam")]
    [SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] Animator camController;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject camPoint;
    // 시네머신 프리룩
    [SerializeField] private PlayerAvatarView view;



    public override void Spawned()
    {
        mainCamera = Camera.main;

        ingameTeamInfos = FindObjectOfType<IngameTeamInfos>();
        characterSelectPanel = GetComponentInChildren<CharacterSelectHandler>();
        waitingPanelHandler = GetComponentInChildren<WaitingPanelHandler>();
        playerStatePanelHandler = GetComponentInChildren<PlayerStatePanelHandler>();
        ingameUIHandler = GetComponentInChildren<InGameUIHandler>();
        playerMovementHandler = GetComponent<PlayerMovementHandler>();
        playerDataHandler = GetComponent<PlayerDataHandler>();
        if (Object.HasInputAuthority)
        {
            view.SetCameraTarget();
        }
        camController = camPoint.GetComponent<Animator>();








    }



    public override void Render()
    {
        //ingameTeamInfo Change Detect
        foreach (var change in ingameTeamInfos._changeDetector.DetectChanges(ingameTeamInfos))
        {
            switch (change)
            {
                case nameof(ingameTeamInfos.teamADictionary):
                    ClassChange();
                    break;
                case nameof(ingameTeamInfos.teamBDictionary):
                    ClassChange();
                    break;
                case nameof(ingameTeamInfos.teamAll):
                    UpdateWaiting();
                    playerDataHandler.RPC_SetPlayerData();
                    break;
                case nameof(ingameTeamInfos.isStartBTNOn):
                    
                    if(ingameTeamInfos.isStartBTNOn)
                    {

                        ingameUIHandler.OnclickStartBTN();
                        SpawnCharactor();
                        ingameTeamInfos.gameState = IngameTeamInfos.GameState.Ready;
                        ingameTeamInfos.startTimer = TickTimer.CreateFromSeconds(Runner, 3);
                         
                    }
                    else
                    {
                        return;
                    }
                        
                    
            

                    break;
                case nameof(ingameTeamInfos.playerAlive):
                    LastPlayer();
                    

                    break;
               
            }
        }
    }


    public void HideCanvas()
    {
        if (!Object.HasInputAuthority)
        {
            canvas.SetActive(false);
        }
        else
        {
            return;
        }
    }
    void Awake()
    {


    }




    private void Start()
    {
        HideCanvas();

    }

    public void Update()
    {

    }

    public void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            camController.Play("etc");

        }
        else
        {
            camController.Play("NormalState");
        }
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_TeamUpdate(NetworkString<_32> name, int job, string team, RpcInfo info = default)
    {
        RPC_Team(name, job, team, info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_Team(NetworkString<_32> name, int job, string team, PlayerRef messageSource)
    {


        if (team == "A")
        {

            ingameTeamInfos.teamADictionary.Add(name, job);
            ingameTeamInfos.teamAll.Add(name, job);
            ingameTeamInfos.playerAlive.Add(name, job);


        }
        else if (team == "B")
        {

            ingameTeamInfos.teamBDictionary.Add(name, job);
            ingameTeamInfos.teamAll.Add(name, job);
            ingameTeamInfos.playerAlive.Add(name, job);
        }




    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_JobUpdate(NetworkString<_32> name, int job, RpcInfo info = default)
    {
        RPC_Job(name, job, info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_Job(NetworkString<_32> name, int job, PlayerRef messageSource)
    {


        if (ingameTeamInfos.teamADictionary.ContainsKey(name))
        {
            ingameTeamInfos.teamADictionary.Add(name, job);
            ingameTeamInfos.teamADictionary.Set(name, job);

        }
        else if (ingameTeamInfos.teamBDictionary.ContainsKey(name))

        {
            ingameTeamInfos.teamBDictionary.Add(name, job);
            ingameTeamInfos.teamBDictionary.Set(name, job);
        }

        ingameTeamInfos.teamAll.Add(name, job);
        ingameTeamInfos.teamAll.Set(name, job);
    }


    public void ClassSelect(string job)
    {
        if (job == null)
        {
            return;
        }
        else if (job == "WarriorBTN")
        {
            RPC_JobUpdate(gameObject.name, 1);
        }
        else if (job == "MageBTN")
        {
            RPC_JobUpdate(gameObject.name, 2);
        }
        else if (job == "ArcherBTN")
        {
            RPC_JobUpdate(gameObject.name, 3);
        }



    }



    public override void FixedUpdateNetwork()
    {




    }

    public void TeamLayerUpdate()
    {
        if (ingameTeamInfos.teamADictionary.ContainsKey(gameObject.name))
        {

            foreach (var player in ingameTeamInfos.teamADictionary)
            {
                CurrentPlayer playerObject = GameObject.Find(player.Key.ToString()).gameObject.GetComponent<CurrentPlayer>();

                SetRenderLayerInChildren(playerObject.playerBody.transform, LayerMask.NameToLayer("team"));
            }
            foreach (var player in ingameTeamInfos.teamBDictionary)
            {
                CurrentPlayer playerObject = GameObject.Find(player.Key.ToString()).gameObject.GetComponent<CurrentPlayer>();

                SetRenderLayerInChildren(playerObject.playerBody.transform, LayerMask.NameToLayer("enemy"));
            }

        }
        else if (ingameTeamInfos.teamBDictionary.ContainsKey(gameObject.name))
        {

            foreach (var player in ingameTeamInfos.teamBDictionary)
            {
                CurrentPlayer playerObject = GameObject.Find(player.Key.ToString()).gameObject.GetComponent<CurrentPlayer>();

                SetRenderLayerInChildren(playerObject.playerBody.transform, LayerMask.NameToLayer("team"));
            }
            foreach (var player in ingameTeamInfos.teamADictionary)
            {
                CurrentPlayer playerObject = GameObject.Find(player.Key.ToString()).gameObject.GetComponent<CurrentPlayer>();

                SetRenderLayerInChildren(playerObject.playerBody.transform, LayerMask.NameToLayer("enemy"));
            }
        }

    }



    public void SetRenderLayerInChildren(Transform transform, int layerNumber)
    {
        //foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true))
        //{
        //    trans.gameObject.layer = layerNumber;
        //}
        transform.gameObject.transform.parent.gameObject.layer = layerNumber;
    }

    public void TeamSelect(string team)
    {
        if (Object.HasInputAuthority)
        {
            if (team == "A") RPC_TeamUpdate(gameObject.name, 0, "A");
            if (team == "B") RPC_TeamUpdate(gameObject.name, 0, "B");

        }
    }



    public void ClassChange()
    {
        if (characterSelectPanel.isActiveAndEnabled)
        {
            characterSelectPanel.TeamClassChange(gameObject.name);

        }
    }

    public void UpdateWaiting()
    {
        if (WaitingPanel.activeSelf)
        {
            waitingPanelHandler.UpdateWaiting();

        }

        if (PlayerStatePanel.activeSelf)
        {
            playerStatePanelHandler.UpdatePlayerStatePanel();

        }


    }


    public void SpawnCharactor()
    {



        foreach (var Player in ingameTeamInfos.teamAll)
        {
            GameObject current = GameObject.Find(Player.Key.ToString());



            int job = ingameTeamInfos.teamAll[Player.Key];
            if (Object.HasStateAuthority)
            {
                //GameObject body = Instantiate(ingameTeamInfos.charactor[job - 1], current.GetComponent<CurrentPlayer>().playerBody.transform.position, Quaternion.identity);
                NetworkObject body = Runner.Spawn(ingameTeamInfos.charactor[job - 1], current.GetComponent<CurrentPlayer>().playerBody.transform.position, Quaternion.identity);
                current.GetComponent<PlayerMovementHandler>().bodyAnime = body.GetComponent<Animator>();

                // body를 current.GetComponent<CurrentPlayer>().playerBody의 자식으로 설정
                body.transform.parent = current.GetComponent<CurrentPlayer>().playerBody.transform;
                RPC_MoveGameobject(body, current.GetComponent<CurrentPlayer>().playerBody);


            }


        }
        if (Object.HasInputAuthority)
        {
            TeamLayerUpdate();

        }

        //플레이어 살아있는지 초기화 하기
        foreach (var player in ingameTeamInfos.playerAlive)
        {
            ingameTeamInfos.playerAlive.Set(player.Key, 1);
        }

    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_MoveGameobject(NetworkObject gameobject, NetworkObject position, RpcInfo info = default)
    {
        RPC_Move(gameobject, position, info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_Move(NetworkObject gameobject, NetworkObject position, PlayerRef messageSource)
    {

        gameobject.transform.parent = position.transform;
        gameobject.transform.localPosition = Vector3.zero;
        gameobject.transform.rotation = position.transform.parent.rotation;





    }


    public void LastPlayer()
    {


        
        int leftPlayer = ingameTeamInfos.playerAlive.Count;
        foreach (var player in ingameTeamInfos.playerAlive)
        {
            if (player.Value == 0)
            {
                leftPlayer--;
            }
        }
        if (leftPlayer <= 1 && ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
        {
            Debug.Log("게임 끝났어~");

            //ingameUIHandler.RestartGame();

            ingameTeamInfos.gameState = IngameTeamInfos.GameState.End;

            ingameUIHandler.RestartGame();
            Runner.Despawn(playerBody.transform.GetChild(0).GetComponent<NetworkObject>());


        }

        



    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_ReStartGame(  RpcInfo info = default)
    {
        RPC_Restart(info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_Restart( PlayerRef messageSource)
    {

        //Runner.Despawn(playerBody.transform.GetChild(0).GetComponent<NetworkObject>());
        //ingameTeamInfos.teamADictionary.Clear();
        //ingameTeamInfos.teamBDictionary.Clear();
        //ingameTeamInfos.teamAll.Clear();
        //ingameTeamInfos.playerAlive.Clear();
        ingameUIHandler.RestartGame();




    }
}