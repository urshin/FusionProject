using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPlayer networkPlayer;


    //플레이어 정보를 가지는 딕셔너리
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    //플레이어 캐릭터
    [SerializeField] private NetworkPrefabRef swordMan;
    [SerializeField] private NetworkPrefabRef magician;
    [SerializeField] private NetworkPrefabRef archer;

    //메인메뉴 세션
    SessionListUIHandler sessionListUIHandler;



    //ingame
    InGameUIHandler inGameUIHandler;

    // Mapping between Token ID and Re-created Players
    Dictionary<int, NetworkPlayer> mapTokenIDWithNetworkPlayer;

    void Awake()
    {
        //Create a new Dictionary
        mapTokenIDWithNetworkPlayer = new Dictionary<int, NetworkPlayer>();

        sessionListUIHandler = FindObjectOfType<SessionListUIHandler>(true);
        
    }
    int GetPlayerToken(NetworkRunner runner, PlayerRef player)
    {
        if (runner.LocalPlayer == player)
        {
            // Just use the local Player Connection Token
            return ConnectionTokenUtils.HashToken(GameManager.instance.GetConnectionToken());
        }
        else
        {
            // Get the Connection Token stored when the Client connects to this Host
            var token = runner.GetPlayerConnectionToken(player);

            if (token != null)
                return ConnectionTokenUtils.HashToken(token);

            Debug.LogError($"GetPlayerToken returned invalid token");

            return 0; // invalid
        }
    }
    IEnumerator CallSpawnedCO()
    {
        yield return new WaitForSeconds(0.5f);
    }


    public void OnConnectedToServer(NetworkRunner runner)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
          print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name  );
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (runner.IsServer)
        {
            //Get the token for the player
            int playerToken = GetPlayerToken(runner, player);

            Debug.Log($"OnPlayerJoined we are server. Connection token {playerToken}");

            //Check if the token is already recorded by the server. 
            if (mapTokenIDWithNetworkPlayer.TryGetValue(playerToken, out NetworkPlayer networkPlayer))
            {
                Debug.Log($"Found old connection token for token {playerToken}. Assigning controlls to that player");

                networkPlayer.GetComponent<NetworkObject>().AssignInputAuthority(player);

                networkPlayer.Spawned();
            }
            else
            {
                Debug.Log($"Spawning new player for connection token {playerToken}");
                NetworkObject networkPlayerObject = runner.Spawn(swordMan, new Vector3(0, 0, 0), Quaternion.identity, player) ;
            }
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
       
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }



    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (sessionListUIHandler == null)
            return;

        if (sessionList.Count == 0)
        {
            Debug.Log("Joined lobby no sessions found");

            sessionListUIHandler.OnNoSessionsFound();
        }
        else
        {
            sessionListUIHandler.ClearList();

            foreach (SessionInfo sessionInfo in sessionList)
            {
                sessionListUIHandler.AddToList(sessionInfo);

                Debug.Log($"Found session {sessionInfo.Name} playerCount {sessionInfo.PlayerCount}");
            }
        }




    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

   
}
