using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using GameData;

public class ServerCommunication : MonoBehaviour {
    static public ServerCommunication ME;
    public PlayerController PREFAB_PLAYER_CONTROLLER;
    public PlayerMotor HERO_A, HERO_B,HERO_C;

    
    Dictionary<int, PlayerInfo> m_playerInfos = new Dictionary<int, PlayerInfo>();
    // Use this for initialization
    void Awake()
    {
        ME = this;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void onNewPlayer(NetworkConnection connection)
    {
        var info = new PlayerInfo();
        info.connection = connection;
        m_playerInfos.Add(connection.connectionId, info);
        refreshForPlayer(connection);
    }
    void refreshForPlayer(NetworkConnection targetPlayer)
    {
        for (var i = 0; i < m_playerInfos.Count; i++)
        {
            var info = m_playerInfos[i];
            if(info.controller != null && info.motor != null)
            {
                info.controller.TargetLink(targetPlayer, info.motor.netId);

            }
        }

    }
    public void playerAssignTeam(NetworkConnection playerConnection, TEAM team)
    {
        Debug.Log("playerAssignTeam");
        var playerInfo = m_playerInfos[playerConnection.connectionId];//.team = team;// = team;
        playerInfo.team = team;
        //m_playerInfos[playerConnection.connectionId] = playerInfo;
    }
    public void playerAssignHero(NetworkConnection playerConnection, HERO hero)
    {
        var playerInfo = m_playerInfos[playerConnection.connectionId];//.team = team;// = team;
        Debug.Log("playerAssignHero" );
        playerInfo.hero = hero;
        m_playerInfos[playerConnection.connectionId] = playerInfo;
        spawnPlayer(playerConnection);
    }
    void spawnPlayer(NetworkConnection playerConnection)
    {
        Debug.Log("spawnPlayer");
        var playerInfo = m_playerInfos[playerConnection.connectionId];
       
        {
            var controller = GameObject.Instantiate(PREFAB_PLAYER_CONTROLLER.gameObject).GetComponent<PlayerController>();
            playerInfo.controller = controller;
        }
        {
            PlayerMotor motor;
            switch (playerInfo.hero) {
                default:
                case HERO.A:
                    motor = GameObject.Instantiate(HERO_A.gameObject).GetComponent<PlayerMotor>();
                    break;
                case HERO.B:
                    motor = GameObject.Instantiate(HERO_B.gameObject).GetComponent<PlayerMotor>();
                    break;
                case HERO.C:
                    motor = GameObject.Instantiate(HERO_C.gameObject).GetComponent<PlayerMotor>();
                    break;
            }
            playerInfo.motor = motor; 
        }
       // m_playerInfos[playerConnection.connectionId] = playerInfo;
        Debug.Log(playerConnection.playerControllers);
        //NetworkServer.ReplacePlayerForConnection(playerConnection, playerInfo.motor.gameObject, playerConnection.playerControllers[0].playerControllerId);
        //NetworkServer.ReplacePlayerForConnection(playerConnection, playerInfo.controller.gameObject, playerConnection.playerControllers[0].playerControllerId);

        NetworkServer.SpawnWithClientAuthority(playerInfo.motor.gameObject, playerConnection);
        NetworkServer.SpawnWithClientAuthority(playerInfo.controller.gameObject, playerConnection);
        //playerInfo.controller.link(playerInfo.motor);
        playerInfo.controller.RpcLink(playerInfo.motor.netId);

    }
}
