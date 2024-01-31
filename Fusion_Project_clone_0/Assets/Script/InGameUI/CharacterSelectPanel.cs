using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    

    public void Start()
    {
        
    }
    public void Update()
    {
            
    }





}
