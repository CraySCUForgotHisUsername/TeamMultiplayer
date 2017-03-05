using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KNetworkManager : NetworkManager {
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("OnClientConnect 0");
        //GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        //NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //ClientScene.AddPlayer(client.connection, 0);
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
        Debug.Log("OnServerAddPlayer 1");
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(player);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        ClientScene.AddPlayer(client.connection, 0);
        Debug.Log("Player is spawned " + player.gameObject.name);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        Debug.Log("OnServerAddPlayer 2");
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        ClientScene.AddPlayer(client.connection, 0);
    }
}
