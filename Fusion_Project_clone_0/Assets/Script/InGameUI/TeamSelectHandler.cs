using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class TeamSelectHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TeamA;
    [SerializeField] TextMeshProUGUI TeamB;

    CurrentPlayersInformation currentPlayersInformation;

    

    private void Awake()
    {
        currentPlayersInformation = FindObjectOfType<CurrentPlayersInformation>();
    }

    private void OnEnable()
    {
        TeamA.text = currentPlayersInformation.TeamAcount.ToString();
    }

    public void OnClickTeamA()
    {
        currentPlayersInformation.TeamAcount++;
        print( currentPlayersInformation.TeamAcount );
        currentPlayersInformation.currentPlayers.Add(PlayerPrefs.GetString("PlayerNickname"),1);
           

    }
    public void OnClickTeamB()
    {
        currentPlayersInformation.TeamBcount++;
        print(currentPlayersInformation.TeamBcount);


    }


    

}
