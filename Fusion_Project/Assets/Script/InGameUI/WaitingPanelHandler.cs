using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingPanelHandler : MonoBehaviour
{
    public CurrentPlayersInformation currentPlayersInformation;

    [SerializeField] TextMeshProUGUI PlayerCounting;

    //방장 권한이 있는 사람만 active시키기
    [SerializeField] GameObject PlayBTN;

    [SerializeField] VerticalLayoutGroup TeamA;
    [SerializeField] VerticalLayoutGroup TeamB;



    private void Awake()
    {
       
    }
    public void OnEnable()
    {
        //InvokeRepeating("ShowWaitingPlayer", 0, 0.5f);
    }





    public void CurrentPlayer()
    {
        int teama = currentPlayersInformation.TeamAcount;
        int teamb = currentPlayersInformation.TeamBcount;
        PlayerCounting.text = "Current Player :  " + teama + teamb;
    }

  
    public void ShowWaitingPlayer()
    {
        HideAllplayer();
       
        if (PlayerPrefs.GetString("Team") == "A")
        {
            int j = 0;
            foreach (var kvp in currentPlayersInformation.teamADictionary)
            {
                // 캐릭터의 이미지 파일명
                string characterSpriteName = "";
                switch (kvp.Value)
                {
                    case 1:
                        characterSpriteName = "Sword";
                        break;
                    case 2:
                        characterSpriteName = "Magic";
                        break;
                    case 3:
                        characterSpriteName = "Archer";
                        break;

                    default:
                        return;
                }

                // 이미지 로드 및 설정
                Sprite characterSprite = Resources.Load<Sprite>(characterSpriteName);
                if (characterSprite != null)
                {
                    GameObject characterObject = TeamA.transform.GetChild(j).gameObject;
                    characterObject.GetComponentInChildren<Image>().sprite = characterSprite;
                    characterObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerNickname");
                    characterObject.SetActive(true);
                }
                else
                {
                    Debug.LogWarning($"Failed to load sprite for character: {characterSpriteName}");
                }

                j++;
            }
        }
        if (PlayerPrefs.GetString("Team") == "B")
        {
            int j = 0;
            foreach (var kvp in currentPlayersInformation.teamBDictionary)
            {
                // 캐릭터의 이미지 파일명
                string characterSpriteName = "";
                switch (kvp.Value)
                {
                    case 1:
                        characterSpriteName = "Sword";
                        break;
                    case 2:
                        characterSpriteName = "Magic";
                        break;
                    case 3:
                        characterSpriteName = "Archer";
                        break;

                    default:
                        return;
                }

                // 이미지 로드 및 설정
                Sprite characterSprite = Resources.Load<Sprite>(characterSpriteName);
                if (characterSprite != null)
                {
                    GameObject characterObject = TeamB.transform.GetChild(j).gameObject;
                    characterObject.GetComponentInChildren<Image>().sprite = characterSprite;
                    characterObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerNickname");
                    characterObject.SetActive(true);
                }
                else
                {
                    Debug.LogWarning($"Failed to load sprite for character: {characterSpriteName}");
                }

                j++;
            }
        }
    }

    void HideAllplayer()
    {
        foreach (Transform child in TeamA.transform)
        {
            
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in TeamB.transform)
        {

            child.gameObject.SetActive(false);
        }
    }
}


