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
  
    [Header("�гε�")]
    [SerializeField] GameObject teamSelectPanel;
    [SerializeField] GameObject characterSelecPanel;
    [SerializeField] GameObject WaitingPanel;
    [SerializeField] GameObject playerStatePanel;

    [SerializeField] GameObject playerinterfaceUI;
    


    [Header("���� TMP")]
    [SerializeField] TextMeshProUGUI ProgressTMP;



    [Header("�÷��̾� ����")]
    [SerializeField] Slider playerDashSlider;


    NetworkRunner runner;



    private void Awake()
    {
    
      

    }


    private void Start()
    {


       // ShowAllPanel();
        //�г� �������� �ʱ�ȭ
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
        //�÷��̾� ����
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
