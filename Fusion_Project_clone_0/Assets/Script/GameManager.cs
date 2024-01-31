using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �ٸ� ��ũ��Ʈ���� �׼����� �� �ֵ��� GameManager�� ���� �ν��Ͻ�
    public static GameManager instance = null;

    // �÷��̾� ���� ��ū�� �����ϴ� �迭
    byte[] connectionToken;


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

    void Start()
    {
        // ���� ��ū�� ��ȿ���� Ȯ���ϰ�, ��ȿ���� �ʴٸ� ���ο� ��ū�� �����մϴ�.
        if (connectionToken == null)
        {
            connectionToken = ConnectionTokenUtils.NewToken();
            Debug.Log($"Player connection token {ConnectionTokenUtils.HashToken(connectionToken)}");
        }
    }

    // �ٸ� ��ũ��Ʈ���� ���� ��ū�� �����ϴ� �޼���
    public void SetConnectionToken(byte[] connectionToken)
    {
        this.connectionToken = connectionToken;
    }

    // �ٸ� ��ũ��Ʈ���� ���� ��ū�� �������� �޼���
    public byte[] GetConnectionToken()
    {
        return connectionToken;
    }


    [Header("���� ���� ����")]
    public int playerCount;




    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerRef playerRef = new PlayerRef();
            print(playerRef.PlayerId);
        }
    }


}
