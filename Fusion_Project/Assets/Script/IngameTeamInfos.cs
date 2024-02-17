using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameTeamInfos : NetworkBehaviour
{

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

    public ChangeDetector _changeDetector;

  


    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
       
    }
    //public override void Render()
    //{
    //    foreach (var change in _changeDetector.DetectChanges(this))
    //    {
    //        switch (change)
    //        {
              
    //        }
    //    }
    //}

 

}
