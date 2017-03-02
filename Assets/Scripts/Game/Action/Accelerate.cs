using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;
using UnityEngine.Networking;

public class Accelerate : Action
{
    [SerializeField]
    float m_speedBonus;
    bool m_isActive = false;


    public override void use(Motor motor)
    {
        base.use(motor);
        if (m_isActive) return;
        m_isActive = true;
        motor.m_scalarsSpeed.Add(getBonusSpeed);
    }
    public override void end(Motor motor)
    {
        base.end(motor);
        if (!m_isActive) return;
        motor.m_scalarsSpeed.Remove(getBonusSpeed);
        m_isActive = false;
    }
    float getBonusSpeed(Motor motor) {
        return m_speedBonus;
    }
    
    public override void kFixedUpdate(Motor motor, float timeElapsed)
    {
        base.kFixedUpdate(motor, timeElapsed);
        if (m_isActive)
        {
        }
    }
}
