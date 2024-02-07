using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class TeamSelectHandler : MonoBehaviour
{
    public TextMeshProUGUI TeamA;
    public TextMeshProUGUI TeamB;


    public CurrentPlayersInformation currentPlayersInformation;

    private void Awake()
    {
     
    }


    private void OnEnable()
    {
       
    }

  
    private void Update()
    {
       // TeamA.text = currentPlayersInformation.TeamAcount.ToString();
    }

    //d
    public void OnClickTeamA()
    {

        PlayerPrefs.DeleteKey("Team");
        PlayerPrefs.SetString("Team", "A");


        currentPlayersInformation.teamAplayerList.Add(PlayerPrefs.GetString("PlayerNickname"));
        currentPlayersInformation.OnJoinTeam("A");
        



    }
    public void OnClickTeamB()
    {
        PlayerPrefs.DeleteKey("Team");
        PlayerPrefs.SetString("Team", "B");
        currentPlayersInformation.teamBplayerList.Add(PlayerPrefs.GetString("PlayerNickname"));
        currentPlayersInformation.OnJoinTeam("B");

    }




}
