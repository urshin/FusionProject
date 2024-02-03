using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CharacterSelectPanel : MonoBehaviour
{

    CurrentPlayersInformation currentPlayersInformation;
    private void Awake()
    {
        currentPlayersInformation = FindObjectOfType<CurrentPlayersInformation>();
    }


    //현재 팀원의 캐릭터 정보들
    [SerializeField] HorizontalLayoutGroup teamCharactorInfo;
    //현재 팀원 캐릭 정보를 가질 프리팹
    [SerializeField] GameObject TeamInfoPrefab;


    //캐릭터 종류 불러올곳
    [SerializeField] HorizontalLayoutGroup CharacterContent;
    //캐릭터 info 프리팹
    [SerializeField] GameObject PlayCharacterPrefab;


    //캐릭터 정보 가지고 있는 텍스트 파일
    string filePath = "Assets/Resources/CharacterInfo.txt";

   

    public void Start()
    {
        if (File.Exists(filePath))
        {
            string characterInfoText = File.ReadAllText(filePath);

        }
        else
        {
            Debug.LogError("파일이 없습니다 ");
        }
    }
    public void Update()
    {

    }





}
