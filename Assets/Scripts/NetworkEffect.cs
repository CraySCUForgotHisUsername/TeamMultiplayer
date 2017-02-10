using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEffect : NetworkBehaviour {
    public static NetworkEffect INSTANCE;
    [SerializeField]
    NetworkIdentity PREFAB_TRAIL;
    // Use this for initialization
    private void Awake()
    {
        //NetworkServer.connections[0].clientOwnedObjects
        var identity = this.GetComponent<NetworkIdentity>();
        Debug.Log("IAMWEQWKE");
        INSTANCE = this;
        
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CmdBulletTrail(Vector3 position, Vector3 pointOfImpact)
    {
        if (!isServer) return;
        var trail = GameObject.Instantiate(
            PREFAB_TRAIL.gameObject,
            position, Quaternion.identity);
        trail.transform.LookAt(pointOfImpact);
        trail.transform.localScale = new Vector3(1, 1, (pointOfImpact - position).magnitude);
        //CmdEEE();
        //var bullet = fire(playerController.m_motor.m_face.position, playerController.m_motor.m_face.forward);

        NetworkServer.Spawn(trail);
         
    }
}
