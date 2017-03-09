using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class AirDash : Action
    {
        [SerializeField]
        float 
            m_forceUsed;
   
        public override void useProcess(Entity entity, EntityMotor motor, Avatar avatar)
        {
            base.use(entity, motor, avatar);
            var dir = motor.VelocityRelativeDir.z * avatar.Look + motor.VelocityRelativeDir.x * avatar.Right;
            if (motor.IsGrounded)
            {
                dir.y = 1;
                dir.Normalize();
            }
            if(dir.magnitude == 0)
            {
                dir = Vector3.up;
            }
            var force = dir * m_forceUsed;
            Debug.Log(avatar.Look);
            Debug.Log("MOTOR VELO " + motor.VelocityDirHorizontal);
            Debug.Log(force);
            motor.Rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }

}
