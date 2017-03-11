using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab : MonoBehaviour
{
    public List<Collider> m_colldiers;
    public List<MeshRenderer> m_meshRenderes;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setLayer(int layer)
    {
        for (int i = 0; i < m_colldiers.Count; i++)
        {
            m_colldiers[i].gameObject.layer = layer;
        }
    }
    public void setMaterial(Material mat)
    {
        for(int i = 0; i < m_meshRenderes.Count; i++)
        {
            m_meshRenderes[i].material = mat;
        }
    }
}
