using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using System.Collections.Generic;



public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    float m_speedMove, m_speedRotate;

    public void move(Rigidbody m_rigidbody, float speed, float horizontal, float vertical, float timeElapsed)
    {
        Vector3 dir = (transform.forward * vertical  + transform.right * horizontal).normalized;
        m_rigidbody.MovePosition(m_rigidbody.transform.position + dir * speed * timeElapsed);
    }
    public void rotate(Rigidbody m_rigidbody, Avatar m_avatar, float speed, float horizontal, float vertical)
    {
        m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(0, speed*horizontal, 0));
        if(m_avatar != null)
            m_avatar.m_head.transform.Rotate(-speed * vertical,0,0);
    }
    public void KUpdate(Rigidbody m_rigidbody, Avatar m_avatar ,float timeElapsed)
    {
        //if (m_avatar == null) return;

        move(m_rigidbody,m_speedMove, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), timeElapsed);
        rotate(m_rigidbody, m_avatar,m_speedRotate, Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }



}
