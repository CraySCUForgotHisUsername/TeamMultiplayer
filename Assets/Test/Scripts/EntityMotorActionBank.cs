using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class EntityMotorActionBank : MonoBehaviour
{
    static protected EntityMotorActionBank ME;
    public EntityMotorActions
        AVT_ROCKET,
        AVT_KABOOM,
        MONK,
        AVT_TRICKSTER;
    private void Awake()
    {
        ME = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public static EntityMotorActions GET(GameData.TYPE hero)
    {
        EntityMotorActions actions = null;
        switch (hero)
        {
            case GameData.TYPE.A:
                actions = ME.AVT_ROCKET;
                break;
            case GameData.TYPE.B:
                actions = ME.AVT_KABOOM;
                break;
            case GameData.TYPE.C:
                actions = ME.AVT_TRICKSTER;
                break;
            case GameData.TYPE.MONK:
                actions = ME.MONK;
                break;
        }
        return actions;
    }

}
