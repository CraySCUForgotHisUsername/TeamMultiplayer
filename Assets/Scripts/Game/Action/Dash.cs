using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;
using UnityEngine.Networking;

public class Dash : Action
{
    [SerializeField]
    float
        m_resourceActivation, 
        m_distance,m_duration;
    [SerializeField]
    bool 
        m_enableAirDash,
        m_enableYDirection,
        m_useVelocityDirection;
    bool m_isActive = false;
    float m_timeElapsed = 0,
        m_distanceMoved = 0,
        //m_distanceMovedOld = 0,
        m_velocity = 0;
    Vector3 m_direction = new Vector3();


    public override void useProcess(NEntity.Entity entity, Motor motor)
    {
        if ((!m_enableAirDash && !motor.IsGrounded) ||
            entity.useResource(m_resourceActivation, false) == 0)
        {
            Debug.Log("FAIL");
            //Was in the air
            //Or failed to pay activation cost
            setReady(true);
            return;
        }
        m_isActive = true;
        m_timeElapsed = 0;
        m_distanceMoved = 0;
        //m_distanceMovedOld = 0;
        m_velocity = m_distance / m_duration;
        if (m_useVelocityDirection)
        {
            m_direction = motor.Velocity.normalized;

        }
        else
        {
            m_direction = motor.m_avatarManager.getAvatar().m_head.transform.forward;

        }
        if (!m_enableYDirection)
        {
            m_direction.y = 0;
            m_direction.Normalize();
        }


        base.useProcess(entity,motor);
    }
    public override void kFixedUpdate(NEntity.Entity entity, Motor motor, float timeElapsed)
    {
        base.kFixedUpdate(entity,motor, timeElapsed);
        if (m_isActive)
        {
            m_timeElapsed += timeElapsed;
            float ratio = Mathf.Min(1, m_timeElapsed / m_duration);
            m_distanceMoved = m_distance * ratio;

            //float move = m_distanceMoved - m_distanceMovedOld;

            motor.Rigidbody.velocity = m_direction * m_velocity;
            //motor.Rigidbody.MovePosition(motor.transform.position + m_direction * move);


            //m_distanceMovedOld = m_distanceMoved;

            if(m_timeElapsed >= m_duration)
            {
                motor.Rigidbody.velocity = Vector3.zero;
               m_isActive = false;

            }
        }
    }
}
