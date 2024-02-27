using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InGameUIHandler;

public class InGameUIHandler : MonoBehaviour
{
    [SerializeField] CurrentPlayer crueentPlayerInformationPrefab;
  
    [Header("패널들")]
    [SerializeField] GameObject teamSelectPanel;
    [SerializeField] GameObject characterSelecPanel;
    [SerializeField] GameObject WaitingPanel;
    [SerializeField] GameObject playerStatePanel;

    [SerializeField] GameObject playerinterfaceUI;
    


    [Header("상태 TMP")]
    [SerializeField] TextMeshProUGUI ProgressTMP;



    [Header("플레이어 정보")]
    [SerializeField] Slider playerDashSlider;


    NetworkRunner runner;



    private void Awake()
    {
    
      

    }


    private void Start()
    {


       // ShowAllPanel();
        //패널 숨김으로 초기화
        HideAllPanel();
        teamSelectPanel.SetActive(true);
       



    }



    public void OnClickTeamSelect()
    {
        
        HideAllPanel();

        characterSelecPanel.SetActive(true);
      
    }


    public void OnClickCharacterSelect()
    {
        HideAllPanel();

        WaitingPanel.SetActive(true);
     
    }


    

    public void OnclickStartBTN()
    {
        HideAllPanel();
        playerinterfaceUI.SetActive(true);

    }

    private void Update()
    {
        //플레이어 정보
        if(Input.GetKey(KeyCode.Tab))
        {
            playerStatePanel.SetActive(true);
        }
        else playerStatePanel.SetActive(false);


    

    }


    void HideAllPanel()
    {
       
        teamSelectPanel.SetActive(false);
        characterSelecPanel.SetActive(false);
        WaitingPanel.SetActive(false);
        playerStatePanel.SetActive(false);
        playerinterfaceUI.SetActive(false);
    }
   

}
