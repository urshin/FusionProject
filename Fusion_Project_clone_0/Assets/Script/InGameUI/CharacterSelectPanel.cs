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

public class CharacterSelectPanel : MonoBehaviour
{

    CurrentPlayersInformation currentPlayersInformation;
    private void Awake()
    {
        currentPlayersInformation = FindObjectOfType<CurrentPlayersInformation>();
        currentPlayersInformation.characterSelectPanel = this;
    }


    //현재 팀원의 캐릭터 정보들
    public HorizontalLayoutGroup teamCharactorInfo;
    //현재 팀원 캐릭 정보를 가질 프리팹
    [SerializeField] GameObject TeamInfoPrefab;


    //캐릭터 종류 불러올곳
    [SerializeField] HorizontalLayoutGroup CharacterContent;
    //캐릭터 info 프리팹
    [SerializeField] GameObject PlayCharacterPrefab;


    //어떤 팀인지
    [SerializeField] TextMeshProUGUI whatTeam;


    //캐릭터 정보 가지고 있는 텍스트 파일
    string filePath = "Assets/Resources/CharacterInfo.txt";


    private void OnEnable()
    {
        currentPlayersInformation = FindObjectOfType<CurrentPlayersInformation>();
        currentPlayersInformation.characterSelectPanel = this;
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
                classinfo.GetComponentInChildren<Button>().onClick.AddListener(() => OnclickCharacterInfo(buttonName));

                classCount++;
            }
            Debug.Log("class count is" + classCount);




        }
        else
        {
            Debug.LogError("파일이 없습니다 ");
        }

    }


    public void Start()
    {
        //currentPlayersInformation = FindObjectOfType<CurrentPlayersInformation>();
        //currentPlayersInformation.characterSelectPanel = this;
        ////ShowTeamInfo();

        //if (File.Exists(filePath))
        //{
        //    string characterInfoText = File.ReadAllText(filePath);
        //    string[] lines = File.ReadAllLines(filePath);

        //    int classCount = 0;

        //    foreach (string line in lines)
        //    {
        //        if (line.StartsWith("Class: Sword"))
        //        {
        //            SetCharacterInfo("Sword", "SwordMan", "SwordBTN", classCount);
        //        }
        //        else if (line.StartsWith("Class: Magic"))
        //        {
        //            SetCharacterInfo("Magic", "Magician", "MagicianBTN", classCount);
        //        }
        //        else if (line.StartsWith("Class: Archer"))
        //        {
        //            SetCharacterInfo("Archer", "Archer", "ArcherBTN", classCount);
        //        }
        //    }

        //    void SetCharacterInfo(string resourceName, string text, string buttonName, int index)
        //    {
        //        Transform classTransform = CharacterContent.transform.GetChild(index);
        //        GameObject classinfo = classTransform.gameObject;
        //        classinfo.SetActive(true);
        //        classinfo.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(resourceName);
        //        classinfo.GetComponentInChildren<TextMeshProUGUI>().text = text;
        //        classinfo.GetComponentInChildren<Button>().name = buttonName;
        //        classCount++;
        //    }
        //    Debug.Log("class count is" + classCount);

        //    //for(int i = 0; i < classCount; i++)
        //    //{
        //    //    CharacterContent.transform.GetChild(i).gameObject.SetActive(true);
        //    //}


        //}
        //else
        //{
        //    Debug.LogError("파일이 없습니다 ");
        //}


    }
    public void Update()
    {
     
    }

    public void OnclickCharacterInfo(string name)
    {

        string team = PlayerPrefs.GetString("Team");
        string playerName = PlayerPrefs.GetString("PlayerNickname");
        int characterIndex = 0;

        switch (name)
        {
            case "SwordBTN":
                characterIndex = 1;
                break;
            case "MagicianBTN":
                characterIndex = 2;
                break;
            case "ArcherBTN":
                characterIndex = 3;
                break;
            default:
                //  Debug.LogWarning("Unknown button clicked: " + clickedButton.name);
                return;
        }

        if (team == "A")
        {
            currentPlayersInformation.teamADictionary.Set(playerName, characterIndex);
        }
        else if (team == "B")
        {
            currentPlayersInformation.teamBDictionary.Set(playerName, characterIndex);
        }
        else
        {
            Debug.LogWarning("Invalid team: " + team);
        }



    }





    public void ShowTeamInfo()
    {
        if (PlayerPrefs.GetString("Team") == "A")
        {
            int j = 0;
            foreach (var kvp in currentPlayersInformation.teamADictionary)
            {
                Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");

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
                    GameObject characterObject = teamCharactorInfo.transform.GetChild(j).gameObject;
                    characterObject.GetComponentInChildren<Image>().sprite = characterSprite;
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
                Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");

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
                    GameObject characterObject = teamCharactorInfo.transform.GetChild(j).gameObject;
                    characterObject.GetComponentInChildren<Image>().sprite = characterSprite;
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


    //public void ShowTeamInfo()
    //{
    //    string team = PlayerPrefs.GetString("Team");
    //    NetworkDictionary<NetworkString<Fusion._32>, float> teamDictionary = team == "A" ? currentPlayersInformation.teamADictionary : currentPlayersInformation.teamBDictionary;

    //    int j = 0;
    //    foreach (var kvp in teamDictionary)
    //    {
    //        Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");

    //        // 캐릭터의 이미지 파일명
    //        string characterSpriteName = "";
    //        switch (kvp.Value)
    //        {
    //            case 1:
    //                characterSpriteName = "Sword";
    //                break;
    //            case 2:
    //                characterSpriteName = "Magic";
    //                break;
    //            case 3:
    //                characterSpriteName = "Archer";
    //                break;

    //            default:
    //                Debug.LogWarning($"Invalid character value: {kvp.Value}");
    //                return;
    //        }

    //        // 이미지 로드 및 설정
    //        Sprite characterSprite = Resources.Load<Sprite>(characterSpriteName);
    //        if (characterSprite != null)
    //        {
    //            GameObject characterObject = teamCharactorInfo.transform.GetChild(j).gameObject;
    //            characterObject.GetComponentInChildren<Image>().sprite = characterSprite;
    //            characterObject.SetActive(true);
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"Failed to load sprite for character: {characterSpriteName}");
    //        }

    //        j++;
    //    }
    //}


}
