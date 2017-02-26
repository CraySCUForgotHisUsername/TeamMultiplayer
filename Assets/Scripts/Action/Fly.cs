using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction {

    public class Fly :  Action{
        public float speed;
        bool isUse = false;
        Vector3 movementDirection;
        public override void use(Motor motor)
        {
            base.use(motor);
            if (isUse)
            {

                isUse = false;
                movementDirection = Vector3.zero;
                motor.isUpdateMovement = true;
                motor.m_evntMoves.Remove(flyHorizontally);
                motor.m_evntJump.Remove(flyVertically);
                motor.m_evntCrawl.Remove(flyDown);
            }
            else
            {
                isUse = true;
                motor.isUpdateMovement = false;
                motor.m_evntMoves.Add(flyHorizontally);
                motor.m_evntJump.Add(flyVertically);
                motor.m_evntCrawl.Add(flyDown);

            }
        }
        void flyHorizontally(Motor motor, float horizontal, float vertical)
        {
            movementDirection = (motor.getCollisionAvatar().m_head.transform.forward * vertical + motor.getCollisionAvatar().m_head.transform.right * horizontal).normalized;
        }
        void flyVertically(Motor motor, float horizontal, float vertical)
        {

        }
        void flyDown(Motor motor)
        {

        }
        public override void kFixedUpdate(Motor motor, float timeElapsed)
        {
            if (!isUse) return;
            //Debug.Log("FLYING");
            base.kUpdate(motor, timeElapsed);
            motor.Rigidbody.AddForce(-Physics.gravity);
            motor.Rigidbody.velocity -= motor.Rigidbody.velocity*2.0f * timeElapsed;
            motor.Rigidbody.MovePosition(motor.transform.position + movementDirection * speed * timeElapsed);
           // motor.Rigidbody.AddForce(-motor.Rigidbody.velocity*500*timeElapsed);
           // motor.Rigidbody.AddForce(movementDirection  * speed* motor.m_entity.getModSpeed() * timeElapsed );
        }
        public override void kUpdate(Motor motor, float timeElapsed)
        {
            //Debug.Log("FLYING");
            //base.kUpdate(motor, timeElapsed);
            //motor.Rigidbody.AddForce(-Physics.gravity *2.0f * timeElapsed);
        }
    }

}
