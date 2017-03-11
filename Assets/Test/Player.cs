using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public static Player LOCAL_PLAYER;
    public PrefabBank               BANK_PREFAB;
    public EntityMotorActionBank    BANK_ACTION;
    public NetworkTransformChild m_netTransformChildHead;
    public GameObject m_mainCamera;

    public Rigidbody m_rigidbody;
    public Avatar m_avatar;
    public Entity m_entity;
    public EntityMotor          m_entityMotor;
    public EntityMotorActions   m_entityMotorActions;
    public PlayerInput m_playerInput;
    
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        m_mainCamera.SetActive(true);
        m_playerInput.enabled = true;
        Player.LOCAL_PLAYER = this;
        Entity.LOCAL_PLAYER_ENTITY = m_entity;
    }
    [Command]
    void CmdChangeTeam(GameData.TEAM team)
    {

        RpcChangeTeam(team);
    }
    [Command]
    void CmdChangeHero(GameData.TYPE hero)
    {
        RpcChangeHero(hero);
    }
    [Command]
    public void CmdFireRocket(Vector3 position, Vector3 to)
    {
        PlayerInputManager.ME.ROCKET(this.m_entity.m_team, position, to);
    }
    [ClientRpc]
    public void RpcChangeTeam(GameData.TEAM team)
    {
        Debug.Log("TEAM CHANGED TIO " + team);
        if (m_entity == null) return;
        m_entity.m_team = team;

    }
    [ClientRpc]
    public void RpcChangeHero(GameData.TYPE hero)
    {
        instantiateAvatar(m_entity.m_team, hero);
        if (m_entity == null) return;
        m_entity.m_type = hero;
        PhysicsLayer.SET_PLAYER(this.m_avatar,this.m_entity.m_team);
    }


    void instantiateAvatar(GameData.TEAM team, GameData.TYPE hero)
    {
        Debug.Log("INSTA " + team + " , "  + hero);
        Avatar avt = null;
        EntityMotorActions actions = null;
        switch (hero)
        {
            case GameData.TYPE.A:
                avt = BANK_PREFAB.AVT_DUELIST;
                break;
            case GameData.TYPE.B:
                avt = BANK_PREFAB.AVT_ROCKET;
                actions = BANK_ACTION.AVT_ROCKET;
                break;
            case GameData.TYPE.C:
                avt = BANK_PREFAB.AVT_TRICKSTER;
                actions = BANK_ACTION.AVT_TRICKSTER;
                break;
            case GameData.TYPE.D:
                avt = BANK_PREFAB.AVT_HEAVY;
                break;
            case GameData.TYPE.E:
                avt = BANK_PREFAB.AVT_SHIELD;
                break;
            case GameData.TYPE.F:
                avt = BANK_PREFAB.AVT_MEDIC;
                break;
        }
        if (avt == null) return;

        m_entityMotor.resetState();
        if(m_avatar != null) GameObject.Destroy(m_avatar.gameObject);
        if(m_entityMotorActions != null)
        {
            m_entityMotorActions.unLink(this.m_entityMotor);
            GameObject.Destroy(m_entityMotorActions.gameObject);
            m_entityMotorActions = null;
        }
        Avatar me = GameObject.Instantiate<Avatar>(avt);
        me.transform.parent = this.transform;
        me.transform.localPosition = Vector3.zero;
        me.transform.localRotation = Quaternion.identity;
        Debug.Log(team);
        if(team == GameData.TEAM.RED)
        {
            me.setMaterial(BANK_PREFAB.MAT_TEAM_RED);

        }
        else if(team == GameData.TEAM.BLUE)
        {
            me.setMaterial(BANK_PREFAB.MAT_TEAM_BLUE);

        }

        if(me.m_head == null)
        {
            m_netTransformChildHead.enabled = false;
        }else
        {
            m_netTransformChildHead.enabled = true;
            m_netTransformChildHead.target = me.m_head.transform;
        }
        m_avatar = me;
        if(actions != null)
        {
            m_entityMotorActions = GameObject.Instantiate<EntityMotorActions>(actions);
            m_entityMotorActions.link(m_entityMotor);
        }
    }
    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        
        m_entityMotor.kFixedUpdate(this.transform,  m_entity,m_avatar, Time.fixedDeltaTime);
    }
    void Update()
    {

        if (!isLocalPlayer) return;
        if (UIManager.IS_NEW_INPUT)
        {
            UIManager.IS_NEW_INPUT = false;
            CmdChangeTeam(UIManager.TEAM_SELECTED);
            CmdChangeHero(UIManager.HERO_SELECTED);
        }

        if(m_avatar != null && m_avatar.m_head != null)
        {
            m_mainCamera.transform.position = m_avatar.m_head.transform.position;
            m_mainCamera.transform.rotation = m_avatar.m_head.transform.rotation;
        }
        m_entity.kUpdate(Time.deltaTime);
        m_playerInput.KUpdate(m_entity, m_entityMotor, m_avatar, Time.deltaTime);
        m_entityMotor.kUpdate(m_entity, m_avatar, Time.deltaTime);


    }
}
