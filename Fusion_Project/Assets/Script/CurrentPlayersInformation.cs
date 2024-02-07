using Fusion;
using System.Collections;
using System.Collections.Generic;
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



    void Awake()
    {
        characterSelectPanel = GetComponentInChildren<CharacterSelectHandler>();
        waitingPanelHandler = GetComponentInChildren<WaitingPanelHandler>();



    }


    [Networked]
    public int TeamAcount { get; set; }
    [Networked]
    public int TeamBcount { get; set; }

    [Networked]
    public int currentplayer { get; set; }
    public int maxPlayer { get; set; }


    [Networked]
    [Capacity(4)] // Sets the fixed capacity of the collection
    [UnitySerializeField] // Show this private property in the inspector.
    public NetworkLinkedList<NetworkString<_32>> teamAplayerList { get; } = MakeInitializer(new NetworkString<_32>[] { });
    [Networked]
    [Capacity(4)] // Sets the fixed capacity of the collection
    [UnitySerializeField] // Show this private property in the inspector.
    public NetworkLinkedList<NetworkString<_32>> teamBplayerList { get; } = MakeInitializer(new NetworkString<_32>[] { });

    //Ç»Á¯ µñ¼Å³Ê¸®
    [Networked]
    [Capacity(3)] // Sets the fixed capacity of the collection
    [UnitySerializeField] // Show this private property in the inspector.
    public NetworkDictionary<NetworkString<_32>, float> teamADictionary => default;

    [Networked]
    [Capacity(3)] // Sets the fixed capacity of the collection
    [UnitySerializeField] // Show this private property in the inspector.
    public NetworkDictionary<NetworkString<_32>, float> teamBDictionary => default;


    private ChangeDetector _changeDetector;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }
    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {

                case nameof(teamADictionary):
                    //characterSelectPanel.ShowTeamInfo();
                    break;

                case nameof(teamBDictionary):
                    //characterSelectPanel.ShowTeamInfo();
                    break;

            }
        }
    }
    private void Start()
    {
        maxPlayer = 6;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            print(teamADictionary.Count);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            for (int i = 0; i < teamAplayerList.Count; i++)
            {
                print(teamAplayerList[i]);
            }
        }
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.Z))
        {
            RPC_TeamAdd("z", "A", 0);
        }
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.X))
        {
            RPC_TeamAdd("x", "A", 0);
        }
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.C))
        {
            RPC_TeamAdd("c", "B", 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var pair in teamADictionary)
            {
                Debug.Log("TeamA : Key: " + pair.Key + ", Value: " + pair.Value);
            }
            foreach (var pair in teamBDictionary)
            {
                Debug.Log("TeamB : Key: " + pair.Key + ", Value: " + pair.Value);
            }
        }
    }
    private TMP_Text _messages;

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_TeamAdd(string NickName, string Team, float classinfo, RpcInfo info = default)
    {
        RPC_TeamABadd(NickName, Team, classinfo, info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_TeamABadd(string NickName, string Team, float classinfo, PlayerRef messageSource)
    {
        // ÆÀ A¿¡ ´ëÇÑ Ã³¸®
        if (Team == "A")
        {
            if (!teamADictionary.ContainsKey(NickName))
            {
                teamADictionary.Add(NickName, classinfo);
            }
            else
            {
                teamADictionary.Set(NickName, classinfo);
            }
        }
        // ÆÀ B¿¡ ´ëÇÑ Ã³¸®
        else if (Team == "B")
        {
            if (!teamBDictionary.ContainsKey(NickName))
            {
                teamBDictionary.Add(NickName, classinfo);
            }
            else
            {
                teamBDictionary.Set(NickName, classinfo);
            }
        }
        //if (!teamADictionary.ContainsKey(NickName))
        //{
        //    if (messageSource == Runner.LocalPlayer)
        //    {

        //        if (Team == "A")
        //        {
        //            teamADictionary.Add(NickName, classinfo);
        //        }
        //        else if (Team == "B")
        //        {
        //            teamBDictionary.Add(NickName, classinfo);
        //        }
        //    }
        //    else
        //    {
        //        if (Team == "A")
        //        {
        //            teamADictionary.Add(NickName, classinfo);
        //        }
        //        else if (Team == "B")
        //        {
        //            teamBDictionary.Add(NickName, classinfo);
        //        }
        //    }

        //}
        //else
        //{
        //    teamADictionary.Set(NickName, classinfo);
        //}

        //if (teamBDictionary.ContainsKey(NickName))
        //{
        //    if (messageSource == Runner.LocalPlayer)
        //    {

        //        if (Team == "A")
        //        {
        //            teamADictionary.Add(NickName, classinfo);
        //        }
        //        else if (Team == "B")
        //        {
        //            teamBDictionary.Add(NickName, classinfo);
        //        }
        //    }
        //    else
        //    {
        //        if (Team == "A")
        //        {
        //            teamADictionary.Add(NickName, classinfo);
        //        }
        //        else if (Team == "B")
        //        {
        //            teamBDictionary.Add(NickName, classinfo);
        //        }
        //    }
        //}
        //else
        //{
        //    teamBDictionary.Set(NickName, classinfo);
        //}



    }

    public void ClearPlayerList()
    {

    }


    public void OnJoinTeam(string team)
    {
        if (team == "A")
        {
            //TeamAcount++;
            //teamADictionary.Add(PlayerPrefs.GetString("PlayerNickname"), 0);
            if (Object.HasInputAuthority)
            {
                RPC_TeamAdd(PlayerPrefs.GetString("PlayerNickname"), "A", 0);
            }
            Debug.Log("AÆÀ Âü°¡");
        }
        if (team == "B")
        {
            //TeamBcount++;
            //teamBDictionary.Add(PlayerPrefs.GetString("PlayerNickname"), 0);
            if (Object.HasInputAuthority)
            {
                RPC_TeamAdd(PlayerPrefs.GetString("PlayerNickname"), "B", 0);
            }
            Debug.Log("BÆÀ Âü°¡");
        }
    }
}