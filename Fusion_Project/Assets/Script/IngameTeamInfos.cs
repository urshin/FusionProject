using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngameTeamInfos : NetworkBehaviour
{

    //공용 Dictionary
    [Networked, Capacity(3)]
    public NetworkDictionary<NetworkString<_32>, int> teamADictionary { get; }
 // Optional initialization
 = MakeInitializer(new Dictionary<NetworkString<_32>, int> { });

    [Networked, Capacity(3)]
    public NetworkDictionary<NetworkString<_32>, int> teamBDictionary { get; }
// Optional initialization
= MakeInitializer(new Dictionary<NetworkString<_32>, int> { });

    [Networked, Capacity(6)]
    public NetworkDictionary<NetworkString<_32>, int> teamAll { get; }
// Optional initialization
= MakeInitializer(new Dictionary<NetworkString<_32>, int> { });


    [Networked]
    public NetworkBool isChangeJob { get; set; }   //ui에서 방장이 게임 시작 버튼 눌렀을 때


    [Networked]
    public NetworkBool isStartBTNOn {  get; set; }   //ui에서 방장이 게임 시작 버튼 눌렀을 때


    public ChangeDetector _changeDetector;


    [Header("SpawnPoint")]
    public Transform[] spawnPoint;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
  


    }

    private void Update()
    {
        
    }

   


 

}
