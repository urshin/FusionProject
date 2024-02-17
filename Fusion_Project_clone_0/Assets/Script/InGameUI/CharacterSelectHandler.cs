using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using static Fusion.NetworkBehaviour;
using Fusion;
using Unity.VisualScripting;
using System.Linq;
using static InGameUIHandler;
using UnityEngine.EventSystems;


public class CharacterSelectHandler : MonoBehaviour
{
    public CurrentPlayersInformation currentPlayersInformation;
    public IngameTeamInfos ingameTeamInfos;

    private void Awake()
    {

    }


    //���� ������ ĳ���� ������
    public HorizontalLayoutGroup teamCharactorInfo;
    //���� ���� ĳ�� ������ ���� ������
    [SerializeField] GameObject TeamInfoPrefab;


    //ĳ���� ���� �ҷ��ð�
    [SerializeField] HorizontalLayoutGroup CharacterContent;
    //ĳ���� info ������
    [SerializeField] GameObject PlayCharacterPrefab;


    //� ������
    [SerializeField] TextMeshProUGUI whatTeam;


    //ĳ���� ���� ������ �ִ� �ؽ�Ʈ ����
    string filePath = "Assets/Resources/CharacterInfo.txt";


    private void OnEnable()
    {
        ingameTeamInfos = currentPlayersInformation.ingameTeamInfos;
        whatTeam.text = PlayerPrefs.GetString("Team");
        if (File.Exists(filePath))
        {
            string characterInfoText = File.ReadAllText(filePath);
            string[] lines = File.ReadAllLines(filePath);

            int classCount = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("Class: Sword"))
                {
                    SetCharacterInfo("Sword", "SwordMan", "SwordBTN", classCount);
                }
                else if (line.StartsWith("Class: Magic"))
                {
                    SetCharacterInfo("Magic", "Magician", "MagicianBTN", classCount);
                }
                else if (line.StartsWith("Class: Archer"))
                {
                    SetCharacterInfo("Archer", "Archer", "ArcherBTN", classCount);
                }
            }

            void SetCharacterInfo(string resourceName, string text, string buttonName, int index)
            {
                Transform classTransform = CharacterContent.transform.GetChild(index);
                GameObject classinfo = classTransform.gameObject;
                classinfo.SetActive(true);
                classinfo.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(resourceName);
                classinfo.GetComponentInChildren<TextMeshProUGUI>().text = text;
                classinfo.GetComponentInChildren<Button>().name = buttonName;

                classCount++;
            }
            Debug.Log("class count is" + classCount);
        }
        else
        {
            Debug.LogError("������ �����ϴ� ");
        }

    }

    public void TeamClassChange(string name, string team)
    {
        for (int i = 0; i < teamCharactorInfo.transform.childCount; i++)
        {
            teamCharactorInfo.transform.GetChild(i).gameObject.SetActive(false);
        }



        if (ingameTeamInfos.teamADictionary.ContainsKey(name))
        {
            int i = 0;
            foreach (var A in ingameTeamInfos.teamADictionary)
            {
                GameObject currentPlayer = teamCharactorInfo.transform.GetChild(i).gameObject;
                currentPlayer.SetActive(true);
                currentPlayer.GetComponentInChildren<TextMeshProUGUI>().text = A.Key.ToString();
                if (A.Value == 1)
                {
                    currentPlayer.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sword");

                }
                else if (A.Value == 2)
                {
                    currentPlayer.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Magic");
                }
                else if (A.Value == 3)
                {
                    currentPlayer.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Archer");
                }
                else
                {
                    return;
                }

                i++;
            }
        }
        else
        {
            return;
        }

        if (ingameTeamInfos.teamBDictionary.ContainsKey(name))
        {
            int i = 0;
            foreach (var A in ingameTeamInfos.teamBDictionary)
            {
                GameObject currentPlayer = teamCharactorInfo.transform.GetChild(i).gameObject;
                currentPlayer.SetActive(true);
                currentPlayer.GetComponentInChildren<TextMeshProUGUI>().text = A.Key.ToString();
                if (A.Value == 1)
                {
                    currentPlayer.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sword");

                }
                else if (A.Value == 2)
                {
                    currentPlayer.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Magic");
                }
                else if (A.Value == 3)
                {
                    currentPlayer.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Archer");
                }
                else
                {
                    return;
                }

                i++;
            }
        }
        else
        {
            return;
        }
    }

    public void OnclickClassSelect()
    {

        currentPlayersInformation.ClassSelect(EventSystem.current.currentSelectedGameObject.name);
    }


}
