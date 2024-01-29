using Fusion.Sockets;
using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Linq;

public class FusionLuncher : MonoBehaviour
{
    // spawner Prefab
    public NetworkRunner networkRunnerPrefab;

    // networkRunner
    NetworkRunner networkRunner;

    private void Awake()
    {
        // Scene에서 NetworkRunner를 찾아서 이미 존재하면 새로 생성하지 않고 기존 것을 사용합니다.
        NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();
        if (networkRunnerInScene != null)
            networkRunner = networkRunnerInScene;
    }

    void Start()
    {
        // 만약 networkRunner가 아직 생성되지 않았다면 새로운 인스턴스를 생성합니다.
        if (networkRunner == null)
        {
            networkRunner = Instantiate(networkRunnerPrefab);
            networkRunner.name = "Network runner";

            // 현재 활성화된 씬이 MainMenu이 아니라면 네트워크 러너를 초기화합니다.
            if (SceneManager.GetActiveScene().name != "MainLobby")
            {
                var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, "TestSession", GameManager.instance.GetConnectionToken(), NetAddress.Any(), SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), null);
            }
            Debug.Log($"Server NetworkRunner started.");
        }
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, string sessionName, byte[] connectionToken, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneManager = GetSceneManager(runner);

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = sessionName,
            CustomLobbyName = "OurLobbyID",
            OnGameStarted = initialized,
            SceneManager = sceneManager,
            ConnectionToken = connectionToken

        });
    }

    // NetworkSceneManager를 가져오는 메서드
    INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if (sceneManager == null)
        {
            // 씬에 이미 존재하는 네트워크 오브젝트를 처리합니다.
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return sceneManager;
    }

    // 로비에 참여하는 메서드
    public void OnJoinLobby()
    {
        var clientTask = JoinLobby();
    }

    private async Task JoinLobby()
    {
        Debug.Log("JoinLobby started");

        string lobbyID = "OurLobbyID";

        var result = await networkRunner.JoinSessionLobby(SessionLobby.Custom, lobbyID);

        if (!result.Ok)
        {
            Debug.LogError($"Unable to join lobby {lobbyID}");
        }
        else
        {
            Debug.Log("JoinLobby ok");
        }
    }

    // 게임을 생성하는 메서드
    public void CreateGame(string sessionName, string sceneName)
    {
        Debug.Log($"Create session {sessionName} scene {sceneName} build Index {SceneUtility.GetBuildIndexByScenePath($"Scenes/{sceneName}")}");
        print(sceneName);
        print(SceneUtility.GetBuildIndexByScenePath($"Scenes/{sceneName}"));


        // 호스트로써 기존 게임에 참여합니다.
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Host, sessionName, GameManager.instance.GetConnectionToken(), NetAddress.Any(), SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath($"1.Scenes/{sceneName}")), null);
    }

    // 게임에 참여하는 메서드
    public void JoinGame(SessionInfo sessionInfo)
    {
        Debug.Log($"Join session {sessionInfo.Name}");

        // 클라이언트로써 기존 게임에 참여합니다.
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Client, sessionInfo.Name, GameManager.instance.GetConnectionToken(), NetAddress.Any(), SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), null);
    }
}