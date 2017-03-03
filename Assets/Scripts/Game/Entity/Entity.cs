using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace NEntity {
    public class Entity : NetworkBehaviour
    {
        public delegate float DEL_MODIFIER();
        static float DAMAGE_AVOID_DISTANCE = 1.0f;
        
        [SyncVar]
        [SerializeField]
        // Basic properties of all interactable objects in the game
        float health = 100.0f;

        [SyncVar]
        [SerializeField]
        float
            m_speed = 10.0f,
            m_jumpPower = 300.0f,
            m_gravityPower = 800.0f;
        [SyncVar]
        [SerializeField]
        float shield = 0.0f; //this is the type of health that regenerates

        [SyncVar]
        [SerializeField]
        float   modGravity = 0.0f,
                modAir     = 0.0f,
                modOffense = 0.0f, //Bonus damage or reduced damage and such
                modDefense = 0.0f, //Reduced damage or increased damage taken and such
                modSpeed = 0.0f;   //Increased speed or decreased speed 


        public List<DEL_MODIFIER> scalarOffense = new List<DEL_MODIFIER>();
        public List<DEL_MODIFIER> scalarDefense = new List<DEL_MODIFIER>();
        public List<DEL_MODIFIER> scalarSpeed = new List<DEL_MODIFIER>();
        public List<DEL_MODIFIER> scalarJump = new List<DEL_MODIFIER>();
        public float AirControl {
            get
            {
                return 1 + modAir;
            }
        }

        public float Health { get { return health; } }
        public float Speed
        {
            get
            {
                float scalar = 1 + Mathf.Max(-1, scalarSpeed.Select(s => s()).Sum());
                //Debug.Log(scalar);
                return m_speed * scalar;
            }

        }
        public float Jump
        {
            get
            {
                float scalar = 1 + Mathf.Max(-1, scalarJump.Select(s => s()).Sum());
                //Debug.Log(scalar);
                return m_jumpPower * scalar;
            }

        }
        public float Gravity {
            get
            {
                return m_gravityPower;
            }
        }


        public float RotationSpeed
        {
            get
            {
                return 1.0f;
            }
        }
        bool isTakingDamage = true;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void takeDamageRaw(float amount)
        {
            float mod = modDefense;
            for (int i = 0; i < scalarDefense.Count; i++)
            {
                mod += scalarDefense[i]();
            }
            //mod = Mathf.Max(1.0f )
            //Debug.Log("Raw damage " + amount);
            health -= amount;
            if (health < 1)
            {
                health = 0;

            }
        }
        public bool IsTakeDamage
        {
            get
            {
                return isTakingDamage;
            }
        }
        public float howMuchDamageWillBeTaken(float amount)
        {
            return amount;
        }
        public bool takeDamage(float amount)
        {
            Debug.Log("takeDamage damage " + amount);
            if (!isTakingDamage) return false;
            health -= amount;
            if (health < 1)
            {
                health = 0;

            }
            return true;
        }
        [Command]
        void CmdClinetUpdatingHealth(float h)
        {
            this.health = h;
        }
        [TargetRpc]
        public void TargetTakeDamage(NetworkConnection target, Vector3 impactPoint, float damage)
        {
            Debug.Log("Hello I am told I am taking damage " + this.gameObject.name);
            if ((this.transform.position - impactPoint).magnitude > DAMAGE_AVOID_DISTANCE)
            {
                //do nothing;
            }
            takeDamage(damage);
        }

        public float getModSpeed()
        {
            return 1.0f;
        }
    }

}
