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

    public List<Character> characterList = new List<Character>();



    // Character 클래스 정의
    [System.Serializable]
    public class Character
    {
        public GameObject Prefab;
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
        ImageSource();
        LoadCharacterInfo("Assets/Resources/CharacterInfo.txt");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Character c in characterList)
            {
                print(c);
            }
        }
    }

    public void ImageSource()
    {
        // Resources 폴더 내의 이미지 리소스를 로드하여 jobSprite 리스트에 추가
        Sprite[] sprites = Resources.LoadAll<Sprite>(""); // YourFolderPath는 실제로 폴더 경로로 대체되어야 합니다.
        jobSprite.AddRange(sprites);
    }

    void LoadCharacterInfo(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        Character character = null;

        foreach (string line in lines)
        {
            if (line.StartsWith("//") || string.IsNullOrWhiteSpace(line))
            {
                continue; // Skip comments and empty lines
            }
            else if (line.StartsWith("Class:"))
            {
                if (character != null)
                {
                    characterList.Add(character); // Add the previous character
                }
                character = new Character(); // Create new character instance
                character.Class = line.Split(':')[1].Trim(); // Extract class name
                character.Prefab = Resources.Load<GameObject>(character.Class);
            }
            else
            {
                string[] parts = line.Split(':');
                string attribute = parts[0].Trim();
                string value = parts[1].Trim();

                switch (attribute)
                {
                    case "HP":
                        character.HP = int.Parse(value);
                        break;
                    case "Speed":
                        character.Speed = int.Parse(value);
                        break;
                    case "Attack":
                        character.Attack = int.Parse(value);
                        break;
                    case "AttackSpeed":
                        character.AttackSpeed = float.Parse(value);
                        break;
                    case "Deffence": // Typo in your text file, should be "Defence"
                        character.Defence = int.Parse(value);
                        break;
                    default:
                        break;
                }
            }
        }

        if (character != null)
        {
            characterList.Add(character); // Add the last character read
        }
    }

}

