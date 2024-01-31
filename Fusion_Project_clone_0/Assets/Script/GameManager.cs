using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 다른 스크립트에서 액세스할 수 있도록 GameManager의 정적 인스턴스
    public static GameManager instance = null;

    // 플레이어 연결 토큰을 저장하는 배열
    byte[] connectionToken;


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

    void Start()
    {
        // 연결 토큰이 유효한지 확인하고, 유효하지 않다면 새로운 토큰을 생성합니다.
        if (connectionToken == null)
        {
            connectionToken = ConnectionTokenUtils.NewToken();
            Debug.Log($"Player connection token {ConnectionTokenUtils.HashToken(connectionToken)}");
        }
    }

    // 다른 스크립트에서 연결 토큰을 설정하는 메서드
    public void SetConnectionToken(byte[] connectionToken)
    {
        this.connectionToken = connectionToken;
    }

    // 다른 스크립트에서 연결 토큰을 가져오는 메서드
    public byte[] GetConnectionToken()
    {
        return connectionToken;
    }


    [Header("세션 게임 정보")]
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
