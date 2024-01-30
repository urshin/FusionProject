using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIHandler : MonoBehaviour
{
    [Header("�гε�")]
    [SerializeField] GameObject teamSelectPanel;
    [SerializeField] GameObject characterSelecPanel;
    [SerializeField] GameObject WaitingPanel;
    [SerializeField] GameObject playerStatePanel;


    [Header("���� TMP")]
    [SerializeField] TextMeshProUGUI ProgressTMP;

    IngamePlayerInfo playerInfo;


    NetworkRunner runner;


    private void Start()
    {
        //�г� �������� �ʱ�ȭ
        HideAllPanel();
        playerInfo = new IngamePlayerInfo();
        teamSelectPanel.SetActive(true);
        
        
    }



    public void OnClickTeamSelect(string Team)
    {
        playerInfo.Team = Team;
        HideAllPanel();

        characterSelecPanel.SetActive(true);
    }


    public void OnClickCharacterSelect(string character)
    {
        playerInfo.character = character;
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            runner = FindAnyObjectByType<NetworkRunner>();
            print("���� ���� �÷��̾� ī��Ʈ"+ runner.SessionInfo.PlayerCount.ToString());
        }
    }




















    void HideAllPanel()
    {
        teamSelectPanel.SetActive(false);
        characterSelecPanel.SetActive(false);
        WaitingPanel.SetActive(false);
        playerStatePanel.SetActive(false);
    }


}
