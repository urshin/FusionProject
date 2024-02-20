using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
    [SerializeField] InGameUIHandler ingameUIHandler;
    public IngameTeamInfos ingameTeamInfos;
    [SerializeField] PlayerMovementHandler playerMovementHandler;




    [Header("PlayerBodyThings")]
    public GameObject playerBody;

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
        playerStatePanelHandler= GetComponentInChildren<PlayerStatePanelHandler>();
        ingameUIHandler = GetComponentInChildren<InGameUIHandler>();
        playerMovementHandler = GetComponentInChildren<PlayerMovementHandler>();

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
                    break;
                case nameof(ingameTeamInfos.isStartBTNOn):
                    ingameUIHandler.OnclickStartBTN();
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


        }
        else if (team == "B")
        {
          
            ingameTeamInfos.teamBDictionary.Add(name, job);
            ingameTeamInfos.teamAll.Add(name, job);
        }




    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_ClassUpdate(NetworkString<_32> name, int job, RpcInfo info = default)
    {
        RPC_Class(name, job, info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_Class(NetworkString<_32> name, int job, PlayerRef messageSource)
    {
        if (ingameTeamInfos.teamADictionary.ContainsKey(name))
        {
            ingameTeamInfos.teamADictionary.Set(name, job);
            ingameTeamInfos.teamAll.Set(name, job);
            UpdatePlayerJob();

        }
        else if (ingameTeamInfos.teamBDictionary.ContainsKey(name))
        {
            ingameTeamInfos.teamBDictionary.Set(name, job);
            ingameTeamInfos.teamAll.Set(name, job);
            UpdatePlayerJob();

        }

    }






    public override void FixedUpdateNetwork()
    {
      
   
          
       
    }


    public void TeamSelect(string team)
    {
        if (Object.HasInputAuthority)
        {
            if (team == "A") RPC_TeamUpdate(gameObject.name, 0, "A");
            if (team == "B") RPC_TeamUpdate(gameObject.name, 0, "B");
        }
    }

    public void ClassSelect(string Class)
    {
        switch (Class)
        {
            case "WarriorBTN":
                RPC_ClassUpdate(gameObject.name, 1);
                break;

            case "MageBTN":
                RPC_ClassUpdate(gameObject.name, 2);
                break;

            case "ArcherBTN":
                RPC_ClassUpdate(gameObject.name, 3);
                break;
        }
    }


    public void UpdatePlayerJob()
    {
        for (int i = 0; i < playerBody.transform.childCount; i++)
        {

            playerBody.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (ingameTeamInfos.teamAll[gameObject.name] == 1)
        {
            playerBody.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (ingameTeamInfos.teamAll[gameObject.name] == 2)
        {
            playerBody.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (ingameTeamInfos.teamAll[gameObject.name] == 3)
        {
            playerBody.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            return;
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

        if (waitingPanelHandler.isActiveAndEnabled)
        {
            waitingPanelHandler.UpdateWaiting();
        }
        

        if (playerStatePanelHandler.isActiveAndEnabled)
        {
            playerStatePanelHandler.UpdatePlayerStatePanel();
        }
        
    }

}