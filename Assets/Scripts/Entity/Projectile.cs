using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NEntity {

    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : Entity
    {
        public Rigidbody m_body;

        List<Collider> m_colliders = new List<Collider>();
        [SerializeField]
        float m_speed = 10.0f;
        [SerializeField]
        float m_forceApply = 10.0f;
        // Use this for initialization
        void Awake()
        {
            m_body = this.GetComponent<Rigidbody>();
        }
        void Start()
        {

        }
        void OnTriggerEnter(Collider other)
        {
            m_colliders.Add(other);

        }
        void FixedUpdate()
        {
            if (m_colliders.Count == 0)
                m_body.MovePosition( transform.position +transform.forward* m_speed * getModSpeed()  * Time.fixedDeltaTime);
            else
            {
                for(int i = 0; i< m_colliders.Count; i++)
                {
                    var collider =  m_colliders[i];
                    var body = getRigidbody(collider.transform);
                    if (body == null) continue;
                    Vector3 dir = (body.transform.position - transform.position).normalized;
                    
                    body.AddForce(dir*m_forceApply);

                }
                GameObject.Destroy(this.gameObject);
            }
            //getModSpeed() * m_force );
        }
        Rigidbody getRigidbody(Transform transform)
        {
            var rigidbody = transform.GetComponent<Rigidbody>();
            if(rigidbody == null)
            {
                if (transform.parent == null)
                    return null;
                return getRigidbody(transform.parent);
            }
            return rigidbody;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}
