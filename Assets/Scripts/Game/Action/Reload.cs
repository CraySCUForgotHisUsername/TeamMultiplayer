using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace NAction {
    public class Reload : Action
    {

        [SerializeField]
        float m_reloadingTimeDelay = 1.5f;

        bool m_isReloading = false;
        float m_reloadingDelayTimeElapsed = 0;
        /*
        public override void useProcess(Entity entity, Motor motor)
        {
            base.useProcess(entity, motor);
            if (m_isReloading) return;
            m_isReloading = true;
            m_reloadingDelayTimeElapsed = m_reloadingTimeDelay;
            motor.setDelay(m_reloadingTimeDelay);
        }
        public override void kUpdate(Entity entity, Motor motor, float timeElapsed)
        {
            base.kUpdate(entity, motor, timeElapsed);
            if (!m_isReloading) return;

            m_reloadingDelayTimeElapsed -= timeElapsed;
            if(m_reloadingDelayTimeElapsed <=0)
            {
                m_isReloading = false;
                entity.m_ammo = entity.m_ammoMax;

            }
        }
         * */

    }

}

