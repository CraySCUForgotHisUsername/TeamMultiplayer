using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{

    [SyncVar]
    public float health = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool takeDamage(float amount)
    {
        health -= amount;
        if(health < 1)
        {
            health = 0;
            
        }
        return true;
    }
    [TargetRpc]
    public void TargetTakeDamage(NetworkConnection target, float extra)
    {
        Debug.Log("Hello I am told I am taking damage " + this.gameObject.name);
        takeDamage(extra);
    }

}
