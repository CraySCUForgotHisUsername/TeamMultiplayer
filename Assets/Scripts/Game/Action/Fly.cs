using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction {

    public class Fly :  Action
    {
        public List<DEL.VOID_ENTITY_MOTOR> m_evntFlyBegin = new List<DEL.VOID_ENTITY_MOTOR>();
        public List<DEL.VOID_ENTITY_MOTOR> m_evntFlyEnd = new List<DEL.VOID_ENTITY_MOTOR>();
        public float m_resourceActivation = 10.0f;
        public float m_resourcePerSecond = 10.0f;
        public float m_speed = 3.0f;
        public float m_timeAcceleration = 3.0f;
        bool isUse = false;
        Vector3 m_dirHorizontal;
        Vector3 m_dirVertical = Vector3.zero;
        public override void useProcess(NEntity.Entity entity, Motor motor)
        {
            base.useProcess(entity,motor);
            if (isUse)
            {
                setActive(entity,motor, false);
            }
            else
            {
                setActive(entity, motor, true);

            }
        }
        void setActive(NEntity.Entity entity, Motor motor,bool value)
        {
            if (!value)
            {

                isUse = false;
                m_dirHorizontal = Vector3.zero;
                motor.isUpdateMovement = true;

                motor.m_evntMoves.Remove(flyHorizontally);
                motor.m_evntJump.Remove(flyUp);
                motor.m_evntJumpStop.Remove(flyUpDownStop);
                motor.m_evntCrawlBegin.Remove(flyDown);
                motor.m_evntCrawlEnd.Remove(flyUpDownStop);
                DEL.RAISE(m_evntFlyEnd, entity, motor);
            }
            else
            {
                if(entity.useResource(m_resourceActivation, false) == 0 )
                {
                    //Activation cost is not paid.
                    //Activation failed.
                    return;
                }
                isUse = true;
                motor.isUpdateMovement = false;
                motor.m_evntMoves.Add(flyHorizontally);
                motor.m_evntJump.Add(flyUp);
                motor.m_evntJumpStop.Add(flyUpDownStop);
                motor.m_evntCrawlBegin.Add(flyDown);
                motor.m_evntCrawlEnd.Add(flyUpDownStop);
                DEL.RAISE(m_evntFlyBegin, entity, motor);

            }

        }
        void flyHorizontally(NEntity.Entity entity, Motor motor, float horizontal, float vertical)
        {
            var avatar = motor.m_avatarManager.getAvatar();
            var avatarCollision = motor.m_avatarManager.getAvatarCollision();
            m_dirHorizontal = (avatar.m_head.transform.forward * vertical + avatarCollision.m_head.transform.right * horizontal).normalized;
        }
        void flyUp(NEntity.Entity entity, Motor motor, float horizontal, float vertical)
        {
            m_dirVertical = Vector3.up;
        }
        void flyUpDownStop(NEntity.Entity entity, Motor motor)
        {
            m_dirVertical = Vector3.zero;

        }
        void flyDown(NEntity.Entity entity, Motor motor)
        {
            m_dirVertical = Vector3.down;

        }
        public override void kFixedUpdate(NEntity.Entity entity, Motor motor, float timeElapsed)
        {
            base.kFixedUpdate(entity,motor, timeElapsed);
            if (!isUse) return;
            Vector3 dir = (m_dirHorizontal + m_dirVertical ).normalized;
            Vector3 velocityDesired = dir * m_speed;
            Vector3 velocityDiff = velocityDesired - motor.Rigidbody.velocity;
            //motor.Rigidbody.AddForce(-Physics.gravity);
            motor.Rigidbody.AddForce(velocityDiff * Mathf.Min(1.0f,  timeElapsed* m_timeAcceleration), ForceMode.Impulse);
            float fuelCost = (velocityDiff * Mathf.Min(1.0f, timeElapsed * m_timeAcceleration)).magnitude;
            fuelCost = m_resourcePerSecond * timeElapsed;
            if (entity.useResource(fuelCost, true) < fuelCost ) {
                setActive(entity, motor, false);
                Debug.Log("NO MORE FUEL");
            }
            //motor.Rigidbody.velocity -= motor.Rigidbody.velocity *Mathf.Min(1, m_airResistance * timeElapsed);


            //motor.Rigidbody.MovePosition(motor.transform.position + dir * m_speed * timeElapsed);
            // motor.Rigidbody.AddForce(-motor.Rigidbody.velocity*500*timeElapsed);
            // motor.Rigidbody.AddForce(movementDirection  * speed* motor.m_entity.getModSpeed() * timeElapsed );
        }
    }

}
