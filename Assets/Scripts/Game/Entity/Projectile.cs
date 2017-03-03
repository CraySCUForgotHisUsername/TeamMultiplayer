using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NEntity {

    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : Entity
    {
        public GameObject m_colliderProjectile;
        public GameObject m_colliderExplosion;
        public float m_explosionRadius;
        List<Collider> m_colliders = new List<Collider>();
        List<int> m_collidedIds = new List<int>();
        Rigidbody m_body;
        int state = 0;
        
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
        void OnTriggerStay(Collider other)
        {
            //m_colliders.Add(other);

        }
        void OnCollisionEnter(Collision other)
        {
            //m_colliders.Add(other);

        }
        void FixedUpdate()
        {
            if (state == 0)
            {

                if (m_colliders.Count == 0)
                {
                    m_body.MovePosition(transform.position + transform.forward * Speed * Time.fixedDeltaTime);
                    return;
                }
                m_colliders.Clear();
                m_collidedIds.Clear();
                m_colliderProjectile.SetActive(false);
                m_colliderExplosion.SetActive(true);
                state++;
                return;
            }
            else if(state == 1)
            {

                for (int i = 0; i < m_colliders.Count; i++)
                {
                    var collider = m_colliders[i];
                    var body = getRigidbody(collider.transform);
                    if (body == null) continue;
                    //Vector3 distance = (collider.contactOffset - m_colliderExplosion.transform.position);//.normalized;
                    //float ratio = Mathf.Max(0, 1 - distance.magnitude / m_explosionRadius);
                    //Debug.Log(collider.contacts.Length);
                    Debug.Log("bomb");
                    bool isAlreadyChecked = false;
                    int id = body.gameObject.GetInstanceID();
                    for(int j = 0; j < m_collidedIds.Count; j++)
                    {
                        if(m_collidedIds[j] == id)
                        {
                            isAlreadyChecked = true;
                            break;
                        }
                    }
                    if (!isAlreadyChecked)
                    {
                        m_collidedIds.Add(body.gameObject.GetInstanceID());
                        float upward =Mathf.Min(1, Mathf.Max(0.1f,1- (body.transform.position - m_colliderExplosion.transform.position).magnitude/ m_explosionRadius) );
                        upward *= upward;
                        body.AddExplosionForce(m_forceApply, m_colliderExplosion.transform.position, m_explosionRadius,  upward);

                    }
                }
                m_colliders.Clear();
                //Debug.Log("Fixed");
                //GameObject.Destroy(this.gameObject);
                //return;
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
            state++;


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
