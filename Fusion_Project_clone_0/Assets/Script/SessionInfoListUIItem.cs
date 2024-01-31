using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class SessionInfoListUIItem : MonoBehaviour
{
    // UI ��ҵ��� �����ϴ� ������
    public TextMeshProUGUI sessionNameText;
    public TextMeshProUGUI playerCountText;
    public Button joinButton;

    // ���� ������ �����ϴ� ����
    SessionInfo sessionInfo;

    // JoinSession �̺�Ʈ�� ó���� ��������Ʈ
    public event Action<SessionInfo> OnJoinSession;

    // ���� ������ �����ϴ� �޼���
    public void SetInformation(SessionInfo sessionInfo)
    {
        this.sessionInfo = sessionInfo;

        // UI �ؽ�Ʈ ������Ʈ
        sessionNameText.text = sessionInfo.Name;
        playerCountText.text = $"{sessionInfo.PlayerCount.ToString()}/{sessionInfo.MaxPlayers.ToString()}";

        // �÷��̾� ���� ���� Join ��ư Ȱ��/��Ȱ��ȭ
        bool isJoinButtonActive = true;
        if (sessionInfo.PlayerCount >= sessionInfo.MaxPlayers)
            isJoinButtonActive = false;
        joinButton.gameObject.SetActive(isJoinButtonActive);
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnClick()
    {
        // JoinSession �̺�Ʈ�� ȣ���մϴ�.
        OnJoinSession?.Invoke(sessionInfo);
    }
}