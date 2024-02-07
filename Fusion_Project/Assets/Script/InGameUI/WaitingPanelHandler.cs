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

    //���� ������ �ִ� ����� active��Ű��
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
                // ĳ������ �̹��� ���ϸ�
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

                // �̹��� �ε� �� ����
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
                // ĳ������ �̹��� ���ϸ�
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

                // �̹��� �ε� �� ����
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


