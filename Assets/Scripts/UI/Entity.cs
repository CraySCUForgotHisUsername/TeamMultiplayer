using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NUI {

    public class Entity : MonoBehaviour
    {
        [SerializeField]
        TMPro.TextMeshPro m_text;
        [SerializeField]
        GameObject m_healthBar;
        [SerializeField]
        NEntity.Entity m_entity;
        // Use this for initialization
        void Start()
        {

        }
        public void setEntity(NEntity.Entity entity)
        {
            m_entity = entity;
        }
        // Update is called once per frame
        void Update()
        {
            m_healthBar.transform.localScale = new Vector3(m_entity.health/m_entity.healthMax, 1,1);
            if(Camera.main != null)
            {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

            }
        }
    }

}
