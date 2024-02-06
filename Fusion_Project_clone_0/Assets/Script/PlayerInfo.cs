using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Team
{
    teamA,
    teamB,
}

[System.Serializable]
public class PlayerInfo:INetworkStruct //구조체로 해보기 
{
    Team team = new Team();


    public string playerName;
    public float HP;
    public float Speed;
    public float Attack;
    public float AttackSpeed;
    public float defence;




}
