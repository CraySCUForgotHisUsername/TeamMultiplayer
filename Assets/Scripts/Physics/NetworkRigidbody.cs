using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class NetworkRigidbody : NetworkBehaviour
{
    public Rigidbody m_rigidbody;
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

    }
    [ClientRpc]
    public virtual void RpcAddExplosionForce(float force, Vector3 relativePosition, float radius, float uplift, ForceMode mode)
    {
        m_rigidbody.AddExplosionForce(force, this.transform.position + relativePosition, radius, uplift, mode);

    }
}