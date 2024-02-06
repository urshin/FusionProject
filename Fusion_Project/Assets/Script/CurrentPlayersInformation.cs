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
    public CharacterSelectPanel characterSelectPanel;
    public WaitingPanelHandler waitingPanelHandler;

    public InGameUIHandler inGameUIHandler;


    public static CurrentPlayersInformation instance;
    void Awake()
    {
        characterSelectPanel = FindObjectOfType<CharacterSelectPanel>();
        waitingPanelHandler = FindObjectOfType<WaitingPanelHandler>();
        inGameUIHandler = FindObjectOfType<InGameUIHandler>();
        if (instance != null)
        {

            return;
        }
        else
        {
            instance = this;
        }

    }


    [Networked]
    public int TeamAcount { get; set; }
    [Networked]
    public int TeamBcount { get; set; }

    [Networked]
    public int currentplayer { get; set; }
    public int maxPlayer { get; set; }




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
                    characterSelectPanel.ShowTeamInfo();
                    break;

                case nameof(teamBDictionary):
                    characterSelectPanel.ShowTeamInfo();
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
            teamADictionary.Add("TestPlayer", 0);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            print(PlayerPrefs.GetString("PlayerNickname"));
        }

    }

    public void ClearPlayerList()
    {

    }


    public void OnJoinTeam(string team)
    {
        if (team == "A")
        {
            TeamAcount++;
            teamADictionary.Add(PlayerPrefs.GetString("PlayerNickname"), 0);

        }
        if (team == "B")
        {
            TeamBcount++;
            teamBDictionary.Add(PlayerPrefs.GetString("PlayerNickname"), 0);

        }
    }

}
