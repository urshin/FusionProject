using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingPanelHandler : MonoBehaviour
{
    public CurrentPlayersInformation currentPlayersInformation;

    [SerializeField] TextMeshProUGUI PlayerCounting;

    //���� ������ �ִ� ����� active��Ű��
    [SerializeField] GameObject PlayBTN;

    [SerializeField] VerticalLayoutGroup TeamA;
    [SerializeField] VerticalLayoutGroup TeamB;



    private void Awake()
    {
       
    }
    public void OnEnable()
    {
        //InvokeRepeating("ShowWaitingPlayer", 0, 0.5f);
    }






  
 
}


