using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngameTeamInfos : NetworkBehaviour
{

    public enum GameState
    {
        CharactorSelect,
        Ready,
        Gaming,
        End,
    }

   public  GameState gameState = new GameState();


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


    // 가상 불리언을 위한 백업 값입니다.
    [Networked] int _intToggle { get; set; }

    // 이 속성은 현재 tick을 백업 값에 자동으로 인코딩하면서도 일반적인 불리언처럼 작동합니다.
    public bool TickToggle
    {
        get => _intToggle > 0; // 이것은 0이 아닌 경우 참을 반환합니다.
        set => _intToggle = value ? Runner.Tick : -Runner.Tick; // 값을 기준으로 백업 값을 설정합니다.
    }

    private Tick toggleLastChangedTick => _intToggle >= 0 ? _intToggle : -_intToggle; // 토글이 마지막으로 변경된 tick을 반환합니다.



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

   public TickTimer startTimer = TickTimer.None;
    
    public override void FixedUpdateNetwork()
    {

        


        if (startTimer.Expired(Runner) && gameState == GameState.Ready)
        {
            gameState = GameState.Gaming;

            return;
        }

    }




}
