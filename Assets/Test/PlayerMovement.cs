using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using System.Collections.Generic;



public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    Transform m_head;
    [SerializeField]
    Rigidbody m_rigidbody;
    [SerializeField]
    float m_speedMove, m_speedRotate;

    public void move(float speed, float horizontal, float vertical, float timeElapsed)
    {
        Vector3 dir = (transform.forward * vertical  + transform.right * horizontal).normalized;
        m_rigidbody.MovePosition(m_rigidbody.transform.position + dir * speed * timeElapsed);
    }
    public void rotate(float speed, float horizontal, float vertical)
    {
        //Debug.Log(horizontal + " <  " + vertical);
        m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(0, speed*horizontal, 0));
        m_head.Rotate(-speed * vertical,0,0);
    }
    void Update()
    {

        move(m_speedMove, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Time.deltaTime);
        rotate(m_speedRotate, Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }



}
