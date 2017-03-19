using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NEntity {

    public class Projectile : MonoBehaviour
    {
        public Entity m_entity;
        public Rigidbody m_body;
        
        // Use this for initialization
        void Start()
        {

        }

        private void FixedUpdate()
        {
            m_body.MovePosition(m_body.transform.position + m_body.transform.forward *m_entity.Speed*Time.fixedDeltaTime);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}
