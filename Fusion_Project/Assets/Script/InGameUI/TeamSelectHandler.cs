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

    [SerializeField] CurrentPlayer currentPlayersInformation;

    

    public void OnclickTeam(string team)
    {
        currentPlayersInformation.TeamSelect(team);
     


    }
    

}
