using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // �ٸ� ��ũ��Ʈ���� �׼����� �� �ֵ��� GameManager�� ���� �ν��Ͻ�
    public static DataManager instance = null;
    private void Awake()
    {
        // GameManager�� �ν��Ͻ��� ������ ���� �ν��Ͻ��� �����ϰ�, �̹� �ִٸ� �ߺ��� �ν��Ͻ��� �����մϴ�.
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �� ��ȯ �ÿ��� ���� �Ŵ����� �ı����� �ʵ��� �����մϴ�.
        DontDestroyOnLoad(gameObject);

    }


    public List<Sprite> jobSprite = new List<Sprite>();



    // Character ������ ������ ����Ʈ

    public List<Character> characterList = new List<Character>();



    // Character Ŭ���� ����
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
        // Resources ���� ���� �̹��� ���ҽ��� �ε��Ͽ� jobSprite ����Ʈ�� �߰�
        Sprite[] sprites = Resources.LoadAll<Sprite>(""); // YourFolderPath�� ������ ���� ��η� ��ü�Ǿ�� �մϴ�.
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

