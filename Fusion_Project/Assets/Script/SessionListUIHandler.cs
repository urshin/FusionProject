using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class SessionListUIHandler : MonoBehaviour
{
    // UI ��ҵ��� �����ϴ� ������
    public TextMeshProUGUI statusText;
    public GameObject sessionItemListPrefab;
    public VerticalLayoutGroup verticalLayoutGroup;

    // Awake �޼��忡�� ����Ʈ �ʱ�ȭ
    private void Awake()
    {
        ClearList();
    }

    // ����Ʈ�� �ʱ�ȭ�ϴ� �޼���
    public void ClearList()
    {
        // VerticalLayoutGroup�� ��� �ڽĵ��� ����
        foreach (Transform child in verticalLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // ���� �޽����� ����ϴ�.
        statusText.gameObject.SetActive(false);
    }


    // ����Ʈ�� ������ �߰��ϴ� �޼���
    public void AddToList(SessionInfo sessionInfo)
    {
        // ����Ʈ�� ���ο� �������� �߰��մϴ�.
        SessionInfoListUIItem addedSessionInfoListUIItem = Instantiate(sessionItemListPrefab, verticalLayoutGroup.transform).GetComponent<SessionInfoListUIItem>();

        addedSessionInfoListUIItem.SetInformation(sessionInfo);

        // �̺�Ʈ�� �����մϴ�.
        addedSessionInfoListUIItem.OnJoinSession += AddedSessionInfoListUIItem_OnJoinSession;
    }

    // ���� �������� Join ��ư Ŭ�� �� ȣ��Ǵ� �̺�Ʈ �ڵ鷯
    private void AddedSessionInfoListUIItem_OnJoinSession(SessionInfo sessionInfo)
    {
        // ��Ʈ��ũ ���� �ڵ鷯�� ã�Ƽ� ���ǿ� �����մϴ�.
        FusionLuncher fusionLuncher = FindObjectOfType<FusionLuncher>();
        fusionLuncher.JoinGame(sessionInfo);

        // ���� �޴� UI �ڵ鷯�� ã�Ƽ� ������ ���� ������ �˸��ϴ�.
        MainMenuHandler mainMenuUIHandler = FindObjectOfType<MainMenuHandler>();
        mainMenuUIHandler.OnJoiningServer();
    }

    // ���� ������ ���� �� ȣ��Ǵ� �޼���
    public void OnNoSessionsFound()
    {
        // ����Ʈ�� �ʱ�ȭ�ϰ� ���� �޽����� �����մϴ�.
        ClearList();
        statusText.text = "No game session found";
        statusText.gameObject.SetActive(true);
    }

    // ���� ������ ã�� ���� �� ȣ��Ǵ� �޼���
    public void OnLookingForGameSessions()
    {
        // ����Ʈ�� �ʱ�ȭ�ϰ� ���� �޽����� �����մϴ�.
        ClearList();
        statusText.text = "Looking for game sessions";
        statusText.gameObject.SetActive(true);
    }
}