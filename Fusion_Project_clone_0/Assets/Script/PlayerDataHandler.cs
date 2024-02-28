using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerDataHandler;

public class PlayerDataHandler : NetworkBehaviour
{
    IngameTeamInfos ingameTeamInfos;
    CurrentPlayer currentPlayer;
    HitboxRoot hitboxRoot;
    PlayerMovementHandler movementHandler;

    public override void Spawned()
    {
        ingameTeamInfos = FindObjectOfType<IngameTeamInfos>();
        currentPlayer = GetComponent<CurrentPlayer>();
        movementHandler = GetComponent<PlayerMovementHandler>();
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

    }

    private ChangeDetector _changeDetector;

    [Networked] public byte hp { get; set; }
    [Networked] public bool isDead { get; set; }

    [System.Serializable]
    public class CharacterInfo
    {
        [Networked] public string job { get; set; }
        [Networked] public int HP { get; set; }
        [Networked] public int Speed { get; set; }
        [Networked] public int Attack { get; set; }
        [Networked] public float AttackSpeed { get; set; }
        [Networked] public int Defence { get; set; }
    }

    [SerializeField]
    public CharacterInfo characterInfo = new CharacterInfo();


    private void Start()
    {

        isDead = false;

    }


    private void Awake()
    {
        movementHandler = GetComponent<PlayerMovementHandler>();
        hitboxRoot = GetComponentInChildren<HitboxRoot>();
    }

    public void OnTakeDamage(byte damageAmount)
    {
        //Only take damage while alive
        if (isDead)
            return;

        //Ensure that we cannot flip the byte as it can't handle minus values.
        if (damageAmount > hp)
            damageAmount = hp;

        hp -= damageAmount;

        Debug.Log($"{Time.time} {transform.name} took damage got {hp} left ");

        //Player died
        if (hp <= 0)
        {
            

            Debug.Log($"{Time.time} {transform.name} died");

            

            isDead = true;
        }
    }

    public override void Render()
    {

        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(hp):

                    Debug.Log("체력이 변동 되었습니다");

                    break;
            }
        }
    }



    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SetPlayerData(RpcInfo info = default)
    {
        RPC_SetData(info.Source);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_SetData(PlayerRef messageSource)
    {
        UpdateCharacterInfo();
        UpdateMovementData();

    }



    public void UpdateMovementData()
    {
        movementHandler._cc.maxSpeed = characterInfo.Speed;

        movementHandler.FindingAnimator();


    }



    public void UpdateCharacterInfo()
    {
        if (ingameTeamInfos.teamAll.ContainsKey(gameObject.name))
        {

            var characterClass = "";
            switch (ingameTeamInfos.teamAll[gameObject.name])
            {
                case 1:
                    characterClass = "Warrior";
                    break;
                case 2:
                    characterClass = "Mage";
                    break;
                case 3:
                    characterClass = "Archer";
                    break;
                default:
                    // Handle default case if needed
                    break;
            }

            var info = DataManager.instance.characterList.Find(x => x.Class == characterClass);
            if (info != null)
            {
                characterInfo.job = info.Class;
                characterInfo.HP = info.HP;
                characterInfo.Speed = info.Speed;
                characterInfo.Attack = info.Attack;
                characterInfo.AttackSpeed = info.AttackSpeed;
                characterInfo.Defence = info.Defence;

            }
            else
            {
                // Handle case when info is not found
            }


        }



    }








}
