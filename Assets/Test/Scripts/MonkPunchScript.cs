using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonkPunchScript : MonoBehaviour
{
    public enum PUNCH_STATE { TRAVELLING, EXPLODING,DEAD };
    public Prefab m_prefab;
    public NetworkPrefabLoader m_networkloader;
    public Prefab m_shield;
    public ExplosiveForce m_explosion;
    public TriggerEnter m_triggerEnter;

    public float m_shieldDeployInterval;
    public Vector3 m_initialForce;
    PUNCH_STATE m_state = PUNCH_STATE.TRAVELLING;
    //public Vector3
    //    m_constantForceRelative,
    //    m_constantForceStatic;
    //public float m_fuseTimeRemaining;

    GameData.TEAM m_team;
    Entity m_entity;
    Rigidbody m_rigidbody;
    //public GRENADE_STATE m_state = GRENADE_STATE.TRAVELLING;

    bool isTriggered = false;
    Vector3 m_lastPosition = new Vector3();
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
        m_prefab.setLayer(PhysicsLayer.GET_LAYER(
            (team == GameData.TEAM.RED) ? PHYSICS_LAYER.LAYER_RED_ATTACK : PHYSICS_LAYER.LAYER_BLUE_ATTACK));
        //PhysicsLayer.SET_ATTACK(m_prefab, team);
        m_team = team;
        m_networkloader = loader;
        m_networkloader.RpcLoadPrefab_ParentMe(PREFAB_ID.MONK_PUNCH, team);
        krigidBody.AddForce(entity.transform.rotation * m_initialForce, ForceMode.Impulse);
        //krigidBody.isKinematic = false;
        //m_explosion.gameObject.SetActive(false);
        m_entity = entity;
        m_rigidbody = krigidBody;
        //Debug.Log(m_rigidbody);
        m_networkloader = loader;
        m_triggerEnter.m_isRepeat = true;
        m_triggerEnter.addHdr(onCollision);
        m_lastPosition = entity.transform.position;
    }
    void onDirectHit(Entity entity)
    {

    }
    void onCollision(Rigidbody body, Entity entity, Vector3 impactPoint)
    {
        if (entity != null && (entity.m_team == GameData.TEAM.RED || entity.m_team == GameData.TEAM.BLUE))
        {
            onDirectHit(entity);
        }
        m_state = PUNCH_STATE.EXPLODING;

        m_rigidbody.velocity = Vector3.zero;
        m_triggerEnter.enabled = false;

    }
    private void FixedUpdate()
    {
        if (m_rigidbody == null) return;
        //m_rigidbody.AddForce(m_constantForceStatic * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    void hprDeployShieldAt(Vector3 position, Vector3 dirLook)
    {
        var shield = Instantiate(m_shield);
        shield.transform.position = position;
        shield.transform.LookAt(position + dirLook);
        PhysicsLayer.SET_SHIELD(shield, m_team);
    }
    private void Update()
    {
        if( m_state == PUNCH_STATE.EXPLODING)
        {
            m_state = PUNCH_STATE.DEAD;
            return;
        }
        if(m_state == PUNCH_STATE.DEAD)
        {
            NetworkServer.Destroy(m_entity.gameObject);
        }
        if( (m_entity.transform.position - m_lastPosition).magnitude > m_shieldDeployInterval ){
            var dir =( m_entity.transform.position - m_lastPosition ).normalized;
            var newPos = m_lastPosition + dir * m_shieldDeployInterval;
            hprDeployShieldAt(newPos, dir);
            m_networkloader.RpcLoadPrefab_Separate(PREFAB_ID.MONK_PUNCH_SHIELD, m_team, newPos, dir);
            m_lastPosition = newPos;
        }
        // NetworkServer.Destroy(m_entity.gameObject);

    }
}
