using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NEntity.NScript
{

    public class EntityTriggerEnter : Script
    {
        public Collider m_collider;
        int m_maxCheckTick = 5;
        bool m_isCollided = false;
        bool m_isCompleted = false;

        public override bool init(EntityPlayer entity)
        {
            base.init(entity);
            if (m_collider != null)
            {
                m_collider.gameObject.SetActive(true);
            }
            return true;
        }
        public override void kFixedUpdate(EntityPlayer entity, float timeElapsed)
        {
            base.kFixedUpdate(entity, timeElapsed);
            if (m_isCompleted) return;
            if (m_isCollided)
            {
                m_maxCheckTick--;
                if(m_maxCheckTick <= 0)
                {
                    m_collider.gameObject.SetActive(false);
                    m_isCompleted = true;

                }

            }
        }
        public override bool isCompleted(EntityPlayer entity)
        {
            return m_isCompleted;
        }
        private void OnTriggerEnter(Collider other)
        {
            m_isCollided = true;
        }
    }

}
