using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatePanelHandler : MonoBehaviour
{
    // 현재 플레이어 정보와 인게임 팀 정보를 저장하는 변수들
    public CurrentPlayersInformation currentPlayersInformation;
    public IngameTeamInfos ingameTeamInfos;

    // 플레이어 수를 표시하는 텍스트
    public TextMeshProUGUI PlayerCounting;


    // 팀 A와 팀 B의 수직 레이아웃 그룹
    [SerializeField] VerticalLayoutGroup TeamA;
    [SerializeField] VerticalLayoutGroup TeamB;

    private void Awake()
    {
        // 초기화 코드가 있을 경우 여기에 추가
    }

    // 활성화될 때 호출되는 함수
    public void OnEnable()
    {
        // 현재 팀 정보를 가져옴
        ingameTeamInfos = currentPlayersInformation.ingameTeamInfos;

       

        // 팀에 플레이어가 있으면 플레이어 수 업데이트
        if (ingameTeamInfos.teamAll.Count > 0)
        {
            PlayerCounting.text = ingameTeamInfos.teamAll.Count.ToString();
        }

        // 대기 상태 업데이트
        UpdatePlayerStatePanel();

    }

    // 대기 상태 업데이트 함수
    public void UpdatePlayerStatePanel()
    {
        // 팀 A와 팀 B의 플레이어 수 가져오기
        int teamACount = ingameTeamInfos.teamADictionary.Count;
        int teamBCount = ingameTeamInfos.teamBDictionary.Count;

        // 전체 플레이어 수 표시
        PlayerCounting.text = (teamACount + teamBCount).ToString();

        // 팀 A와 팀 B의 모든 플레이어 비활성화
        DeactivateAllPlayers(TeamA);
        DeactivateAllPlayers(TeamB);

        // 팀 A 업데이트
        UpdateTeam(ingameTeamInfos.teamADictionary, TeamA, teamACount);

        // 팀 B 업데이트
        UpdateTeam(ingameTeamInfos.teamBDictionary, TeamB, teamBCount);
    }

    // 해당 팀의 모든 플레이어를 비활성화하는 함수
    private void DeactivateAllPlayers(VerticalLayoutGroup team)
    {
        for (int i = 0; i < team.transform.childCount; i++)
        {
            team.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // 특정 팀의 플레이어 업데이트 함수
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
            if (j >= count) // 최적화: 모든 플레이어를 업데이트한 경우 루프 종료
            {
                break;
            }
        }
    }

    // 스프라이트를 가져오는 함수
    private Sprite GetSprite(string resourceName)
    {
        // 리소스에서 스프라이트를 한 번만 로드
        if (!loadedSprites.ContainsKey(resourceName))
        {
            loadedSprites[resourceName] = Resources.Load<Sprite>(resourceName);
        }
        return loadedSprites[resourceName];
    }

    // 로드된 스프라이트를 저장하는 딕셔너리
    private Dictionary<string, Sprite> loadedSprites = new Dictionary<string, Sprite>();


}
