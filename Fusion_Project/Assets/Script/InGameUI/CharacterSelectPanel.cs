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


    //���� ������ ĳ���� ������
    [SerializeField] HorizontalLayoutGroup teamCharactorInfo;
    //���� ���� ĳ�� ������ ���� ������
    [SerializeField] GameObject TeamInfoPrefab;


    //ĳ���� ���� �ҷ��ð�
    [SerializeField] HorizontalLayoutGroup CharacterContent;
    //ĳ���� info ������
    [SerializeField] GameObject PlayCharacterPrefab;


    //ĳ���� ���� ������ �ִ� �ؽ�Ʈ ����
    string filePath = "Assets/Resources/CharacterInfo.txt";

   

    public void Start()
    {
        if (File.Exists(filePath))
        {
            string characterInfoText = File.ReadAllText(filePath);

        }
        else
        {
            Debug.LogError("������ �����ϴ� ");
        }
    }
    public void Update()
    {

    }





}
