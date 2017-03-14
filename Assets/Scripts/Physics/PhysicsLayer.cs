using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PHYSICS_LAYER
{
    LAYER_WORLD,
    LAYER_WORLD_DECORATION,
    LAYER_RED_PLAYER,
    LAYER_BLUE_PLAYER,
    LAYER_RED_ATTACK,
    LAYER_BLUE_ATTACK,
    LAYER_RED_SHIELD,
    LAYER_BLUE_SHIELD
};
public class PhysicsLayer : MonoBehaviour {
    

    public static PhysicsLayer ME;
    public int
        LAYER_WORLD,
        LAYER_WORLD_DECORATION,
        LAYER_RED_PLAYER,
        LAYER_BLUE_PLAYER,
        LAYER_RED_ATTACK,
        LAYER_BLUE_ATTACK,
        LAYER_RED_SHIELD,
        LAYER_BLUE_SHIELD;
    public static int GET_LAYER(PHYSICS_LAYER layer)
    {
        switch (layer) {
            default:
            case PHYSICS_LAYER.LAYER_WORLD:
                return ME.LAYER_WORLD;
            case PHYSICS_LAYER.LAYER_WORLD_DECORATION:
                return ME.LAYER_WORLD_DECORATION;
            case PHYSICS_LAYER.LAYER_RED_PLAYER:
                return ME.LAYER_RED_PLAYER;
            case PHYSICS_LAYER.LAYER_BLUE_PLAYER:
                return ME.LAYER_BLUE_PLAYER;
            case PHYSICS_LAYER.LAYER_RED_ATTACK:
                return ME.LAYER_RED_ATTACK;
            case PHYSICS_LAYER.LAYER_BLUE_ATTACK:
                return ME.LAYER_BLUE_ATTACK;
            case PHYSICS_LAYER.LAYER_RED_SHIELD:
                return ME.LAYER_RED_SHIELD;
            case PHYSICS_LAYER.LAYER_BLUE_SHIELD:
                return ME.LAYER_BLUE_SHIELD;
        }


    }
    private void Awake()
    {
        ME = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void SET_WORLD(Prefab prefab,bool isDecoration = false)
    {
        int layer = (isDecoration) ? ME.LAYER_WORLD_DECORATION : ME.LAYER_WORLD;
        for (int i = 0; i < prefab.m_colldiers.Count; i++)
        {
            prefab.m_colldiers[i].gameObject.layer = layer;
        }

    }
    public static void SET_PLAYER(Prefab prefab, GameData.TEAM team)
    {
        int layer = (team == GameData.TEAM.RED) ? ME.LAYER_RED_PLAYER : ME.LAYER_BLUE_PLAYER;
        for (int i = 0; i < prefab.m_colldiers.Count; i++)
        {
            prefab.m_colldiers[i].gameObject.layer = layer;
        }

    }
    public static void SET_ATTACK(Prefab prefab, GameData.TEAM team)
    {
        int layer = (team == GameData.TEAM.RED) ? ME.LAYER_RED_ATTACK : ME.LAYER_BLUE_ATTACK;
        for (int i = 0; i < prefab.m_colldiers.Count; i++)
        {
            prefab.m_colldiers[i].gameObject.layer = layer;
        }

    }
    public static void SET_SHIELD(Prefab prefab, GameData.TEAM team)
    {
        int layer = (team == GameData.TEAM.RED) ? ME.LAYER_RED_SHIELD : ME.LAYER_BLUE_SHIELD;
        for (int i = 0; i < prefab.m_colldiers.Count; i++)
        {
            prefab.m_colldiers[i].gameObject.layer = layer;
        }

    }
}
