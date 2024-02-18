using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngameTeamInfos : NetworkBehaviour
{

    //°ø¿ë Dictionary
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




    [Networked, Capacity(6)]
    public NetworkDictionary<NetworkObject, int> allPlayer { get; }
// Optional initialization
= MakeInitializer(new Dictionary<NetworkObject, int> { });



    [Networked]
    public NetworkBool isStartBTNOn {  get; set; }  


    public ChangeDetector _changeDetector;


    [Header("SpawnPoint")]
    [SerializeField] Transform[] spawnPoint;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
       
    }

    private void Update()
    {
        
    }

    public void SetPlayerPosition()
    {
        int i = 0;
        foreach (var player in teamADictionary)
        {
            GameObject playerprefab = GameObject.Find(player.Key.ToString()).transform.GetChild(1).gameObject;
            playerprefab.transform.position = spawnPoint[i].transform.position;
            i++;
        }

        foreach (var player in teamBDictionary)
        {
            GameObject playerprefab = GameObject.Find(player.Key.ToString());
            playerprefab.transform.position = spawnPoint[i].transform.position;
            i++;
        }

    }


 

}
