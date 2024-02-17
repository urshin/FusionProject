using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [Header("패널들")]
    public GameObject playerNickNamePanel;
    public GameObject sessionBrowserPanel;
    public GameObject createSessionPanel;
    public GameObject CharacterSelectPanel;
    public GameObject statusPanel;

    [Header("플레이어 설정")]
    public TMP_InputField playerNameInputField;

    [Header("새로운 게임 세션")]
    public TMP_InputField sessionNameInputField;

    [Header("플레이어 선택")]
    public byte characterNum;

    void Start()
    {
        // 플레이어 닉네임이 저장되어 있다면 입력 필드에 설정합니다.
        if (PlayerPrefs.HasKey("PlayerNickname"))
            playerNameInputField.text = PlayerPrefs.GetString("PlayerNickname");
    }

    void HideAllPanels()
    {
        playerNickNamePanel.SetActive(false);
        sessionBrowserPanel.SetActive(false);
        createSessionPanel.SetActive(false);
        CharacterSelectPanel.SetActive(false);
        statusPanel.SetActive(false);

    }
    // "Find Game" 버튼 클릭 시 호출되는 메서드
    public void OnFindGameClicked()
    {
        // 플레이어 닉네임을 저장하고 게임 매니저에 설정합니다.
        PlayerPrefs.SetString("PlayerNickname", playerNameInputField.text);
        GameManager.instance.PlayerNickname = playerNameInputField.text;
        PlayerPrefs.Save();

        // 네트워크 러너 핸들러를 찾아서 로비 참여를 시작합니다.
        FusionLuncher fusionLuncher = FindObjectOfType<FusionLuncher>();
        fusionLuncher.OnJoinLobby();

        // 모든 패널을 숨기고 세션 브라우저 패널을 활성화합니다.
        HideAllPanels();
        sessionBrowserPanel.gameObject.SetActive(true);
        FindObjectOfType<SessionListUIHandler>(true).OnLookingForGameSessions();
    }



    // "Create New Game" 버튼 클릭 시 호출되는 메서드
    public void OnCreateNewGameClicked()
    {
        // 모든 패널을 숨기고 새로운 게임 세션 생성 패널을 활성화합니다.
        HideAllPanels();
        createSessionPanel.SetActive(true);
    }

    // "Start New Session" 버튼 클릭 시 호출되는 메서드
    public void OnStartNewSessionClicked()
    {
        //네트워크 러너 핸들러를 찾아서 새로운 게임 세션을 생성합니다.
        FusionLuncher fusionLuncher = FindObjectOfType<FusionLuncher>();
        fusionLuncher.CreateGame(sessionNameInputField.text, "Test");
        HideAllPanels();
    }


    // 서버에 참여 중일 때 호출되는 메서드
    public void OnJoiningServer()
    {
        // 모든 패널을 숨기고 상태 패널을 활성화합니다.
        HideAllPanels();
        statusPanel.gameObject.SetActive(true);
    }
}
