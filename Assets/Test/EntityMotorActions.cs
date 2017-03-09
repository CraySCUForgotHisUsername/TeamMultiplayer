using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using NAction;

public class EntityMotorActions : MonoBehaviour {

    public List<Action>
        m_actJump,
        m_actLMB,
        m_actRMB,
        m_actR,
        m_actF,
        m_actShift,
        m_actE,
        m_actQ;

    void hprLink(List<Action> from, List<Action> to)
    {
        for (int i = 0; i < from.Count; i++)
        {
            to.Add(from[i]);
        }

    }
    void hprUnLink(List<Action> from, List<Action> to)
    {
        for (int i = 0; i < from.Count; i++)
        {
            to.Remove(from[i]);
        }

    }
    public void link(EntityMotor motor)
    {
        this.transform.parent = motor.transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        hprLink(m_actJump, motor.m_actJump);
        hprLink(m_actLMB, motor.m_actLMB);
        hprLink(m_actRMB, motor.m_actRMB);
        hprLink(m_actR, motor.m_actR);
        hprLink(m_actF, motor.m_actF);
        hprLink(m_actShift, motor.m_actShift);
        hprLink(m_actE, motor.m_actE);
        hprLink(m_actQ, motor.m_actQ);
    }
    public void unLink(EntityMotor motor)
    {
        hprUnLink(m_actJump, motor.m_actJump);
        hprUnLink(m_actLMB, motor.m_actLMB);
        hprUnLink(m_actRMB, motor.m_actRMB);
        hprUnLink(m_actR, motor.m_actR);
        hprUnLink(m_actF, motor.m_actF);
        hprUnLink(m_actShift, motor.m_actShift);
        hprUnLink(m_actE, motor.m_actE);
        hprUnLink(m_actQ, motor.m_actQ);

    }
}
