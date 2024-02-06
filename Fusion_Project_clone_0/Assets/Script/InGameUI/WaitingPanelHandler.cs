using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingPanelHandler : MonoBehaviour
{
    CurrentPlayersInformation currentPlayersInformation;


    [SerializeField] TextMeshProUGUI PlayerCounting;

    //���� ������ �ִ� ����� active��Ű��
    [SerializeField] GameObject PlayBTN;

    [SerializeField] VerticalLayoutGroup TeamA;
    [SerializeField] VerticalLayoutGroup TeamB;



    private void Awake()
    {
        currentPlayersInformation = FindObjectOfType<CurrentPlayersInformation>();
    }
    public void OnEnable()
    {
       // CurrentPlayer();
    }


    public void CurrentPlayer()
    {
        PlayerCounting.text = "Current Player :  " + (currentPlayersInformation.TeamAcount + currentPlayersInformation.TeamBcount).ToString();
    }


    



}
