using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public partial class Player : NetworkBehaviour {
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
    public void CmdChangeTeam(GameData.TEAM team)
    {

        RpcChangeTeam(team);
    }
    [Command]
    public void CmdChangeHero(GameData.TYPE hero)
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
        Game.SET_PLAYER_POSITION(m_entity.transform, team);
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
    [ClientRpc]
    public void RpchdrDead()
    {
        //play death animation
        if (isLocalPlayer)
        {

            Game.PLAYER_DEAD(m_entity);
            m_entity.health = m_entity.healthMax;
            m_entity.CmdClientSetHealth(m_entity.healthMax);

        }else
        {
            //m_entity.health = 0;

        }
    }
    [ClientRpc]
    public void RpcAddExplosionForce(float force, Vector3 relativePosition, float radius, float uplift, ForceMode mode)
    {
        m_entityMotor.IsGrounded = false;
        m_entityMotor.Rigidbody.AddExplosionForce(force, this.transform.position + relativePosition, radius, uplift, mode);

    }
    [TargetRpc]
    public void TargetInstantiateAvatar(NetworkConnection target, GameData.TEAM team, GameData.TYPE hero)
    {
        this.m_entity.m_team = team;
        this.m_entity.m_type = hero;
        instantiateAvatar(team, hero);
    }

    void instantiateAvatar(GameData.TEAM team, GameData.TYPE hero)
    {
        Debug.Log("INSTA " + team + " , "  + hero);
        Avatar avt = null;
        EntityMotorActions actions = null;
        avt     = PrefabBank.GET_AVATAR(hero);
        actions = EntityMotorActionBank.GET(hero);
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
        //Debug.Log(team);
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
        if (!isLocalPlayer)
        {
            m_entityMotor.kFixedNonLocalUpdate(this.transform, m_entity, m_avatar, Time.fixedDeltaTime);
            return;
        }
        
        m_entityMotor.kFixedUpdate(this.transform,  m_entity,m_avatar, Time.fixedDeltaTime);
    }
    void Update()
    {
        if (isServer)
        {

            if (m_entity.health <= 0)
            {
                RpchdrDead();
                //Game.PLAYER_DEAD(m_entity);
            }
        }
        if (!isLocalPlayer) return;
      

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
