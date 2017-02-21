using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    public GameObject m_model;
    public 
        float 
        timeElapsed, 
        timeMax = 1.5f;
	
    // Use this for initialization
	void Start () {
		
	}
	public void init(Vector3 from, Vector3 to)
    {
        m_model.transform.localScale = new Vector3(1,1, (from-to).magnitude );
        m_model.transform.localPosition = new Vector3(0,0, (from - to).magnitude/2);
        this.transform.position = from;
        this.transform.LookAt(to);
    }
	// Update is called once per frame
	void Update () {
        bool isEnd = false;
        timeElapsed += Time.deltaTime;
        isEnd = timeElapsed >= timeMax;
        float ratio = Mathf.Min(timeElapsed / timeMax, 1.0f);

        this.transform.localScale = new Vector3(1 - ratio, 1 - ratio, 1);
        if (isEnd)
        {
            GameObject.Destroy(this.gameObject);
        }
		
	}
}
