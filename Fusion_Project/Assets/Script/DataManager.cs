using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 다른 스크립트에서 액세스할 수 있도록 GameManager의 정적 인스턴스
    public static DataManager instance = null;
    private void Awake()
    {
        // GameManager의 인스턴스가 없으면 현재 인스턴스를 설정하고, 이미 있다면 중복된 인스턴스를 제거합니다.
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 씬 전환 시에도 게임 매니저를 파괴하지 않도록 설정합니다.
        DontDestroyOnLoad(gameObject);

    }


    public List<Sprite> jobSprite = new List<Sprite>();

    // Character 정보를 저장할 리스트
    List<Character> characterList = new List<Character>();



    // Character 클래스 정의
    public class Character
    {
        public string Class;
        public int HP;
        public int Speed;
        public int Attack;
        public float AttackSpeed;
        public int Defence;
    }







    // Start is called before the first frame update
    void Start()
    {
        ReadCharacterInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ImageSource()
    {
        // Resources 폴더 내의 이미지 리소스를 로드하여 jobSprite 리스트에 추가
        Sprite[] sprites = Resources.LoadAll<Sprite>("Assets/Resources/"); // YourFolderPath는 실제로 폴더 경로로 대체되어야 합니다.
        jobSprite.AddRange(sprites);
    }

    public void ReadCharacterInfo()
    {

        string filePath = "Assets/Resources/CharacterInfo.txt";
        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            // 파일 내용 한 줄씩 읽기
            string[] lines = File.ReadAllLines(filePath);

            // 각 줄을 처리하여 Character 객체로 변환하고 리스트에 추가
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
                {
                    string[] tokens = line.Split(':');
                    Character character = new Character();
                    character.Class = tokens[1].Trim();
                    character.HP = int.Parse(tokens[2].Trim());
                    character.Speed = int.Parse(tokens[3].Trim());
                    character.Attack = int.Parse(tokens[4].Trim());
                    character.AttackSpeed = float.Parse(tokens[5].Trim());
                    character.Defence = int.Parse(tokens[6].Trim());
                    characterList.Add(character);
                }
            }

            //// 읽어온 정보 출력 
            //foreach (Character character in characterList)
            //{
            //    Debug.Log("Class: " + character.Class + ", HP: " + character.HP + ", Speed: " + character.Speed + ", Attack: " + character.Attack + ", AttackSpeed: " + character.AttackSpeed + ", Defence: " + character.Defence);
            //}
        }
        else
        {
            Debug.LogError("CharacterInfo.txt 파일을 찾을 수 없습니다.");
        }
    }



}
