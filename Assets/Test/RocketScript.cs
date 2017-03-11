using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {
    public Prefab prefab;
    public NEntity.NScript.EntityScriptReader reader;
    public NEntity.NScript.MoveForward move;
    public void init(
        GameData.TEAM team, Entity entity, Rigidbody rigidBody,
        Vector3 position, Vector3 to)
    {
        PhysicsLayer.SET_ATTACK(prefab, team);

        reader.enabled = true;
        reader.m_entity = entity;
        move.m_rigidBody = rigidBody;

        this.transform.parent = entity.transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        entity.transform.position = position;
        entity.transform.LookAt(to);  

    }
}
