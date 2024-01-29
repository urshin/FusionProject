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
        // Scene���� NetworkRunner�� ã�Ƽ� �̹� �����ϸ� ���� �������� �ʰ� ���� ���� ����մϴ�.
        NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();
        if (networkRunnerInScene != null)
            networkRunner = networkRunnerInScene;
    }

    void Start()
    {
        // ���� networkRunner�� ���� �������� �ʾҴٸ� ���ο� �ν��Ͻ��� �����մϴ�.
        if (networkRunner == null)
        {
            networkRunner = Instantiate(networkRunnerPrefab);
            networkRunner.name = "Network runner";

            // ���� Ȱ��ȭ�� ���� MainMenu�� �ƴ϶�� ��Ʈ��ũ ���ʸ� �ʱ�ȭ�մϴ�.
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

    // NetworkSceneManager�� �������� �޼���
    INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if (sceneManager == null)
        {
            // ���� �̹� �����ϴ� ��Ʈ��ũ ������Ʈ�� ó���մϴ�.
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return sceneManager;
    }

    // �κ� �����ϴ� �޼���
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

    // ������ �����ϴ� �޼���
    public void CreateGame(string sessionName, string sceneName)
    {
        Debug.Log($"Create session {sessionName} scene {sceneName} build Index {SceneUtility.GetBuildIndexByScenePath($"Scenes/{sceneName}")}");
        print(sceneName);
        print(SceneUtility.GetBuildIndexByScenePath($"Scenes/{sceneName}"));


        // ȣ��Ʈ�ν� ���� ���ӿ� �����մϴ�.
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Host, sessionName, GameManager.instance.GetConnectionToken(), NetAddress.Any(), SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath($"1.Scenes/{sceneName}")), null);
    }

    // ���ӿ� �����ϴ� �޼���
    public void JoinGame(SessionInfo sessionInfo)
    {
        Debug.Log($"Join session {sessionInfo.Name}");

        // Ŭ���̾�Ʈ�ν� ���� ���ӿ� �����մϴ�.
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Client, sessionInfo.Name, GameManager.instance.GetConnectionToken(), NetAddress.Any(), SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), null);
    }
}