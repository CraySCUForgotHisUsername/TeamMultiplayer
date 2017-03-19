using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using NAction;

public class EntityMotorActions : MonoBehaviour {

    public List<Action>
        m_actPassive,
        m_actJump,
        m_actLMB,
        m_actRMB,
        m_actR,
        m_actF,
        m_actShift,
        m_actE,
        m_actQ;

    void hprLink(EntityMotor motor, List<Action> from, List<Action> to)
    {
        for (int i = 0; i < from.Count; i++)
        {
            from[i].init(motor);
            to.Add(from[i]);
        }

    }
    void hprUnLink(EntityMotor motor, List<Action> from, List<Action> to)
    {
        for (int i = 0; i < from.Count; i++)
        {
            from[i].unInit(motor);
            to.Remove(from[i]);
        }

    }
    public void link(EntityMotor motor)
    {
        this.transform.parent = motor.transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        hprLink(motor, m_actPassive, motor.m_actPassive);
        hprLink(motor, m_actJump, motor.m_actJump);
        hprLink(motor,m_actLMB, motor.m_actLMB);
        hprLink(motor,m_actRMB, motor.m_actRMB);
        hprLink(motor,m_actR, motor.m_actR);
        hprLink(motor,m_actF, motor.m_actF);
        hprLink(motor,m_actShift, motor.m_actShift);
        hprLink(motor,m_actE, motor.m_actE);
        hprLink(motor,m_actQ, motor.m_actQ);
    }
    public void unLink(EntityMotor motor)
    {
        hprUnLink(motor, m_actPassive, motor.m_actPassive);
        hprUnLink(motor, m_actJump, motor.m_actJump);
        hprUnLink(motor,m_actLMB, motor.m_actLMB);
        hprUnLink(motor,m_actRMB, motor.m_actRMB);
        hprUnLink(motor,m_actR, motor.m_actR);
        hprUnLink(motor,m_actF, motor.m_actF);
        hprUnLink(motor,m_actShift, motor.m_actShift);
        hprUnLink(motor,m_actE, motor.m_actE);
        hprUnLink(motor,m_actQ, motor.m_actQ);

    }
}
