using System.Collections;
using System.Collections.Generic;
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
        float shield = 0.0f; //this is the type of health that regenerates
        [SyncVar]
        [SerializeField]
        float modOffense = 0.0f; //Bonus damage or reduced damage and such
        [SyncVar]
        [SerializeField]
        float modDefense = 0.0f; //Reduced damage or increased damage taken and such
        [SyncVar]
        [SerializeField]
        float modSpeed = 0.0f;   //Increased speed or decreased speed 


        public List<DEL_MODIFIER> scalarOffense = new List<DEL_MODIFIER>();
        public List<DEL_MODIFIER> scalarDefense = new List<DEL_MODIFIER>();
        public List<DEL_MODIFIER> scalarSpeed = new List<DEL_MODIFIER>();
        public float Health { get { return health; } }
        
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
