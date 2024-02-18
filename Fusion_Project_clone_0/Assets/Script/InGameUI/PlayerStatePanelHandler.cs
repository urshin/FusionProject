using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatePanelHandler : MonoBehaviour
{
    // ���� �÷��̾� ������ �ΰ��� �� ������ �����ϴ� ������
    public CurrentPlayersInformation currentPlayersInformation;
    public IngameTeamInfos ingameTeamInfos;

    // �÷��̾� ���� ǥ���ϴ� �ؽ�Ʈ
    public TextMeshProUGUI PlayerCounting;


    // �� A�� �� B�� ���� ���̾ƿ� �׷�
    [SerializeField] VerticalLayoutGroup TeamA;
    [SerializeField] VerticalLayoutGroup TeamB;

    private void Awake()
    {
        // �ʱ�ȭ �ڵ尡 ���� ��� ���⿡ �߰�
    }

    // Ȱ��ȭ�� �� ȣ��Ǵ� �Լ�
    public void OnEnable()
    {
        // ���� �� ������ ������
        ingameTeamInfos = currentPlayersInformation.ingameTeamInfos;

       

        // ���� �÷��̾ ������ �÷��̾� �� ������Ʈ
        if (ingameTeamInfos.teamAll.Count > 0)
        {
            PlayerCounting.text = ingameTeamInfos.teamAll.Count.ToString();
        }

        // ��� ���� ������Ʈ
        UpdatePlayerStatePanel();

    }

    // ��� ���� ������Ʈ �Լ�
    public void UpdatePlayerStatePanel()
    {
        // �� A�� �� B�� �÷��̾� �� ��������
        int teamACount = ingameTeamInfos.teamADictionary.Count;
        int teamBCount = ingameTeamInfos.teamBDictionary.Count;

        // ��ü �÷��̾� �� ǥ��
        PlayerCounting.text = (teamACount + teamBCount).ToString();

        // �� A�� �� B�� ��� �÷��̾� ��Ȱ��ȭ
        DeactivateAllPlayers(TeamA);
        DeactivateAllPlayers(TeamB);

        // �� A ������Ʈ
        UpdateTeam(ingameTeamInfos.teamADictionary, TeamA, teamACount);

        // �� B ������Ʈ
        UpdateTeam(ingameTeamInfos.teamBDictionary, TeamB, teamBCount);
    }

    // �ش� ���� ��� �÷��̾ ��Ȱ��ȭ�ϴ� �Լ�
    private void DeactivateAllPlayers(VerticalLayoutGroup team)
    {
        for (int i = 0; i < team.transform.childCount; i++)
        {
            team.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Ư�� ���� �÷��̾� ������Ʈ �Լ�
    private void UpdateTeam(NetworkDictionary<NetworkString<_32>, int> teamDictionary, VerticalLayoutGroup teamTransform, int count)
    {
        int j = 0;
        foreach (var player in teamDictionary)
        {
            GameObject currentPlayer = teamTransform.transform.GetChild(j).gameObject;
            currentPlayer.SetActive(true);
            currentPlayer.GetComponentInChildren<TextMeshProUGUI>().text = player.Key.ToString();

            Sprite playerSprite = null;
            switch (player.Value)
            {
                case 1:
                    playerSprite = GetSprite("Sword");
                    break;
                case 2:
                    playerSprite = GetSprite("Magic");
                    break;
                case 3:
                    playerSprite = GetSprite("Archer");
                    break;
            }

            if (playerSprite != null)
            {
                currentPlayer.GetComponentInChildren<Image>().sprite = playerSprite;
            }

            j++;
            if (j >= count) // ����ȭ: ��� �÷��̾ ������Ʈ�� ��� ���� ����
            {
                break;
            }
        }
    }

    // ��������Ʈ�� �������� �Լ�
    private Sprite GetSprite(string resourceName)
    {
        // ���ҽ����� ��������Ʈ�� �� ���� �ε�
        if (!loadedSprites.ContainsKey(resourceName))
        {
            loadedSprites[resourceName] = Resources.Load<Sprite>(resourceName);
        }
        return loadedSprites[resourceName];
    }

    // �ε�� ��������Ʈ�� �����ϴ� ��ųʸ�
    private Dictionary<string, Sprite> loadedSprites = new Dictionary<string, Sprite>();


}
