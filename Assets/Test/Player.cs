using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    
    public PrefabBank PREFAB_BANK;
    public NetworkTransformChild m_netTransformChildHead;
    public GameObject m_mainCamera;

    public Rigidbody m_rigidbody;
    public Avatar m_avatar;
    public Entity m_entity;
    public EntityMotor m_entityMotor;
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
    }
    [Command]
    void CmdChangeTeam(GameData.TEAM team)
    {

        RpcChangeTeam(team);
    }
    [Command]
    void CmdChangeHero(GameData.HERO hero)
    {
        RpcChangeHero(hero);
    }
    [ClientRpc]
    public void RpcChangeTeam(GameData.TEAM team)
    {
    }
    [ClientRpc]
    public void RpcChangeHero(GameData.HERO hero)
    {
        instantiateAvatar(hero);
    }


    void instantiateAvatar(GameData.HERO hero)
    {
        Debug.Log("INSTA");
        Avatar avt = null;
        switch (hero)
        {
            case GameData.HERO.A:
                avt = PREFAB_BANK.AVT_DUELIST;
                break;
            case GameData.HERO.B:
                avt = PREFAB_BANK.AVT_ROCKET;
                break;
            case GameData.HERO.C:
                avt = PREFAB_BANK.AVT_TRICKSTER;
                break;
            case GameData.HERO.D:
                avt = PREFAB_BANK.AVT_HEAVY;
                break;
            case GameData.HERO.E:
                avt = PREFAB_BANK.AVT_SHIELD;
                break;
            case GameData.HERO.F:
                avt = PREFAB_BANK.AVT_MEDIC;
                break;
        }

        if (avt == null) return;
        if(m_avatar != null) GameObject.Destroy(m_avatar.gameObject);
        Avatar me = GameObject.Instantiate<Avatar>(avt);
        me.transform.parent = this.transform;
        me.transform.localPosition = Vector3.zero;
        me.transform.localRotation = Quaternion.identity;
        if(me.m_head == null)
        {
            m_netTransformChildHead.enabled = false;
        }else
        {
            m_netTransformChildHead.enabled = true;
            m_netTransformChildHead.target = me.m_head.transform;
        }
        m_avatar = me;
    }
    private void FixedUpdate()
    {

        if (!isLocalPlayer) return;
        m_entityMotor.kFixedUpdate(this.transform,  m_entity, Time.fixedDeltaTime);
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
        m_playerInput.KUpdate(m_entity, m_entityMotor, m_avatar, Time.deltaTime);
        m_entityMotor.kUpdate(m_entity, Time.deltaTime);


    }
}
