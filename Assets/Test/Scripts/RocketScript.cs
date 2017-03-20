using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {
    public Prefab prefab;
    public NEntity.NScript.EntityScriptReader reader;


    public NEntity.NScript.LoadNetworkPrefab loadRocketModel;
    public NEntity.NScript.MoveForward move;
    public NEntity.NScript.LoadNetworkPrefab loadExplosionModel;
    public NEntity.NScript.LoadPrefab loadExplosionPlayer;
    public NEntity.NScript.GameObjectDestroy destroyNetworkObject;

    public void init(
        NetworkPrefabLoader loader,
        GameData.TEAM team,  EntityPlayer entity, Rigidbody rigidBody,
        Vector3 position, Vector3 to)
    {
        PhysicsLayer.SET_ATTACK(prefab, team);

        reader.enabled = true;
        reader.m_entity = entity;
        loadRocketModel.networkloader = loader;
        loadExplosionModel.networkloader = loader;
        loadRocketModel.team = team;
        loadExplosionModel.team = team;
        move.m_rigidBody = rigidBody;
        destroyNetworkObject.obj = entity.gameObject;
        destroyNetworkObject.isNetworkedObject = true;
        if(team == GameData.TEAM.RED)
        {
            loadExplosionPlayer.layer = PHYSICS_LAYER.LAYER_RED_ATTACK;
        }
        else
        {
            loadExplosionPlayer.layer = PHYSICS_LAYER.LAYER_BLUE_ATTACK;

        }
        this.transform.parent = entity.transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        entity.transform.position = position;
        entity.transform.LookAt(to);  

    }
}
