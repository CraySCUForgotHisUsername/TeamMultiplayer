using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NEntity.NScript
{

    public class MoveForward : Script
    {
        public Rigidbody m_rigidBody;
        bool m_isCollided = false;
        public override bool init(Entity entity)
        {
            base.init(entity);
            entity.GetComponent<Rigidbody>().AddForce(entity.transform.forward * 10, ForceMode.Impulse);
            m_rigidBody = entity.GetComponent<Rigidbody>();
            return true; 
        }
        public override void kFixedUpdate(Entity entity, float timeElapsed)
        {
            base.kFixedUpdate(entity, timeElapsed);
            //if (m_rigidBody == null) return;
            //m_rigidBody.MovePosition(m_rigidBody.transform.position + m_rigidBody.transform.forward*entity.Speed*timeElapsed);
        }
        public override bool isCompleted(Entity entity)
        {
            return m_isCollided;
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("collider enter " +other.gameObject.name);
            m_rigidBody.velocity = Vector3.zero;
            m_isCollided = true;
        }
    }

}
