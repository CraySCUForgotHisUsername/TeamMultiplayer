using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction {

    public class Fly :  Action
    {
        public float m_airResistance = 3.0f;
        public float speed;
        bool isUse = false;
        Vector3 m_dirHorizontal;
        Vector3 m_dirVertical = Vector3.zero;
        public override void use(NEntity.Entity entity, Motor motor)
        {
            base.use(entity,motor);
            if (isUse)
            {

                isUse = false;
                m_dirHorizontal = Vector3.zero;
                motor.isUpdateMovement = true;

                motor.m_evntMoves.Remove(flyHorizontally);
                motor.m_evntCrawl.Remove(flyDown);
                motor.m_evntJump.Remove(flyUp);
                motor.m_evntJumpStop.Remove(flyUpDownStop);
                motor.m_evntCrawlStop.Remove(flyUpDownStop);
            }
            else
            {
                isUse = true;
                motor.isUpdateMovement = false;
                motor.m_evntMoves.Add(flyHorizontally);
                motor.m_evntCrawl.Add(flyDown);
                motor.m_evntJump.Add(flyUp);
                motor.m_evntJumpStop.Add(flyUpDownStop);
                motor.m_evntCrawlStop.Add(flyUpDownStop);

            }
        }
        void flyHorizontally(Motor motor, float horizontal, float vertical)
        {
            var avatar = motor.m_avatarManager.getAvatar();
            var avatarCollision = motor.m_avatarManager.getAvatarCollision();
            m_dirHorizontal = (avatar.m_head.transform.forward * vertical + avatarCollision.m_head.transform.right * horizontal).normalized;
        }
        void flyUp(Motor motor, float horizontal, float vertical)
        {
            m_dirVertical = Vector3.up;
        }
        void flyUpDownStop(Motor motor)
        {
            m_dirVertical = Vector3.zero;

        }
        void flyDown(Motor motor)
        {
            m_dirVertical = Vector3.down;

        }
        public override void kFixedUpdate(NEntity.Entity entity, Motor motor, float timeElapsed)
        {
            if (!isUse) return;
            base.kUpdate(entity,motor, timeElapsed);
            Vector3 dir = (m_dirHorizontal + m_dirVertical ).normalized;
            //motor.Rigidbody.AddForce(-Physics.gravity);
            motor.Rigidbody.velocity -= motor.Rigidbody.velocity *Mathf.Min(1, m_airResistance * timeElapsed);
            motor.Rigidbody.MovePosition(motor.transform.position + dir * speed * timeElapsed);
           // motor.Rigidbody.AddForce(-motor.Rigidbody.velocity*500*timeElapsed);
           // motor.Rigidbody.AddForce(movementDirection  * speed* motor.m_entity.getModSpeed() * timeElapsed );
        }
        public override void kUpdate(NEntity.Entity entity, Motor motor, float timeElapsed)
        {
            //Debug.Log("FLYING");
            //base.kUpdate(motor, timeElapsed);
            //motor.Rigidbody.AddForce(-Physics.gravity *2.0f * timeElapsed);
        }
    }

}
