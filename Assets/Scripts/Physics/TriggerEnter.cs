using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class TriggerEnter : MonoBehaviour
{
    public delegate void DEL_ENTER(Rigidbody body, Entity entity, Vector3 impactPoint);
    public bool m_isRepeat = false;
    List<DEL_ENTER> m_enters = new List<DEL_ENTER>();
    List<Collider> m_collidedColliders = new List<Collider>();
    List<int> m_collidedIds = new List<int>();
    [SerializeField]
    int m_checkTickInit;
    int m_checkTick;
    public void resetCollisionCheck()
{
    m_collidedIds.Clear();

}
    public void addHdr(DEL_ENTER hdr)
    {
        m_enters.Add(hdr);
    }
    public void recycle()
    {
        //m_enters.Clear();
        m_checkTick = m_checkTickInit;

    }
    // Use this for initialization
    void Awake()
    {
        m_checkTick = m_checkTickInit;
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
        for (int i = m_collidedColliders.Count-1; i >=0; i--)
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
            //Debug.Log("TRIGGERE ENTERED !!! " +m_enters.Count + " , " + isAlreadyChecked);
            if (!isAlreadyChecked)
            {
                //int shieldLayerTest = -1;
                RaycastHit hit;
                Physics.Raycast(new Ray(this.transform.position, (collider.transform.position - this.transform.position).normalized), out hit, collider.gameObject.layer);
                if (hit.transform != null && body == getRigidbody(hit.transform))
                {
                    m_collidedColliders.RemoveAt(i);
                    m_collidedIds.Add(body.gameObject.GetInstanceID());
                    for (int iEvent = 0; iEvent < m_enters.Count; iEvent++)
                    {
                        m_enters[iEvent](body, body.GetComponent<Entity>(), hit.point); ;
                    }
                    onObjectEnter(body, body.GetComponent<Entity>(), hit.point);

                }
                else
                {

                    //m_collidedIds.Remove(id);
                }
                //float upward = Mathf.Min(1, Mathf.Max(0.1f, 1 - (body.transform.position - m_colliderExplosion.transform.position).magnitude / m_explosionRadius));
                //upward *= upward;
                //body.AddExplosionForce(m_forceApply, m_colliderExplosion.transform.position, m_explosionRadius, upward);

            }
            //m_collidedColliders.RemoveAt(i);
        }
        //m_collidedColliders.Clear();
        if (m_checkTick <= 0)
        {
            //Debug.Log("CHECK IS NOW LOOKING FOR RIGIDBODY !!! " + m_collidedColliders.Count);
            
                if (m_isRepeat) {
                    recycle();
                }
              else
                {
                    GameObject.Destroy(this.gameObject);

                }
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

    public virtual void onObjectEnter(Rigidbody body, Entity entity,Vector3 impactPoint)
    {
}
    // Update is called once per frame
    void Update()
    {

    }
}
