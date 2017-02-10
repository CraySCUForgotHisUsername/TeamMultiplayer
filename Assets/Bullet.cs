using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
    public float m_distance  ;
    [SerializeField]
    GameObject PREFAB_TRAIL;

	// Use this for initialization
	void Start () {



    }
    public Vector3 fire()
    {
        float travelDistance = 0;
        var trail = GameObject.Instantiate(PREFAB_TRAIL, transform.position, transform.rotation);
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.transform == null || hit.distance > m_distance)
        {
            travelDistance = m_distance;
            //hit the air
        }
        else
        {
            travelDistance = hit.distance;
            // trail.transform.LookAt(hit.point);

        }
        trail.transform.localScale = new Vector3(1, 1, travelDistance);
        trail.transform.parent = this.transform;
        return this.transform.position + this.transform.forward * travelDistance;
        // trail.transform.position = transform.position;
    }

    [Command]
    public virtual void CmdSpawn()
    {
        var trail = GameObject.Instantiate(PREFAB_TRAIL, transform.position, transform.rotation);
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.transform == null || hit.distance > m_distance)
        {
            trail.transform.localScale = new Vector3(m_distance, 1, 1);
            //hit the air
        }
        else
        {
            trail.transform.localScale = new Vector3(hit.distance, 1, 1);
            // trail.transform.LookAt(hit.point);

        }
        trail.transform.parent = this.transform;
        // trail.transform.position = transform.position;

        
        NetworkServer.Spawn(trail);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
