using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrenadeScript : MonoBehaviour
{
    public enum GRENADE_STATE { TRAVELLING,BURNING ,DEAD,INACTIVE};
    public Prefab m_prefab;
    public NetworkPrefabLoader m_networkloader;
    public ExplosiveForce m_explosion;
    public TriggerEnter m_triggerEnter;
    public Vector3 m_initialForce;
    public Vector3 
        m_constantForceRelative,
        m_constantForceStatic;
    public float m_fuseTimeRemaining;

    GameData.TEAM m_team;
    Entity m_entity;
    Rigidbody m_rigidbody;
    public GRENADE_STATE m_state = GRENADE_STATE.TRAVELLING;

    bool isTriggered = false;
    //public NEntity.NScript.EntityScriptReader reader;


    //public NEntity.NScript.LoadNetworkPrefab loadRocketModel;
    //public NEntity.NScript.MoveForward move;
    //public NEntity.NScript.LoadNetworkPrefab loadExplosionModel;
    //public NEntity.NScript.LoadPrefab loadExplosionPlayer;
    //public NEntity.NScript.GameObjectDestroy destroyNetworkObject;

    public void init(
        NetworkPrefabLoader loader,
        GameData.TEAM team, Entity entity, Rigidbody krigidBody,
        Vector3 position, Vector3 to)
    {
        PhysicsLayer.SET_ATTACK(m_prefab, team);
        m_team = team;
        m_networkloader = loader;
        m_networkloader.RpcLoadPrefab_ParentMe(PREFAB_ID.GRENADE, team);
        krigidBody.AddForce(entity.transform.rotation* m_initialForce, ForceMode.Impulse);
        //krigidBody.isKinematic = false;
        //m_explosion.gameObject.SetActive(false);
        m_entity = entity;
        m_rigidbody = krigidBody;
        //Debug.Log(m_rigidbody);
        m_networkloader = loader;
        m_triggerEnter.m_isRepeat = true;
        m_triggerEnter.resetCollisionCheck();
        m_triggerEnter.addHdr(onCollision);
        m_prefab.setLayer(PhysicsLayer.GET_LAYER(
            (team == GameData.TEAM.RED) ? PHYSICS_LAYER.LAYER_RED_ATTACK : PHYSICS_LAYER.LAYER_BLUE_ATTACK));
    }
    void onDirectHit(Entity entity)
    {

    }
    void onCollision(Rigidbody body, Entity entity, Vector3 impactPoint)
    {
        if(m_state != GRENADE_STATE.TRAVELLING  && m_state != GRENADE_STATE.BURNING)
        {
            return;
        }
        if (entity != null &&  (entity.m_team == GameData.TEAM.RED || entity.m_team == GameData.TEAM.BLUE))
        {
            onDirectHit(entity);
            m_state = GRENADE_STATE.DEAD;
            return;
        }
        if (body.gameObject.layer == PhysicsLayer.GET_LAYER(PHYSICS_LAYER.LAYER_RED_SHIELD) ||
            body.gameObject.layer == PhysicsLayer.GET_LAYER(PHYSICS_LAYER.LAYER_BLUE_SHIELD)
            )
        {
            m_state = GRENADE_STATE.DEAD;

            return;
        }
        if(m_state == GRENADE_STATE.TRAVELLING)
            m_state = GRENADE_STATE.BURNING;


        m_triggerEnter.enabled = false;

    }
    private void FixedUpdate()
    {
        if (m_rigidbody == null) return;
        m_rigidbody.AddForce(m_constantForceStatic * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    private void Update()
    {
        //Debug.Log(m_state);
        if( m_state == GRENADE_STATE.DEAD)
        {
            m_explosion.gameObject.SetActive(true);

            m_networkloader.RpcLoadPrefab_INDEPENDENT(PREFAB_ID.GREANDE_EXPLOSION, m_team);
            m_explosion.transform.parent = null;
            m_explosion.gameObject.SetActive(true);
            m_state = GRENADE_STATE.INACTIVE;
            return;
            

        }
        if( m_state == GRENADE_STATE.BURNING)
        {
            m_fuseTimeRemaining -= Time.deltaTime;
            if (m_fuseTimeRemaining <= 0) m_state = GRENADE_STATE.DEAD;
            return;
        }
        if(m_state == GRENADE_STATE.INACTIVE)
        {

            NetworkServer.Destroy(m_entity.gameObject);
        }

    }
}
