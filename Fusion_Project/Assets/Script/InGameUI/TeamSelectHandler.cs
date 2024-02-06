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

    CurrentPlayersInformation currentPlayersInformation;



    private void Awake()
    {
        currentPlayersInformation = FindObjectOfType<CurrentPlayersInformation>();
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
        
        
        PlayerPrefs.SetString("Team", "A");
        currentPlayersInformation.OnJoinTeam("A");
        



    }
    public void OnClickTeamB()
    {
        
        PlayerPrefs.SetString("Team", "B");
        currentPlayersInformation.OnJoinTeam("B");

    }




}
