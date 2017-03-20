using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace NAction {

    public class Action : MonoBehaviour
    {

        public bool m_isReceiveInput = true;
        bool m_isReady = true;
        float m_delayTimeElapsed = 0;

        [SerializeField]
        float m_delayMax = 0;

        [SerializeField]
        bool m_ammoIsNeeded = false,
                m_ammoIsStream = false;
        [SerializeField]
        int m_ammoRequired = 0;

        [SerializeField]
        bool m_resourceIsNeeded = false,
                m_resourceIsStream = false;
        [SerializeField]
        float m_resourceRequired = 0;



        public virtual void init(EntityMotor motor)
        {

        }
        public virtual void unInit(EntityMotor motor)
        {

        }
        // Use this for initialization
        public virtual void runLocal(PlayerController playerController)
        {

        }
        public virtual void runServer(PlayerController playerController)
        {

        }
        public void use(EntityPlayer entity, EntityMotor motor,Avatar avatar)
        {
            if (!m_isReceiveInput) return;
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

            useProcess(entity, motor, avatar);
        }

        public void hold(EntityPlayer entity, EntityMotor motor)
        {
            if (!m_isReceiveInput) return;

        }
        public void end(EntityPlayer entity, EntityMotor motor, Avatar avatar)
        {
            if (!m_isReceiveInput) return;
            endProcess(entity, motor, avatar);
        }
        public virtual void useProcess(EntityPlayer entity, EntityMotor motor, Avatar avatar)
        {

        }
        public virtual void holdProcess(EntityPlayer entity, EntityMotor motor)
        {

        }
        public virtual void endProcess(EntityPlayer entity, EntityMotor motor, Avatar avatar)
        {

        }
        protected void setReady(bool value)
        {
            m_isReady = value;
            m_delayTimeElapsed = 0;
        }
        public virtual void kUpdate(EntityPlayer entity, EntityMotor motor, Avatar avatar, float timeElapsed)
        {
            if (m_isReady) return;
            m_delayTimeElapsed += timeElapsed;
            if (m_delayTimeElapsed > m_delayMax)
            {
                m_isReady = true;
                m_delayTimeElapsed = 0;
            }
        }
        public virtual void kFixedUpdate(EntityPlayer entity, EntityMotor motor, Avatar avatar, float timeElapsed)
        {

        }
    }

}
