using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    public int maxPlayer { get; set; }
    [Networked]
    public int currentplayer { get; set; }

   
    public Dictionary<string, int> currentPlayers { get; set; }

    public PlayerInfo[] playerInfos { get; set; }



    private void Start()
    {
        maxPlayer = 6;
        

    }



    private void Update()
    {
        


    }

   
    

}
