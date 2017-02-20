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
    public void CmdTest()
    {

    }
    //Simple bullet effect. Request les
    [Command]
    public void CmdEffect_00(int effectNumber)
    {

    }
   
    [Command]
    public void CmdDamage(
        NetworkInstanceId attackerID, NetworkInstanceId targetID, Vector3 originalImpactPoint, float damageInstant, float damageDelayed)
    {
        var attackerIdentity = ClientScene.FindLocalObject(attackerID).GetComponent<NetworkIdentity>();
        var targetIdentity = ClientScene.FindLocalObject(targetID).GetComponent<NetworkIdentity>();
        var targetHealth = targetIdentity.GetComponent<Health>();
        //Debug.Log(attackerIdentity);
        //Debug.Log(targetIdentity);
        //Debug.Log(targetHealth);
        var targetConnection = targetIdentity.connectionToClient;
        targetHealth.takeDamageRaw(damageInstant);
        if (targetConnection== null)
        {
            Debug.Log("Object not owned by anyone");
            targetHealth.takeDamage(damageDelayed);
        }
        else
        {
            Debug.Log("Asking if the target will be damaged");
            targetHealth.TargetTakeDamage(targetIdentity.connectionToClient, originalImpactPoint, damageDelayed);

        }
        //Debug.Log(targetIdentity.connectionToClient);
        //if(targetIdentity.player)
        //ask the target if the damage can be processed
    } 
    [Command]
    public void CmdAddMeClient()
    {
        Debug.Log(connectionToClient);
        ServerCommunication.ME.onNewPlayer(connectionToClient);

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

