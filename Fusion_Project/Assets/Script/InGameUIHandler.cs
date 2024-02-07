using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using static InGameUIHandler;

public class InGameUIHandler : MonoBehaviour
{
    [SerializeField] CurrentPlayersInformation CrueentPlayerInformation;

    [Header("�гε�")]
    [SerializeField] GameObject teamSelectPanel;
    [SerializeField] GameObject characterSelecPanel;
    [SerializeField] GameObject WaitingPanel;
    [SerializeField] GameObject playerStatePanel;


    [Header("���� TMP")]
    [SerializeField] TextMeshProUGUI ProgressTMP;



    NetworkRunner runner;




    private void Awake()
    {
       // Instantiate(CrueentPlayerInformation);
      
        teamSelectPanel.GetComponent<TeamSelectHandler>().currentPlayersInformation = CrueentPlayerInformation;
        characterSelecPanel.GetComponent<CharacterSelectHandler>().currentPlayersInformation = CrueentPlayerInformation;
        WaitingPanel.GetComponent<WaitingPanelHandler>().currentPlayersInformation = CrueentPlayerInformation;
        
    }


    private void Start()
    {
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
    }


}
