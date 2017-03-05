using System.Collections;
using System.Collections.Generic;
using NEntity;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class Rewind : Action
    {
        public float m_activationCost, m_resourcePerSecond;
        bool isUse = false;
        Vector3 m_positionBefore = Vector3.zero;
        public override void useProcess(NEntity.Entity entity, Motor motor)
        {
            base.useProcess(entity, motor);
            //Debug.Log("USE " + !isUse);
            setActive(entity, motor,!isUse);
        }
        public override void endProcess(Entity entity, Motor motor)
        {
            base.endProcess(entity, motor);
            //setActive(entity, motor, false);
        }
        void setActive(Entity entity, Motor motor, bool state)
        {
            //Debug.Log("REWIND SET " + state);
            if (isUse == state) return;
            isUse = state;
            if (isUse)
            {
                float result = entity.useResource(m_activationCost, false);
                if(result == 0)
                {
                    //failed to pay the activation cost
                    isUse = false;
                    setReady(true);
                    return;
                }
                m_positionBefore = motor.transform.position;

            }else
            {
                motor.Rigidbody.MovePosition( m_positionBefore);

            }
        }
        public override void kUpdate(NEntity.Entity entity, Motor motor, float timeElapsed)
        {
            base.kUpdate(entity, motor, timeElapsed);
            if (!isUse) return;
            float resource = m_resourcePerSecond * timeElapsed;
            float resourceUsed = entity.useResource(resource, true);
            if (resourceUsed < resource)
            {
                //Debug.Log("SET FALSE TO" + resourceUsed + " AND " + resource);
                setActive(entity, motor, false);

            }
        }
    }

}
