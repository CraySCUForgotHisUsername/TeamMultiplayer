using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class Hover : Action
    {
        [SerializeField]
        float m_resourceUseRate = 1.0f;
        public float m_velocityHorizontal;
        public float m_minHoldTime = 0.25f;
        public float m_forceUpward;
        [SerializeField]
        float 
            m_forceAdjustmentMax,
            m_forceAdjustmentTimeAcceleration;
        bool isUse = false, isActivated = false;
        float m_timeElapsed = 0;
        public override void useProcess(EntityPlayer entity, EntityMotor motor, Avatar avatar)
        {
            base.use(entity,motor, avatar);
            if (isUse) return;
            setActive(entity,motor, true);
        }
        public override void endProcess(EntityPlayer entity, EntityMotor motor, Avatar avatar)
        {
            base.endProcess(entity, motor, avatar   );
            if (!isUse) return;
            setActive(entity,motor, false);
        }
        public void setActive(EntityPlayer entity, EntityMotor motor, bool value)
        {
            entity.m_isRegenResource = !value;
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
        void activate(EntityMotor motor, bool value)
        {
            isActivated = value;
            motor.isUpdateMovement = !value;
        }
        public override void kFixedUpdate(EntityPlayer entity, EntityMotor motor, Avatar m_avatar, float timeElapsed)
        {
           // Debug.Log(isActivated);
            if (!isUse) return;
            base.kUpdate(entity, motor, m_avatar, timeElapsed);
            //motor.Rigidbody.AddForce(-Physics.gravity);
            if (isActivated)
            {
                motor.IsGrounded = false;
                //Vector3 bodyVelocity = new Vector3(motor.Rigidbody.velocity.x,0, motor.Rigidbody.velocity.z);
                //motor.Rigidbody.velocity -= motor.Rigidbody.velocity * Mathf.Min(1, m_airResistance * timeElapsed);
                Vector3 dirFly = (motor.Velocity ).normalized;
                float bodyDownwardForce = Mathf.Min(m_forceAdjustmentMax, Mathf.Max(0,-motor.Rigidbody.velocity.y) );
                Vector3 bodyVelocity = new Vector3(motor.Rigidbody.velocity.x, 0, motor.Rigidbody.velocity.z);

                //if(bodyDownwardForce>0.1f) Debug.Log(bodyDownwardForce);
                float forceUpward = m_forceUpward ;
                motor.Rigidbody.AddForce(Vector3.up * forceUpward * timeElapsed, ForceMode.Impulse);


                motor.Rigidbody.AddForce(Vector3.up * bodyDownwardForce * Mathf.Min(1, m_forceAdjustmentTimeAcceleration * timeElapsed), ForceMode.Impulse);
                Vector3 stablize = Vector3.zero;
                if (motor.Velocity.magnitude != 0)
                {
                    Vector3 desiredVelocity = motor.VelocityDirHorizontal * m_velocityHorizontal;
                    Vector3 desiredVelocityDir = desiredVelocity.normalized;
                    desiredVelocity = desiredVelocityDir * Mathf.Max(desiredVelocity.magnitude, Vector3.Dot( bodyVelocity, desiredVelocity.normalized) );
                    stablize = (desiredVelocity - bodyVelocity);
                    //Debug.Log("STABLIZE " + stablize);
                    motor.Rigidbody.AddForce(stablize * Mathf.Min(1, m_forceAdjustmentTimeAcceleration * timeElapsed), ForceMode.Impulse);
                }
                float resourceRequired = (Vector3.up * forceUpward * timeElapsed).magnitude + (Vector3.up * bodyDownwardForce * Mathf.Min(1, m_forceAdjustmentTimeAcceleration * timeElapsed)).magnitude +
                    (stablize * Mathf.Min(1, 1.5f * timeElapsed)).magnitude ;
                resourceRequired *= m_resourceUseRate;
                float resourceUsed = entity.useResource(resourceRequired, true);
                //Debug.Log("USED FUEL " + resourceRequired);
                if (resourceUsed < resourceRequired)
                {
                    setActive(entity, motor, false);

                }
                //motor.Rigidbody.AddForce(-motor.Rigidbody.velocity * 0.9f);
                //motor.Rigidbody.AddForce(dirFly * m_force * timeElapsed, ForceMode.Impulse);
            }
            // motor.Rigidbody.AddForce(-motor.Rigidbody.velocity*500*timeElapsed);
            // motor.Rigidbody.AddForce(movementDirection  * speed* motor.m_entity.getModSpeed() * timeElapsed );
        }
        /*
       
         * */
        public override void kUpdate(EntityPlayer entity, EntityMotor motor, Avatar avatar, float timeElapsed)
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
    }

}
