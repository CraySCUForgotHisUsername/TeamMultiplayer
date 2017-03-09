using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class Wallride : Action
    {
        public float activationCost;
        public bool isClimbVertical, isClimbHorizontal;
        public float m_climbingSpeed,m_speedWallRiding;
        public float m_climbEndNudgeForce;
        public float m_wallFindRadius;
        enum WALLRIDE_STATE { INACTIVE, READY, CLIMBING, SLIDING};
        WALLRIDE_STATE m_wallRideState = WALLRIDE_STATE.INACTIVE;
        float normalAllowance = 0.1f;
        Vector3 m_wallDirection;
        Vector3 m_wallNormal;
        Vector3 slidingDirection;

        
        void hdr_jump(Entity entity, EntityMotor motor, Avatar avatar, float horizontal, float vertical)
        {

        }
        void setState(Entity entity, EntityMotor motor, WALLRIDE_STATE state)
        {
            if(state == WALLRIDE_STATE.INACTIVE )
            {
                if (m_wallRideState == WALLRIDE_STATE.CLIMBING)
                {
                    Debug.Log("NUDGE");
                    motor.Rigidbody.AddForce(Vector3.up * m_climbEndNudgeForce,ForceMode.Impulse);

                }

            }
            if (state == WALLRIDE_STATE.CLIMBING || state == WALLRIDE_STATE.SLIDING)
            {
                if (entity.useResource(activationCost, false) < activationCost) return;
                motor.Rigidbody.velocity = Vector3.zero;
                motor.isUpdateGravity = false;
                motor.isUpdateMovement = false;

            }
            else
            {
                motor.isUpdateGravity = true;
                motor.isUpdateMovement = true;
            }
            m_wallRideState = state;

        }
        public override void useProcess(Entity entity, EntityMotor motor, Avatar avatar)
        {
            base.useProcess(entity, motor, avatar);
            setState(entity,motor, WALLRIDE_STATE.READY);
        }
        public override void endProcess(Entity entity, EntityMotor motor, Avatar avatar)
        {
            base.endProcess(entity, motor, avatar);
            setState(entity, motor, WALLRIDE_STATE.INACTIVE);
        }
        public override void kUpdate(Entity entity, EntityMotor motor, Avatar avatar,float timeElapsed)
        {
            base.kUpdate(entity, motor, avatar, timeElapsed);
            if (m_wallRideState == WALLRIDE_STATE.INACTIVE) return;
            //Debug.Log("" + m_wallRideState);
            if (m_wallRideState == WALLRIDE_STATE.READY) {
                udpateFindWall(entity, motor, avatar, timeElapsed);
                return;
            }
            if(m_wallRideState== WALLRIDE_STATE.CLIMBING)
            {
                Vector3 velo = new Vector3(m_wallDirection.x,1,m_wallDirection.z)* entity.Speed *m_speedWallRiding;
                motor.Rigidbody.MovePosition(motor.transform.position + velo * timeElapsed);
                Vector3 previousWallNormal = m_wallNormal;
                if(!hprFindWall(avatar.transform.position, m_wallDirection, m_wallFindRadius))
                {
                    setState(entity, motor, WALLRIDE_STATE.INACTIVE);
                    return;
                }
                if(1-Vector3.Dot(previousWallNormal, m_wallNormal) > normalAllowance)
                {

                    setState(entity, motor, WALLRIDE_STATE.INACTIVE);
                    return;
                }

            }

            if (m_wallRideState == WALLRIDE_STATE.SLIDING)
            {
                Vector3 forwardDir = Vector3.Cross(m_wallNormal, new Vector3(0, 1, 0));
                //forwardDir.y = 0;
                forwardDir.Normalize();
                Vector3 velo = (new Vector3(m_wallDirection.x, 0, m_wallDirection.z)+ forwardDir) * entity.Speed * m_speedWallRiding;
                motor.Rigidbody.MovePosition(motor.transform.position + velo * timeElapsed);
                Vector3 previousWallNormal = m_wallNormal;
                if (!hprFindWall(avatar.transform.position, m_wallDirection, m_wallFindRadius))
                {
                    setState(entity, motor, WALLRIDE_STATE.INACTIVE);
                    return;
                }
                if (1 - Vector3.Dot(previousWallNormal, m_wallNormal) > normalAllowance)
                {

                    setState(entity, motor, WALLRIDE_STATE.INACTIVE);
                    return;
                }


            }

        }
        bool hprFindWall(Vector3 pos, Vector3 dir, float maxDistance)
        {
            RaycastHit hit;
            Physics.Raycast(new Ray( pos, dir), out hit, maxDistance);
            if(hit.transform == null) return false;
            m_wallDirection = dir;
            m_wallNormal = hit.normal;
            return true;
        }
        void udpateFindWall(Entity entity, EntityMotor motor, Avatar avatar, float timeElapsed)
        {
            Debug.Log("Looking for a wall");
            
            if (isClimbVertical)
            {
                if(hprFindWall(avatar.transform.position, avatar.transform.forward, m_wallFindRadius))
                {
                    setState(entity, motor, WALLRIDE_STATE.CLIMBING);
                    return;
                }

            }
            if (isClimbHorizontal)
            {
                if(hprFindWall(avatar.transform.position, avatar.transform.right, m_wallFindRadius) || hprFindWall(avatar.transform.position, -avatar.transform.right, m_wallFindRadius))
                    setState(entity, motor, WALLRIDE_STATE.SLIDING);


            }

        }
    }

}
