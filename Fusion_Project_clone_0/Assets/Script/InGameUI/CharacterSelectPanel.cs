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


    //���� ������ ĳ���� ������
    [SerializeField] HorizontalLayoutGroup teamCharactorInfo;
    //���� ���� ĳ�� ������ ���� ������
    [SerializeField] GameObject TeamInfoPrefab;


    //ĳ���� ���� �ҷ��ð�
    [SerializeField] HorizontalLayoutGroup CharacterContent;
    //ĳ���� info ������
    [SerializeField] GameObject PlayCharacterPrefab;

    

    public void Start()
    {
        
    }
    public void Update()
    {
            
    }





}
