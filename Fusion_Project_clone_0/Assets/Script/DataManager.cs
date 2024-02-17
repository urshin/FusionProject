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
    List<Character> characterList = new List<Character>();



    // Character Ŭ���� ����
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
        // Resources ���� ���� �̹��� ���ҽ��� �ε��Ͽ� jobSprite ����Ʈ�� �߰�
        Sprite[] sprites = Resources.LoadAll<Sprite>("Assets/Resources/"); // YourFolderPath�� ������ ���� ��η� ��ü�Ǿ�� �մϴ�.
        jobSprite.AddRange(sprites);
    }

    public void ReadCharacterInfo()
    {

        string filePath = "Assets/Resources/CharacterInfo.txt";
        // ������ �����ϴ��� Ȯ��
        if (File.Exists(filePath))
        {
            // ���� ���� �� �پ� �б�
            string[] lines = File.ReadAllLines(filePath);

            // �� ���� ó���Ͽ� Character ��ü�� ��ȯ�ϰ� ����Ʈ�� �߰�
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

            //// �о�� ���� ��� 
            //foreach (Character character in characterList)
            //{
            //    Debug.Log("Class: " + character.Class + ", HP: " + character.HP + ", Speed: " + character.Speed + ", Attack: " + character.Attack + ", AttackSpeed: " + character.AttackSpeed + ", Defence: " + character.Defence);
            //}
        }
        else
        {
            Debug.LogError("CharacterInfo.txt ������ ã�� �� �����ϴ�.");
        }
    }



}
