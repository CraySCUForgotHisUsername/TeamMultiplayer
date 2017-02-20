using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using GameData;

public class ClientCommunication : NetworkBehaviour
{

    public static ClientCommunication ME;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void OnStartClient()
    {
        CmdAddMeClient();
    }
    [Command]
    public void CmdDamage(
        NetworkInstanceId attackerID, NetworkInstanceId targetID, float damage)
    {
        var attackerIdentity = ClientScene.FindLocalObject(attackerID).GetComponent<NetworkIdentity>();
        var targetIdentity = ClientScene.FindLocalObject(targetID).GetComponent<NetworkIdentity>();
        var targetHealth = targetIdentity.GetComponent<Health>();
        //Debug.Log(attackerIdentity);
        //Debug.Log(targetIdentity);
        //Debug.Log(targetHealth);
        var targetConnection = targetIdentity.connectionToClient;
        if(targetConnection== null)
        {
            targetHealth.takeDamage(damage);
        }
        else
        {
            targetHealth.TargetTakeDamage(targetIdentity.connectionToClient, damage);

        }
        //Debug.Log(targetIdentity.connectionToClient);
        //if(targetIdentity.player)
        //ask the target if the damage can be processed
    } 
    [Command]
    public void CmdAddMeClient()
    {
        Debug.Log(connectionToClient);
        ServerCommunication.ME.playerAdd(connectionToClient);

    }
    public override void OnStartLocalPlayer()
    {
        gameObject.name = "MeClientCommunication";
        ME = this;
    }
    [Command]
    public void CmdSelectTeam(TEAM team)
    {
        ServerCommunication.ME.playerAssignTeam(connectionToClient, team);

    }
    [Command]
    public void CmdSelectHero(HERO hero)
    {
        ServerCommunication.ME.playerAssignHero(connectionToClient, hero);

    }

}

