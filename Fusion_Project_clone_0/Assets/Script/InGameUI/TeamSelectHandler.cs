using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class TeamSelectHandler : NetworkBehaviour
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

    private ChangeDetector _changeDetector;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }
    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(currentPlayersInformation.TeamAcount):
                    print(currentPlayersInformation.TeamAcount);
                    TeamA.text = currentPlayersInformation.TeamAcount.ToString();
                    break;
            }
        }
    }
    private void Update()
    {
       // TeamA.text = currentPlayersInformation.TeamAcount.ToString();
    }

    //d
    public void OnClickTeamA()
    {
        currentPlayersInformation.TeamAcount++;
        print(currentPlayersInformation.TeamAcount);
        




    }
    public void OnClickTeamB()
    {
        currentPlayersInformation.TeamBcount++;
        print(currentPlayersInformation.TeamBcount);
       


    }




}
