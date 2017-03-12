using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PREFAB_ID {
    ROCKET,ROCKET_EXPLOSION
}

public class PrefabBank : MonoBehaviour {
    public static PrefabBank ME;
    public Material MAT_TEAM_RED, MAT_TEAM_BLUE;
    public Prefab
        PRF_ROCKET;
    public GameObject
        PRF_ROCKET_EXPLOSION_RED,
        PRF_ROCKET_EXPLOSION_BLUE;
    public Avatar
        AVT_DUELIST, AVT_ROCKET, AVT_TRICKSTER, AVT_HEAVY, AVT_SHIELD, AVT_MEDIC;
    private void Awake()
    {
        ME = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    public static Transform SPAWN(PREFAB_ID id, GameData.TEAM team )
    {
        switch (id)
        {
            case PREFAB_ID.ROCKET:
                var rocket = Instantiate<Prefab>(ME.PRF_ROCKET);
                if (team == GameData.TEAM.RED)
                    rocket.setMaterial(ME.MAT_TEAM_RED);
                else
                    rocket.setMaterial(ME.MAT_TEAM_BLUE);
                return rocket.transform;
            case PREFAB_ID.ROCKET_EXPLOSION:
                if (team == GameData.TEAM.RED)
                    return Instantiate(ME.PRF_ROCKET_EXPLOSION_RED).transform;
                return Instantiate(ME.PRF_ROCKET_EXPLOSION_BLUE).transform;

        }
        return null;
    }
}
