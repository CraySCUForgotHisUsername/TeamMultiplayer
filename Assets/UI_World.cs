using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_World : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main == null) return;
        transform.LookAt(Camera.main.transform);
	}
}
