using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using static InGameUIHandler;

public class InGameUIHandler : MonoBehaviour
{
    [SerializeField] CurrentPlayer crueentPlayerInformationPrefab;
  
    [Header("패널들")]
    [SerializeField] GameObject teamSelectPanel;
    [SerializeField] GameObject characterSelecPanel;
    [SerializeField] GameObject WaitingPanel;
    [SerializeField] GameObject playerStatePanel;


    [Header("상태 TMP")]
    [SerializeField] TextMeshProUGUI ProgressTMP;



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
    }
    void ShowAllPanel()
    {

        teamSelectPanel.SetActive(true);
        characterSelecPanel.SetActive(true);
        WaitingPanel.SetActive(true);
        playerStatePanel.SetActive(true);
    }


}
