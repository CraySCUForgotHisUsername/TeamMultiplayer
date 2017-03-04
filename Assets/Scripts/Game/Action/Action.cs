using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Action : MonoBehaviour  {
    
    bool m_isReady = true;
    float m_delayTimeElapsed = 0;

    [SerializeField]
    float m_delayMax = 0;

    [SerializeField]
    bool    m_ammoIsNeeded = false,
            m_ammoIsStream = false;
    [SerializeField]
    int m_ammoRequired = 0;

    [SerializeField]
    bool    m_resourceIsNeeded = false,
            m_resourceIsStream = false;
    [SerializeField]
    float m_resourceRequired = 0;

    

    public virtual void init(NMotor.Motor motor)
    {

    }
    // Use this for initialization
    public virtual void runLocal(PlayerController playerController)
    {

    }
    public virtual void runServer(PlayerController playerController)
    {

    }
    public void use(NEntity.Entity entity, NMotor.Motor motor)
    {
        if (!m_isReady) return;
        m_isReady = false;
        bool canUse = true;
        if (m_ammoIsNeeded && entity.useAmmoTest(m_ammoRequired, m_ammoIsStream) == 0)
            canUse = false;
        if (m_resourceIsNeeded && entity.useResourceTest(m_resourceRequired, m_resourceIsStream) == 0)
            canUse = false;
        if (!canUse) return;

        if (m_ammoIsNeeded) entity.useAmmo(m_ammoRequired, m_ammoIsStream);
        if (m_resourceIsNeeded) entity.useResource(m_resourceRequired, m_resourceIsStream);
        
        useProcess(entity, motor);
    }
    public void hold(NEntity.Entity entity, NMotor.Motor motor)
    {

    }
    public void end(NEntity.Entity entity, NMotor.Motor motor)
    {
        endProcess(entity, motor);
    }
    public virtual void useProcess(NEntity.Entity entity, NMotor.Motor motor)
    {

    }
    public virtual void holdProcess(NEntity.Entity entity, NMotor.Motor motor)
    {

    }
    public virtual void endProcess(NEntity.Entity entity, NMotor.Motor motor)
    {

    }
    protected void setReady(bool value)
    {
        m_isReady = value;
        m_delayTimeElapsed = 0;
    }
    public virtual void kUpdate(NEntity.Entity entity, NMotor.Motor motor, float timeElapsed)
    {
        if (m_isReady) return;
        m_delayTimeElapsed += timeElapsed;
        if (m_delayTimeElapsed > m_delayMax)
        {
            m_isReady = true;
            m_delayTimeElapsed = 0;
        }
    }
    public virtual void kFixedUpdate(NEntity.Entity entity, NMotor.Motor motor, float timeElapsed)
    {

    }
}
