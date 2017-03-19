using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PREFAB_ID {
    ROCKET,  ROCKET_EXPLOSION,
    GRENADE, GREANDE_EXPLOSION,
    MONK_PUNCH,
    MONK_PUNCH_SHIELD
}

public class PrefabBank : MonoBehaviour {
    public static PrefabBank ME;
    public Material MAT_TEAM_RED, MAT_TEAM_BLUE,
        MAT_SHIELD_RED,MAT_SHIELD_BLUE;
    public Prefab
        PRF_ROCKET, PRF_GRENADE, PRF_MONK_PUNCH,
        PRF_SHIELD,PRF_MONK_PUNCH_SHIELD;
    public GameObject
        PRF_ROCKET_EXPLOSION_RED,
        PRF_ROCKET_EXPLOSION_BLUE,
        PRF_GRENADE_EXPLOSION_RED,
        PRF_GRENADE_EXPLOSION_BLUE;
    public Avatar
        AVT_DUELIST, 
        AVT_ROCKET,
        AVT_MONK,
        AVT_KABOOM, 
        AVT_TRICKSTER, AVT_HEAVY, AVT_SHIELD, AVT_MEDIC;
    private void Awake()
    {
        ME = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public static Avatar GET_AVATAR(GameData.TYPE type)
    {
        Avatar avt = null;
        switch (type)
        {
            case GameData.TYPE.A:
                avt = ME.AVT_ROCKET;
                //actions = BANK_ACTION.AVT_ROCKET;
                break;
            case GameData.TYPE.B:
                avt = ME.AVT_KABOOM;
                //actions = BANK_ACTION.AVT_KABOOM;
                break;
            case GameData.TYPE.C:
                avt = ME.AVT_TRICKSTER;
                //actions = BANK_ACTION.AVT_TRICKSTER;
                break;
            case GameData.TYPE.MONK:
                avt = ME.AVT_MONK;
                break;
            case GameData.TYPE.E:
                avt = ME.AVT_SHIELD;
                break;
            case GameData.TYPE.F:
                avt = ME.AVT_MEDIC;
                break;
        }
        return avt;
    }
    static void hprSetTeamColor(Prefab p, GameData.TEAM team)
    {
        if (team == GameData.TEAM.RED)
            p.setMaterial(ME.MAT_TEAM_RED);
        else
            p.setMaterial(ME.MAT_TEAM_BLUE);

    }
    static void hprSetShieldColor(Prefab p, GameData.TEAM team)
    {
        if (team == GameData.TEAM.RED)
            p.setMaterial(ME.MAT_SHIELD_RED);
        else
            p.setMaterial(ME.MAT_SHIELD_BLUE);

    }
    public static Transform SPAWN(PREFAB_ID id, GameData.TEAM team )
    {
        switch (id)
        {
            case PREFAB_ID.MONK_PUNCH_SHIELD:
                var monkPunchShield = Instantiate<Prefab>(ME.PRF_MONK_PUNCH_SHIELD);
                PhysicsLayer.SET_SHIELD(monkPunchShield, team);
                monkPunchShield.gameObject.name = "MONK PUNCH SHIELD";
                hprSetShieldColor(monkPunchShield, team);
                return monkPunchShield.transform;
            case PREFAB_ID.MONK_PUNCH:
                var punch = Instantiate<Prefab>(ME.PRF_MONK_PUNCH);
                hprSetTeamColor(punch, team);
                return punch.transform;
            case PREFAB_ID.ROCKET:
                var rocket = Instantiate<Prefab>(ME.PRF_ROCKET);
                hprSetTeamColor(rocket, team);
                return rocket.transform;
            case PREFAB_ID.GRENADE:
                var grenade = Instantiate<Prefab>(ME.PRF_GRENADE);
                if (team == GameData.TEAM.RED)
                    grenade.setMaterial(ME.MAT_TEAM_RED);
                else
                    grenade.setMaterial(ME.MAT_TEAM_BLUE);
                return grenade.transform;
            case PREFAB_ID.ROCKET_EXPLOSION:
                if (team == GameData.TEAM.RED)
                    return Instantiate(ME.PRF_ROCKET_EXPLOSION_RED).transform;
                return Instantiate(ME.PRF_ROCKET_EXPLOSION_BLUE).transform;
            case PREFAB_ID.GREANDE_EXPLOSION:
                if (team == GameData.TEAM.RED)
                    return Instantiate(ME.PRF_GRENADE_EXPLOSION_RED).transform;
                return Instantiate(ME.PRF_GRENADE_EXPLOSION_BLUE).transform;

        }
        return null;
    }
}
