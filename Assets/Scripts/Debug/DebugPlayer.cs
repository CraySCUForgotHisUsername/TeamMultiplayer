using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DebugPlayer : NetworkBehaviour{
    [SerializeField]
    Rigidbody body;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) return;
        if (Input.GetKey(KeyCode.W))
        {
            body.MovePosition(body.transform.position + new Vector3(0, 0, 1 * Time.deltaTime));

        }
        if (Input.GetKey(KeyCode.S))
        {
            body.MovePosition(body.transform.position + new Vector3(0, 0, -1 * Time.deltaTime));

        }
    }
}
