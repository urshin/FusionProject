using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIHandler : MonoBehaviour
{
    [Header("패널들")]
    [SerializeField] GameObject teamSelectPanel;
    [SerializeField] GameObject characterSelecPanel;
    [SerializeField] GameObject WaitingPanel;
    [SerializeField] GameObject playerStatePanel;


    [Header("상태 TMP")]
    [SerializeField] TextMeshProUGUI ProgressTMP;

    IngamePlayerInfo playerInfo;


    NetworkRunner runner;


    private void Start()
    {
        //패널 숨김으로 초기화
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
        //플레이어 정보
        if(Input.GetKey(KeyCode.Tab))
        {
            playerStatePanel.SetActive(true);
        }
        else playerStatePanel.SetActive(false);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            runner = FindAnyObjectByType<NetworkRunner>();
            print("현재 세션 플레이어 카운트"+ runner.SessionInfo.PlayerCount.ToString());
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
