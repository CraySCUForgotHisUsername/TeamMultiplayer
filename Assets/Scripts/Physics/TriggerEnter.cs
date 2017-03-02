using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [RequireComponent(typeof(Rigidbody))]
    public class TriggerEnter : MonoBehaviour
    {
        public List<Collider> m_colliders;
        List<Collider> m_collidedColliders = new List<Collider>();
        List<int> m_collidedIds = new List<int>();
        [SerializeField]
        int m_checkTick;
        
        // Use this for initialization
        void Awake()
        {
        }
        void Start()
        {

        }
        void OnTriggerEnter(Collider other)
        {
            m_collidedColliders.Add(other);

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
            if (m_checkTick <= 0)
            {
                for (int i = 0; i < m_collidedColliders.Count; i++)
                {
                    var collider = m_collidedColliders[i];
                if (collider == null) continue;
                    var body = getRigidbody(collider.transform);
                    if (body == null) continue;
                    //Vector3 distance = (collider.contactOffset - m_colliderExplosion.transform.position);//.normalized;
                    //float ratio = Mathf.Max(0, 1 - distance.magnitude / m_explosionRadius);
                    //Debug.Log(collider.contacts.Length);
                    //Debug.Log("bomb");
                    bool isAlreadyChecked = false;
                    int id = body.gameObject.GetInstanceID();
                    for (int j = 0; j < m_collidedIds.Count; j++)
                    {
                        if (m_collidedIds[j] == id)
                        {
                            isAlreadyChecked = true;
                            break;
                        }
                    }
                    if (!isAlreadyChecked)
                    {
                        m_collidedIds.Add(body.gameObject.GetInstanceID());
                        onObjectEnter(body, body.GetComponent<NEntity.Entity>());
                        //float upward = Mathf.Min(1, Mathf.Max(0.1f, 1 - (body.transform.position - m_colliderExplosion.transform.position).magnitude / m_explosionRadius));
                        //upward *= upward;
                        //body.AddExplosionForce(m_forceApply, m_colliderExplosion.transform.position, m_explosionRadius, upward);

                    }
                }
                GameObject.Destroy(this.gameObject);
                m_colliders.Clear();
            }
            m_checkTick--;


            //getModSpeed() * m_force );
        }
        Rigidbody getRigidbody(Transform transform)
        {
            var rigidbody = transform.GetComponent<Rigidbody>();
            if (rigidbody == null)
            {
                if (transform.parent == null)
                    return null;
                return getRigidbody(transform.parent);
            }
            return rigidbody;
        }

        public virtual void onObjectEnter(Rigidbody body, NEntity.Entity entity)
        {

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
