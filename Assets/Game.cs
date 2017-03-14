using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    static public Game ME;
    public Transform SPAWN_RED, SPAWN_BLUE; 
    private void Awake()
    {
        ME = this;
    }
    public static void SET_PLAYER_POSITION(Transform trnasform, GameData.TEAM team)
    {
        if(team == GameData.TEAM.RED)
        {
            trnasform.position = ME.SPAWN_RED.position;

        }
        else if (team == GameData.TEAM.BLUE)
        {
            trnasform.position = ME.SPAWN_BLUE.position;

        }

    }
    public static void PLAYER_DEAD(Entity entity)
    {
        entity.Health = entity.healthMax;
        entity.m_resourceNow = entity.m_resourceMax;
        SET_PLAYER_POSITION(entity.transform, entity.m_team);

    }
}
