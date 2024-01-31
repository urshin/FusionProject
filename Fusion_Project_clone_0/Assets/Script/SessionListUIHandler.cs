using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class SessionListUIHandler : MonoBehaviour
{
    // UI 요소들을 참조하는 변수들
    public TextMeshProUGUI statusText;
    public GameObject sessionItemListPrefab;
    public VerticalLayoutGroup verticalLayoutGroup;

    // Awake 메서드에서 리스트 초기화
    private void Awake()
    {
        ClearList();
    }

    // 리스트를 초기화하는 메서드
    public void ClearList()
    {
        // VerticalLayoutGroup의 모든 자식들을 삭제
        foreach (Transform child in verticalLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // 상태 메시지를 숨깁니다.
        statusText.gameObject.SetActive(false);
    }


    // 리스트에 세션을 추가하는 메서드
    public void AddToList(SessionInfo sessionInfo)
    {
        // 리스트에 새로운 아이템을 추가합니다.
        SessionInfoListUIItem addedSessionInfoListUIItem = Instantiate(sessionItemListPrefab, verticalLayoutGroup.transform).GetComponent<SessionInfoListUIItem>();

        addedSessionInfoListUIItem.SetInformation(sessionInfo);

        // 이벤트를 연결합니다.
        addedSessionInfoListUIItem.OnJoinSession += AddedSessionInfoListUIItem_OnJoinSession;
    }

    // 세션 아이템의 Join 버튼 클릭 시 호출되는 이벤트 핸들러
    private void AddedSessionInfoListUIItem_OnJoinSession(SessionInfo sessionInfo)
    {
        // 네트워크 러너 핸들러를 찾아서 세션에 참여합니다.
        FusionLuncher fusionLuncher = FindObjectOfType<FusionLuncher>();
        fusionLuncher.JoinGame(sessionInfo);

        // 메인 메뉴 UI 핸들러를 찾아서 서버에 참여 중임을 알립니다.
        MainMenuHandler mainMenuUIHandler = FindObjectOfType<MainMenuHandler>();
        mainMenuUIHandler.OnJoiningServer();
    }

    // 게임 세션이 없을 때 호출되는 메서드
    public void OnNoSessionsFound()
    {
        // 리스트를 초기화하고 상태 메시지를 설정합니다.
        ClearList();
        statusText.text = "No game session found";
        statusText.gameObject.SetActive(true);
    }

    // 게임 세션을 찾는 중일 때 호출되는 메서드
    public void OnLookingForGameSessions()
    {
        // 리스트를 초기화하고 상태 메시지를 설정합니다.
        ClearList();
        statusText.text = "Looking for game sessions";
        statusText.gameObject.SetActive(true);
    }
}