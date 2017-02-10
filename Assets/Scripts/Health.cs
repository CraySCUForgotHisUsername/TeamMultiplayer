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
    public void takeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            health = 0;
            
        }
    }
}
