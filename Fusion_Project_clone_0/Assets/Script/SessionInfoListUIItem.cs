using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class SessionInfoListUIItem : MonoBehaviour
{
    // UI 요소들을 참조하는 변수들
    public TextMeshProUGUI sessionNameText;
    public TextMeshProUGUI playerCountText;
    public Button joinButton;

    // 세션 정보를 저장하는 변수
    SessionInfo sessionInfo;

    // JoinSession 이벤트를 처리할 델리게이트
    public event Action<SessionInfo> OnJoinSession;

    // 세션 정보를 설정하는 메서드
    public void SetInformation(SessionInfo sessionInfo)
    {
        this.sessionInfo = sessionInfo;

        // UI 텍스트 업데이트
        sessionNameText.text = sessionInfo.Name;
        playerCountText.text = $"{sessionInfo.PlayerCount.ToString()}/{sessionInfo.MaxPlayers.ToString()}";

        // 플레이어 수에 따라 Join 버튼 활성/비활성화
        bool isJoinButtonActive = true;
        if (sessionInfo.PlayerCount >= sessionInfo.MaxPlayers)
            isJoinButtonActive = false;
        joinButton.gameObject.SetActive(isJoinButtonActive);
    }

    // 버튼 클릭 시 호출되는 메서드
    public void OnClick()
    {
        // JoinSession 이벤트를 호출합니다.
        OnJoinSession?.Invoke(sessionInfo);
    }
}