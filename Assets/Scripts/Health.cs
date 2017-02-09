using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{

    [SyncVar]
    public int health = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void takeDamage(int amount)
    {
        if (!isServer)
            return;
        health -= amount;
        if(health <= 0)
        {
            health = 0;
            
        }
    }
}
