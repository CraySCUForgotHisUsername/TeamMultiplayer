using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    static float DAMAGE_AVOID_DISTANCE = 1.0f;

    bool isTakingDamage = true;
    [SyncVar]
    public float health = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void takeDamageRaw(float amount)
    {
        //Debug.Log("Raw damage " + amount);
        health -= amount;
        if (health < 1)
        {
            health = 0;

        }
    }
    public bool IsTakeDamage
    {
        get {
         return isTakingDamage;
        }
    }
    public float howMuchDamageWillBeTaken(float amount)
    {
        return amount;
    }
    public bool takeDamage(float amount)
    {
        Debug.Log("takeDamage damage " + amount);
        if (!isTakingDamage) return false;
        health -= amount;
        if(health < 1)
        {
            health = 0;
            
        }
        return true;
    }
    [Command]
    void CmdClinetUpdatingHealth(float h)
    {
        this.health = h;
    }
    [TargetRpc]
    public void TargetTakeDamage(NetworkConnection target, Vector3 impactPoint, float damage)
    {
        Debug.Log("Hello I am told I am taking damage " + this.gameObject.name);
        if((this.transform.position - impactPoint).magnitude > DAMAGE_AVOID_DISTANCE)
        {
            //do nothing;
        }
        takeDamage(damage);
    }

}
