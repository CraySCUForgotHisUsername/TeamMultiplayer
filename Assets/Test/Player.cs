using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    
    public PrefabBank PREFAB_BANK;
    public GameObject m_mainCamera;

    public Rigidbody m_rigidbody;
    public NetworkTransformChild m_netTransformChildHead;
    public Avatar m_avatar;
    public PlayerMovement m_playerMovement;
    
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        m_mainCamera.SetActive(true);
        m_playerMovement.enabled = true;
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
        }

        if (avt == null) return;
        if(m_avatar != null)GameObject.Destroy(m_avatar.gameObject);
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
        m_playerMovement.KUpdate(m_rigidbody, m_avatar, Time.deltaTime);


    }
}
