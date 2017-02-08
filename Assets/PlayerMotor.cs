using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    Camera cam;
    private Vector3
        m_velocity = Vector3.zero,
        m_rotation = Vector3.zero,
        m_rotationCamera = Vector3.zero;

    Rigidbody m_rigidbody;
	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();

	}
    void FixedUpdate()
    {
        updateMovement();
    }
    //Run during fixedupdate
    void updateMovement()
    {
        if (m_velocity != Vector3.zero)
            m_rigidbody.MovePosition(m_rigidbody.position + m_velocity * Time.fixedDeltaTime);
        if (m_rotation != Vector3.zero)
            m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(m_rotation * Time.fixedDeltaTime));
        if (m_rotationCamera != Vector3.zero)
            cam.transform.Rotate(-m_rotationCamera * Time.fixedDeltaTime);
    }
    public void move(Vector3 velocity)
    {
        m_velocity = velocity;
    }
    public void rotate(Vector3 velocity)
    {
        m_rotation = velocity;
    }
    public void rotateCamera(Vector3 velocity)
    {
        m_rotationCamera = velocity;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
