using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [Header("�гε�")]
    public GameObject playerNickNamePanel;
    public GameObject sessionBrowserPanel;
    public GameObject createSessionPanel;
    public GameObject CharacterSelectPanel;
    public GameObject statusPanel;

    [Header("�÷��̾� ����")]
    public TMP_InputField playerNameInputField;

    [Header("���ο� ���� ����")]
    public TMP_InputField sessionNameInputField;

    [Header("�÷��̾� ����")]
    public byte characterNum;

    void Start()
    {
        // �÷��̾� �г����� ����Ǿ� �ִٸ� �Է� �ʵ忡 �����մϴ�.
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
    // "Find Game" ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnFindGameClicked()
    {
        // �÷��̾� �г����� �����ϰ� ���� �Ŵ����� �����մϴ�.
        PlayerPrefs.SetString("PlayerNickname", playerNameInputField.text);
        GameManager.instance.PlayerNickname = playerNameInputField.text;
        PlayerPrefs.Save();

        // ��Ʈ��ũ ���� �ڵ鷯�� ã�Ƽ� �κ� ������ �����մϴ�.
        FusionLuncher fusionLuncher = FindObjectOfType<FusionLuncher>();
        fusionLuncher.OnJoinLobby();

        // ��� �г��� ����� ���� ������ �г��� Ȱ��ȭ�մϴ�.
        HideAllPanels();
        sessionBrowserPanel.gameObject.SetActive(true);
        FindObjectOfType<SessionListUIHandler>(true).OnLookingForGameSessions();
    }



    // "Create New Game" ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnCreateNewGameClicked()
    {
        // ��� �г��� ����� ���ο� ���� ���� ���� �г��� Ȱ��ȭ�մϴ�.
        HideAllPanels();
        createSessionPanel.SetActive(true);
    }

    // "Start New Session" ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnStartNewSessionClicked()
    {
        //��Ʈ��ũ ���� �ڵ鷯�� ã�Ƽ� ���ο� ���� ������ �����մϴ�.
        FusionLuncher fusionLuncher = FindObjectOfType<FusionLuncher>();
        fusionLuncher.CreateGame(sessionNameInputField.text, "Test");
        HideAllPanels();
    }


    // ������ ���� ���� �� ȣ��Ǵ� �޼���
    public void OnJoiningServer()
    {
        // ��� �г��� ����� ���� �г��� Ȱ��ȭ�մϴ�.
        HideAllPanels();
        statusPanel.gameObject.SetActive(true);
    }
}
