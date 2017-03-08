using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class Fly_Disable : Fly
    {
        /*
        [SerializeField]
        List<Action> m_disabledActions;
        void Awake()
        {
            m_evntFlyBegin.Add( hdrDisable);
            m_evntFlyEnd.Add( hdrEnable);
        }
        void hdrEnable(NEntity.Entity entity, Motor motor)
        {
            for(int i = 0; i < m_disabledActions.Count; i++)
            {
                m_disabledActions[i].m_isReceiveInput = true;

            }

        }
        void hdrDisable(NEntity.Entity entity, Motor motor)
        {
            for (int i = 0; i < m_disabledActions.Count; i++)
            {
                m_disabledActions[i].end(entity, motor);
                m_disabledActions[i].m_isReceiveInput = false;
            }

        }
         * */
    }

}
