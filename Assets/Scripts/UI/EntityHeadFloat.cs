using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NUI {

    public class EntityHeadFloat : MonoBehaviour
    {
        [SerializeField]
        TMPro.TextMeshPro m_text;
        [SerializeField]
        GameObject
            m_healthBar,
            m_healthBarMeter;
            //m_healthBarBackground;
        [SerializeField]
        Entity m_entity;
        // Use this for initialization
        void Start()
        {

        }
        public void setEntity(Entity entity)
        {
            //m_entity = entity;
        }
        // Update is called once per frame
        void Update()
        {
            m_healthBarMeter.transform.localScale = new Vector3(m_entity.health/m_entity.healthMax, 1,1);
            if(Camera.main != null)
            {
                m_healthBar.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
                //m_healthBarBackground.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
                m_text.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

            }
        }
    }

}
