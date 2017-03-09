using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour {
    public GameObject 
        m_head, m_body,
        m_weapon;
    public Collider m_headCollider, m_bodyColldier, m_weaponColldier;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Vector3 Look {
        get
        {
            if(m_head != null)
            {
                return m_head.transform.forward;
            }
            return transform.forward;
        }
    }
    public Vector3 Right {
        get
        {
            if (m_body != null)
            {
                return m_body.transform.right;
            }
            return transform.right;
        }
    }


}
