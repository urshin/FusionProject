using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    // Remote Client Token Hash
    [Networked] public int token { get; set; }
    public NetworkString<_16> nickName { get; set; }


    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local player");
        }
        else
        {

            Debug.Log("Spawned remote player");

        }

        //Runner.SetPlayerObject(Object.InputAuthority, Object);

        GameManager.instance.playerId = $"P_{Object.Id}";
        transform.name = $"P_{Object.Id}";
        // transform.name = PlayerPrefs.GetString("PlayerNickname");
        // transform.name =  GameManager.instance.PlayerNickname;
    }


    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);

    }

   
}