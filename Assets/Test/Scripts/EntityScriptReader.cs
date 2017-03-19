using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NEntity.NScript
{

    public class EntityScriptReader : MonoBehaviour
    {
        public Entity m_entity;
        public List<Script> m_scripts;

        int m_scriptAt = 0;
        bool isInitiated = false;

        // Use this for initialization
        void Start()
        {

        }

        private void FixedUpdate()
        {
            if(m_scriptAt >= m_scripts.Count)
            {
                this.enabled = false;
                return;
            }
            var kScript = m_scripts[m_scriptAt];
            if (!isInitiated)
            {
                isInitiated = kScript.init(m_entity);
            }
            kScript.kFixedUpdate(m_entity,Time.fixedDeltaTime * m_entity.Time);
            if (kScript.isCompleted(m_entity) && kScript.confirmComplete(m_entity))
            {
                m_scriptAt++;
                isInitiated = false;
            }
            //m_body.MovePosition(m_body.transform.position + m_body.transform.forward * m_entity.Speed * Time.fixedDeltaTime);
        }
        // Update is called once per frame
        void Update()
        {
            if (m_scriptAt >= m_scripts.Count)
            {
                this.enabled = false;
                return;
            }

        }
    }

}
