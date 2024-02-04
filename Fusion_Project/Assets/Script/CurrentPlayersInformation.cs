using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Fusion.NetworkBehaviour;

public class CurrentPlayersInformation : NetworkBehaviour
{
    public static CurrentPlayersInformation instance;
    void Awake()
    {
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

    ////퓨젼 딕셔너리
    //[Networked]
    //[Capacity(4)] // Sets the fixed capacity of the collection
    //[UnitySerializeField] // Show this private property in the inspector.
    //private NetworkDictionary<NetworkString<_32>, float> playerDistances => default;



    [UnitySerializeField]
    PlayerInfo[] playerInfos { get; set; }


    public List<PlayerInfo> playerList { get; set; }



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
                case nameof(TeamAcount):
                    //print(TeamAcount);
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
       if(Input.GetKeyDown(KeyCode.T))
        {
           
        }


    }

    public void ClearPlayerList()
    {
        //플레이어 리스트 초기화 하기
        playerList.Clear();
    }

   
    

}
