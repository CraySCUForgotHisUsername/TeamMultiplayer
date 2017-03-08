using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class FlyHover : Action
    {
        [SerializeField]
        float m_resourceUseRate = 1.0f;
        public float m_minHoldTime = 1.0f;
        public float m_forceUpward;
        [SerializeField]
        float m_forceAdjustmentMax;
        bool isUse = false, isActivated = false;
        float m_timeElapsed = 0;
        /*
        public override void useProcess(NEntity.Entity entity,Motor motor)
        {
            base.use(entity,motor);
            if (isUse) return;
            setActive(motor,true);
        }
        public override void endProcess(NEntity.Entity entity, Motor motor)
        {
            base.endProcess(entity, motor);
            if (!isUse) return;
            setActive(motor,false);
        }
        public void setActive(Motor motor, bool value)
        {
            if (value) {

                isUse = true;
                isActivated = false;
                m_timeElapsed = 0;
            }
            else
            {

                isUse = false;
                m_timeElapsed = 0;
                activate(motor, false);
            }
        }
        public override void kFixedUpdate(NEntity.Entity entity, Motor motor, float timeElapsed)
        {
           // Debug.Log(isActivated);
            if (!isUse) return;
            base.kUpdate(entity, motor, timeElapsed);
            //motor.Rigidbody.AddForce(-Physics.gravity);
            if (isActivated)
            {
                //Vector3 bodyVelocity = new Vector3(motor.Rigidbody.velocity.x,0, motor.Rigidbody.velocity.z);
                //motor.Rigidbody.velocity -= motor.Rigidbody.velocity * Mathf.Min(1, m_airResistance * timeElapsed);
                Vector3 dirFly = (motor.Velocity ).normalized;
                float bodyDownwardForce = Mathf.Min(m_forceAdjustmentMax, Mathf.Max(0,-motor.Rigidbody.velocity.y) );
                Vector3 bodyVelocity = new Vector3(motor.Rigidbody.velocity.x, 0, motor.Rigidbody.velocity.z);
                Vector3 stablize = (motor.Velocity - bodyVelocity);


                //if(bodyDownwardForce>0.1f) Debug.Log(bodyDownwardForce);
                motor.Rigidbody.AddForce(Vector3.up * m_forceUpward * timeElapsed, ForceMode.Impulse);
                motor.Rigidbody.AddForce(Vector3.up * bodyDownwardForce * Mathf.Min(1, 3 * timeElapsed), ForceMode.Impulse);
                motor.Rigidbody.AddForce(stablize * Mathf.Min(1, 1.5f * timeElapsed), ForceMode.Impulse);
                float resourceRequired = (Vector3.up * m_forceUpward * timeElapsed).magnitude + (Vector3.up * bodyDownwardForce * Mathf.Min(1, 3 * timeElapsed)).magnitude +
                    (stablize * Mathf.Min(1, 1.5f * timeElapsed)).magnitude ;
                resourceRequired *= m_resourceUseRate;
                float resourceUsed = entity.useResource(resourceRequired, true);
                //Debug.Log("USED FUEL " + resourceRequired);
                if (resourceUsed < resourceRequired)
                {
                    setActive(motor, false);

                }
                //motor.Rigidbody.AddForce(-motor.Rigidbody.velocity * 0.9f);
                //motor.Rigidbody.AddForce(dirFly * m_force * timeElapsed, ForceMode.Impulse);
            }
            // motor.Rigidbody.AddForce(-motor.Rigidbody.velocity*500*timeElapsed);
            // motor.Rigidbody.AddForce(movementDirection  * speed* motor.m_entity.getModSpeed() * timeElapsed );
        }
        void activate(Motor motor, bool value)
        {
            isActivated = value;
            //motor.isUpdateMovement = !value;
        }
        public override void kUpdate(NEntity.Entity entity, Motor motor, float timeElapsed)
        {
           // Debug.Log(motor.isUpdateMovement);
            if (!isUse) return;
            m_timeElapsed += timeElapsed;
            if (m_timeElapsed < m_minHoldTime) return;
            activate(motor,true);
            //Debug.Log("FLYING");
            //base.kUpdate(motor, timeElapsed);
            //motor.Rigidbody.AddForce(-Physics.gravity *2.0f * timeElapsed);
        }
         * */
    }

}
