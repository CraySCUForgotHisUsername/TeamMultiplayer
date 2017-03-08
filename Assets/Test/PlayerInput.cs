using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using System.Collections.Generic;



public class PlayerInput : NetworkBehaviour
{

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
    public void KUpdate(Entity entity, EntityMotor motor, Avatar m_avatar ,float timeElapsed)
    {
        if (entity == null) return;
        
        //move(m_rigidbody,m_speedMove, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), timeElapsed);
        //rotate(m_rigidbody, m_avatar,m_speedRotate, Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //updateMotorInput();
    }
    
    public virtual void updateMotorInput()
    {
        /*
        m_motor.move(m_entity, m_entity.Speed,Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_motor.rotate(m_entity.RotationSpeed, Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        headRotation = m_motor.m_avatarManager.HeadRotation;
        CmdSetHeadRotation(headRotation.eulerAngles.x, headRotation.eulerAngles.y, headRotation.eulerAngles.z);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_motor.jumpBegin(m_entity, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            m_motor.jumpEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_motor.crawlBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            m_motor.crawlEnd(m_entity);
        }

        if (!m_motor.IsReadyForInput) return;
        if (Input.GetMouseButton(0)){
            m_motor.actLMBBegin(m_entity);
        }
        else if (Input.GetMouseButtonUp(0)) {
            m_motor.actRMBEnd(m_entity);
        }
        if (Input.GetMouseButton(1)){
            m_motor.actRMBBegin(m_entity);
        }
        else if (Input.GetMouseButtonUp(1)){
            m_motor.actRMBEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_motor.actRBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            m_motor.actREnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_motor.actShiftBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_motor.actShiftEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_motor.actEBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            m_motor.actEEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            m_motor.actFBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            m_motor.actFEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_motor.actQBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            m_motor.actQEnd(m_entity);
        }
         * */
    }



}
