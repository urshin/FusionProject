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


    //ĳ����
    public List<GameObject> charactor = new List<GameObject>();




    //���� Dictionary
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


    // ���� �Ҹ����� ���� ��� ���Դϴ�.
    [Networked] int _intToggle { get; set; }

    // �� �Ӽ��� ���� tick�� ��� ���� �ڵ����� ���ڵ��ϸ鼭�� �Ϲ����� �Ҹ���ó�� �۵��մϴ�.
    public bool TickToggle
    {
        get => _intToggle > 0; // �̰��� 0�� �ƴ� ��� ���� ��ȯ�մϴ�.
        set => _intToggle = value ? Runner.Tick : -Runner.Tick; // ���� �������� ��� ���� �����մϴ�.
    }

    private Tick toggleLastChangedTick => _intToggle >= 0 ? _intToggle : -_intToggle; // ����� ���������� ����� tick�� ��ȯ�մϴ�.



    [Networked]
    public NetworkBool isStartBTNOn {  get; set; }   //ui���� ������ ���� ���� ��ư ������ ��


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
