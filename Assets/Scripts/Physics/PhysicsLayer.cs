using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsLayer : MonoBehaviour {

    public int
        LAYER_WORLD,
        LAYER_WORLD_DECORATION,
        LAYER_RED_PLAYER,
        LAYER_BLUE_PLAYER,
        LAYER_RED_ATTACK,
        LAYER_BLUE_ATTACK,
        LAYER_RED_SHIELD,
        LAYER_BLUE_SHIELD;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
