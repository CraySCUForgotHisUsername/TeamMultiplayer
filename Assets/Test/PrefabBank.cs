using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PREFAB_ID {
    ROCKET
}

public class PrefabBank : MonoBehaviour {
    public static PrefabBank ME;
    public Material MAT_TEAM_RED, MAT_TEAM_BLUE;
    public Prefab
        PRF_ROCKET;
    public Avatar
        AVT_DUELIST, AVT_ROCKET, AVT_TRICKSTER, AVT_HEAVY, AVT_SHIELD, AVT_MEDIC;
    private void Awake()
    {
        ME = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public static Transform SPAWN(PREFAB_ID id, GameData.TEAM team )
    {
        Prefab p = null;
        switch (id) {
            case PREFAB_ID.ROCKET:
                p = Instantiate<Prefab>(ME.PRF_ROCKET);
                break;
        }
        if (p == null) return null;
        if (team == GameData.TEAM.WORLD) return p.transform;
        if (team == GameData.TEAM.RED)
            p.setMaterial(ME.MAT_TEAM_RED);
        else
            p.setMaterial(ME.MAT_TEAM_BLUE);

        return p.transform;
        

    }
}
