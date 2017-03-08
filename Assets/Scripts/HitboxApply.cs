using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HitboxApply : TriggerEnter {

    public float
        m_damage,
        m_force;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    public override void onObjectEnter(Rigidbody body, Entity entity)
    {
        Debug.Log("qweqweqweqwe");
        base.onObjectEnter(body, entity);
        var dir = (body.transform.position - this.transform.position).normalized;
        Debug.Log(dir);
        body.AddForce(dir*m_force);
        //entity.damage
    }
     * */
}
