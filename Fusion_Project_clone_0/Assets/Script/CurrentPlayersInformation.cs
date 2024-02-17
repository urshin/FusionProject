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

public class CurrentPlayersInformation : NetworkBehaviour
{
    [Header("UI")]
    CharacterSelectHandler characterSelectPanel;
    WaitingPanelHandler waitingPanelHandler;
    [SerializeField] TextMeshProUGUI progressTMP;
    [SerializeField] GameObject canvas;
    public IngameTeamInfos ingameTeamInfos;


    [Networked, Capacity(3)]
    public NetworkDictionary<NetworkString<_32>, int> teamADic { get; }
  // Optional initialization
  = MakeInitializer(new Dictionary<NetworkString<_32>, int> { });


    private ChangeDetector _changeDetector;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        ingameTeamInfos = FindObjectOfType<IngameTeamInfos>();
        characterSelectPanel = GetComponentInChildren<CharacterSelectHandler>();
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
            }
        }


        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(teamADic):
                    
                    
                    break;

            }
        }
    }

    public void updateDiction()
    {

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

        // HideCanvas();

    }




    private void Start()
    {
        HideCanvas();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Object.HasInputAuthority)
        {
            foreach (var A in ingameTeamInfos.teamADictionary)
            {
                print("A Team ::::: " + A.Key + "   " + A.Value);
                progressTMP.text = (A.Key + "   " + A.Value);
            }
            foreach (var A in ingameTeamInfos.teamBDictionary)
            {
                print("B Team ::::: " + A.Key + "   " + A.Value);
                progressTMP.text = (A.Key + "   " + A.Value);
            }
        }


        if (Input.GetKeyDown(KeyCode.O) && Object.HasInputAuthority)
        {
            print(Object.HasStateAuthority.ToString() + Object.HasInputAuthority.ToString());
            progressTMP.text = (Object.HasStateAuthority.ToString() + Object.HasInputAuthority.ToString());
        }

        if (Input.GetKeyDown(KeyCode.X) && Object.HasInputAuthority)
        {

            RPC_TeamUpdate(gameObject.name, 0, "A");


        }
        if (Input.GetKeyDown(KeyCode.Backspace) && Object.HasInputAuthority)
        {
            progressTMP.text = "";
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
            //if (messageSource == Runner.LocalPlayer)
                teamADic.Add(name, job);
                ingameTeamInfos.teamADictionary.Add(name, job);
                ingameTeamInfos.teamAll.Add(name, job);
          

        }
        else if (team == "B")
        {
            teamADic.Add(name, job);
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
    public void RPC_Class(NetworkString<_32> name, int job,  PlayerRef messageSource)
    {
        if(ingameTeamInfos.teamADictionary.ContainsKey(name))
        {
            ingameTeamInfos.teamADictionary.Set(name, job);
            ingameTeamInfos.teamAll.Set(name, job);
        }
        else if (ingameTeamInfos.teamBDictionary.ContainsKey(name))
        {
            ingameTeamInfos.teamBDictionary.Set(name, job);
            ingameTeamInfos.teamAll.Set(name, job);

        }

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
            case "SwordBTN":
                RPC_ClassUpdate(gameObject.name, 1);
                break;

            case "MagicianBTN":
                RPC_ClassUpdate(gameObject.name, 2);
                break;

            case "ArcherBTN":
                RPC_ClassUpdate(gameObject.name, 3);
                break;
        }
    }

    public void ClassChange()
    {
        characterSelectPanel.TeamClassChange(gameObject.name, PlayerPrefs.GetString("Team"));
    }


}