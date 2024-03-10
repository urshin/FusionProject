using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameTeamInfos : NetworkBehaviour
{
    
    public enum GameState
    {
        CharactorSelect,
        Ready,
        Gaming,
        End,
        Restart,
    }
  
    //public GameState gameState = new GameState();

    [Networked]
    public GameState gameState { get;set;}

    //캐릭터
    public List<GameObject> charactor = new List<GameObject>();




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


    [Networked, Capacity(6)]
    public NetworkDictionary<NetworkString<_32>, int> playerAlive { get; }
// Optional initialization
= MakeInitializer(new Dictionary<NetworkString<_32>, int> { });






    [Networked]
    public NetworkBool isStartBTNOn { get; set; }   //ui에서 방장이 게임 시작 버튼 눌렀을 때


    public ChangeDetector _changeDetector;


    [Header("SpawnPoint")]
    public Transform[] spawnPoint;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        isStartBTNOn = false;


    }

    private void Update()
    {

    }

    public TickTimer startTimer = TickTimer.None;

    public override void FixedUpdateNetwork()
    {




        if (startTimer.Expired(Runner) && gameState == GameState.Ready)
        {
            gameState = GameState.Gaming;
            isStartBTNOn = false;
            return;
        }
        if (gameState == GameState.End)
        {


            teamADictionary.Clear();

            teamBDictionary.Clear();

            foreach (var player in teamAll)
            {
                teamAll.Set(player.Key, 0);
            }

            foreach (var player in playerAlive)
            {
                playerAlive.Set(player.Key, 1);
            }
            DestroyPlayerBody();
            gameState = GameState.CharactorSelect;
        }

      






    }
    
    public void DestroyPlayerBody()
    {
        foreach(var player in teamAll)
        {
            NetworkObject playerbody = GameObject.Find(player.Key.ToString()).GetComponent<CurrentPlayer>().playerBody;
            if(playerbody.transform.childCount != 0)
            Runner.Despawn(playerbody.transform.GetChild(0).GetComponent<NetworkObject>());
        }
    }


}
