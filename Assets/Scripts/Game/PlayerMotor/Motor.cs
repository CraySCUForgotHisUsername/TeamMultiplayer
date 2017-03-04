using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace NMotor {

    //Weak delay can be overriden by using other ability.
    //Strong delay cannot be overriden by using other ability.
    //public enum DELAY_TYPE { WEAK,STRONG};
    [RequireComponent(typeof(Rigidbody))]
    public class Motor : NetworkBehaviour
    {
        const float AIR_SURFING_FORCE = 50.0f;
        const float AIR_SURFING_FORCE_LINEAR_MAX = 0.05f;
        const float JUMP_DELAY = 0.25f;
        const float MINI_JUMP_PUSH_POWER = 0.45f;
        const float JUMP_POWER_HORIZONTAL = 1.0f;

        public delegate float DEL_RETURN_FLOAT();
        public delegate float DEL_GET_SCHALAR(Motor motor);
        public delegate void DEL_ME(Motor motor);
        public delegate void DEL_ENTITY_ME(NEntity.Entity entity, Motor motor);
        public delegate void DEL_MOVE(NEntity.Entity entity, Motor motor, float horizontal, float vertical);
        public delegate void DEL_JUMP(NEntity.Entity entity, Motor motor, float horizontal, float vertical);

        public List<DEL_GET_SCHALAR> m_scalarsSpeed = new List<DEL_GET_SCHALAR>();
        public List<DEL_MOVE> m_evntMoves = new List<DEL_MOVE>();
        public List<DEL_JUMP> m_evntJump = new List<DEL_JUMP>();
        public List<DEL_ENTITY_ME> m_evntJumpStop = new List<DEL_ENTITY_ME>();
        public List<DEL_ENTITY_ME> m_evntCrawlBegin = new List<DEL_ENTITY_ME>();
        public List<DEL_ENTITY_ME> m_evntCrawlEnd = new List<DEL_ENTITY_ME>();

        [SerializeField]
        string m_playerName = "PlayerNameHere";
        [SerializeField]
        float
            m_lookSensitivity = 100.0f;
        public GameData.PlayerInfo m_playerInfo = new GameData.PlayerInfo();
        public AvatarManager m_avatarManager;
        //Avatar m_avatar,
        //    m_avatarUsed,
        //    m_avatarFirstPerson,
        //    m_avatarThirdPerson;

       
        public List<Action>
            m_actJump,
            m_actLMB,
            m_actRMB,
            m_actR,
            m_actF,
            m_actShift,
            m_actE,
            m_actQ;

        private Vector3
            m_moveDirection = Vector3.zero,
            m_velocity = Vector3.zero,
            m_rotation = Vector3.zero,
            m_rotationFace = Vector3.zero;

        public bool isUpdateMovement = true;
        bool m_isJumpAvailable = false;
        float m_jumpTimeElapsed = 0;
        Rigidbody m_rigidbody;


        Vector3 m_forward = Vector3.zero,
            m_right = Vector3.zero,
            m_upward = Vector3.up;
        bool wasGrounded = true;
        bool m_isTouchingGround = false;

        bool m_isInputDelayed = true;
        float m_timeInputDelay = 0;
        
        public Rigidbody Rigidbody
        {
            get { return m_rigidbody; }
        }
    

        public Vector3 Velocity
        {
            get
            {
                return m_velocity;

            }
        }
        public Vector3 VelocityDirHorizontal
        {
            get
            {
                return new Vector3(m_velocity.x, 0, m_velocity.z).normalized;

            }
        }
        public bool IsReadyForInput
        {
            get{
                return !m_isInputDelayed;
            }
        }
        public bool IsGrounded {
            get
            {
                return m_isTouchingGround;
            }
        }
        
        // Use this for initialization
        private void Awake()
        {
        }
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();

        }
        bool updateIsGrounded()
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), -Vector3.up, out hit, 0.2f);
            m_upward = Vector3.up;
            if (hit.transform != null)
            {
                var rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
                m_upward = hit.normal;
                m_forward = (Quaternion.FromToRotation(this.transform.up, hit.normal) * this.transform.forward);
                m_right = (Quaternion.FromToRotation(this.transform.up, hit.normal) * this.transform.right);
                //m_right = (Quaternion.LookRotation(hit.normal) * this.tran.up);

            }else
            {
                m_upward = transform.up;
                m_forward = transform.forward;
                m_right = transform.right;
            }
            m_isTouchingGround = (hit.transform != null);
            return m_isTouchingGround;
            
        }
        //Run during fixedupdate
        public virtual void updateMovement(NEntity.Entity entity, float timeElapsed)
        {
            //Debug.Log("GRAVITY " + (-m_upward * entity.Gravity * timeElapsed));
            m_rigidbody.AddForce(-m_upward * entity.Gravity  * timeElapsed ,ForceMode.Impulse);
            m_rigidbody.drag = 10.0f;
            m_rigidbody.angularDrag = 10.0f;
            if (!IsGrounded)
            {
                m_rigidbody.drag = 0.0f;
                m_rigidbody.angularDrag = 0.0f;
                //Debug.Log(isGrounded);
                if (wasGrounded && m_isJumpAvailable)
                {
                    //Vector3 velocityXZ = new Vector3(m_velocity.x, 0, m_velocity.z).normalized;
                    //Vector3 dir = m_velocity.normalized
                    //Debug.Log("OK JUMP!" + velocityXZ * 100);

                    //float ratio =Mathf.Max(0.1f+ Mathf.Min(1, m_timeRunning / 0.5f));
                    m_rigidbody.AddForce(VelocityDirHorizontal * entity.Speed * MINI_JUMP_PUSH_POWER, ForceMode.Impulse);

                }
            }
            else
            {

            }

            wasGrounded = IsGrounded;
            if (IsGrounded)
            {
                //entity should restore the air control
            }
            

            if (m_rotation != Vector3.zero)
                m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(m_rotation * Time.fixedDeltaTime));
            if (m_velocity == Vector3.zero)
            {
                // m_timeRunning = 0;
                return;
            }
            //land control

            if (IsGrounded)
            {
                // m_timeRunning += Time.fixedDeltaTime;
                //Debug.Log("MOVING " + Time.time);
                m_rigidbody.MovePosition(m_rigidbody.position + m_velocity * Time.fixedDeltaTime);

            }
            //air control
            if (!IsGrounded)
            {
                // m_timeRunning = 0;
                var bodyVelocity_XZ = m_rigidbody.velocity;
                bodyVelocity_XZ.y = 0;
                //float bodyVelocityMagnitude = bodyVelocity.magnitude;
                var bodyVelocityDirection = bodyVelocity_XZ.normalized;

                var desiredVelocityXZ = new Vector3(m_velocity.x, 0, m_velocity.z);
                var addThisVelocity = (m_velocity- bodyVelocity_XZ);
                var desiredVelocityDirection = (desiredVelocityXZ - m_velocity).normalized;
                //Debug.Log("desired to " + desiredVelocityDirection);
                //float ratioDifference = 1-Vector3.Dot(bodyVelocityDirection, desiredVelocityDirection);

                //float possibleAccelerationRange = 1 - Mathf.Max(-1, Mathf.Min(1, Vector3.Dot(bodyVelocity_XZ, desiredVelocityDirection)));

                /* Don't need these I do it better way
                float possibleAccelerationRange = Mathf.Max(-1, Mathf.Min(1,
                    Vector3.Dot(bodyVelocity_XZ, desiredVelocityDirection) / desiredVelocityDirection.magnitude));
                possibleAccelerationRange *= -1;
                possibleAccelerationRange += 1;
                possibleAccelerationRange /= 2.0f;
                 * */
                float possibleAceelerationDueToSpeedDiff = Mathf.Min(
                    (desiredVelocityXZ.magnitude -
                    Vector3.Dot(bodyVelocity_XZ, desiredVelocityDirection)) / desiredVelocityXZ.magnitude, 1.0f);
                //Debug.Log(possibleAceelerationDueToSpeedDiff + "," + desiredVelocityXZ);
                //possibleAccelerationRange = Mathf.Min( Mathf.Max(0, possibleAccelerationRange * 2.0f),1.0f);
                //Debug.Log();
                //possibleAccelerationRange = 1.0f;
                //var directionCorrect = desiredVelocityDirection - bodyVelocityDirection;
                //directionCorrect.Normalize();
                //possibleAccelerationRange = Mathf.Max(possibleAceelerationDueToSpeedDiff * AIR_SURFING_FORCE_LINEAR_MAX, possibleAccelerationRange);
                //possibleAccelerationRange = possibleAceelerationDueToSpeedDiff;// * AIR_SURFING_FORCE_LINEAR_MAX;
                Vector3 force = desiredVelocityXZ * possibleAceelerationDueToSpeedDiff;// *  Mathf.Min(1.0f, 100.0f * timeElapsed);
                //Debug.Log(possibleAccelerationRange + " , " +force);
                //Debug.Log(m_velocity + " , " + possibleAccelerationRange + " , " +  force);
                m_rigidbody.AddForce(addThisVelocity * AIR_SURFING_FORCE_LINEAR_MAX, ForceMode.Impulse);
                //m_rigidbody.AddForce(force * entity.AirControl, ForceMode.Impulse);
            }

            //if (m_rotationFace != Vector3.zero)
            //    m_head.transform.Rotate(-m_rotationFace * Time.fixedDeltaTime);
        }
        [ClientRpc]
        public void RpcSetPlayerTeam(int team)
        {
            this.m_playerInfo.team = (GameData.TEAM)team;
        }
        public virtual void move(NEntity.Entity entity, float speed, float horizontal, float vertical)
        {
            m_moveDirection.x = horizontal;
            m_moveDirection.z = vertical;
            for (int i = 0; i < m_evntMoves.Count; i++)
            {
                m_evntMoves[i](entity,this, horizontal, vertical);
            }
            Vector3 direction = (m_right * horizontal + m_forward * vertical).normalized;//.normalized;
            m_velocity = direction * speed; ;
            if (vertical < 0)
                m_velocity *= 0.9f;
        }
    
        void setJumpAvailable(bool value) {
            m_isJumpAvailable = value;
            m_jumpTimeElapsed = 0;

        }

        public virtual void crawlBegin(NEntity.Entity entity)
        {
            for (int i = 0; i < m_evntCrawlBegin.Count; i++)
            {
                m_evntCrawlBegin[i](entity,this);
            }
        }
        public virtual void crawlEnd(NEntity.Entity entity)
        {
            for (int i = 0; i < m_evntCrawlEnd.Count; i++)
            {
                m_evntCrawlEnd[i](entity, this);
            }
        }
        public void rotate(float rotateScalar, float horizontal, float vertical)
        {
            transform.Rotate(new Vector3(0, horizontal, 0) * m_lookSensitivity* rotateScalar);
            m_avatarManager.rotoate(new Vector3(-vertical, 0, 0) * m_lookSensitivity* rotateScalar);
        }
        public void rotateHead(Vector3 velocity)
        {
        }
        
        public virtual void kFixedUpdate(NEntity.Entity entity, float timeElapsed)
        {
            if (!m_isJumpAvailable) {
                m_jumpTimeElapsed += timeElapsed;
                if (m_jumpTimeElapsed > JUMP_DELAY) {
                    m_isJumpAvailable = true;
                    m_jumpTimeElapsed = 0;
                }

            }

            //Debug.Log(m_right);
            if (isUpdateMovement)
            {
                updateIsGrounded();
                updateMovement(entity, timeElapsed);
            }

            hprFixedUpdate(entity, m_actLMB, timeElapsed);
            hprFixedUpdate(entity,m_actRMB, timeElapsed);
            hprFixedUpdate(entity, m_actJump, timeElapsed);
            hprFixedUpdate(entity, m_actR, timeElapsed);
            hprFixedUpdate(entity, m_actE, timeElapsed);
            hprFixedUpdate(entity,m_actF, timeElapsed);
            hprFixedUpdate(entity,m_actShift, timeElapsed);
        }
        // Update is called once per frame
        public virtual void kUpdate(NEntity.Entity entity, float timeElapsed)
        {
            hprUpdate(entity, m_actLMB, timeElapsed);
            hprUpdate(entity, m_actRMB, timeElapsed);
            hprUpdate(entity, m_actJump, timeElapsed);
            hprUpdate(entity, m_actR, timeElapsed);
            hprUpdate(entity, m_actE, timeElapsed);
            hprUpdate(entity, m_actF, timeElapsed);
            hprUpdate(entity, m_actShift, timeElapsed);
            if (m_isInputDelayed)
            {
                m_timeInputDelay -= timeElapsed;
                if(m_timeInputDelay <= 0)
                {
                    m_isInputDelayed = false;
                    m_timeInputDelay = 0;
                }else
                {
                    return;
                }

            }
        }
        public virtual void setDelay(float time)
        {
            m_isInputDelayed = true;
            m_timeInputDelay = Mathf.Max(time, m_timeInputDelay);
        }
        public virtual void jumpBegin(NEntity.Entity entity, float horizontal, float vertical)
        {
            hprUse(entity, m_actJump);
            for (int i = 0; i < m_evntJump.Count; i++)
            {
                m_evntJump[i](entity, this, horizontal, vertical);
            }

            if (!IsGrounded) return;
            if (m_isJumpAvailable)
            {
                m_rigidbody.AddForce(m_avatarManager.Up * entity.Jump,ForceMode.Impulse);
                //Vector3 direction = (m_avatar.transform.right * horizontal + m_avatar.transform.forward * vertical).normalized;//.normalized;//.normalized;
                m_rigidbody.AddForce(VelocityDirHorizontal * entity.Speed * JUMP_POWER_HORIZONTAL, ForceMode.Impulse);
                setJumpAvailable(false);
            }
            
        }
        public virtual void jumpEnd(NEntity.Entity entity)
        {
            for (int i = 0; i < m_evntJumpStop.Count; i++)
            {
                m_evntJumpStop[i](entity, this);
            }
            hprEnd(entity, m_actJump);

        }
        public virtual void actLMBBegin(NEntity.Entity entity)
        {
            hprUse(entity,m_actLMB);

        }
        public virtual void actLMBEnd(NEntity.Entity entity)
        {
            hprEnd(entity, m_actLMB);

        }
        public virtual void actRMBBegin(NEntity.Entity entity)
        {
            hprUse(entity, m_actRMB);

        }
        public virtual void actRMBEnd(NEntity.Entity entity)
        {
            hprEnd(entity, m_actRMB);
        }
        public virtual void actRBegin(NEntity.Entity entity)
        {
            hprUse(entity, m_actR);

        }
        public virtual void actREnd(NEntity.Entity entity)
        {
            hprEnd(entity, m_actR);

        }
        public virtual void actFBegin(NEntity.Entity entity)
        {
            hprUse(entity, m_actF);
        }
        public virtual void actFEnd(NEntity.Entity entity)
        {
            hprEnd(entity, m_actF);

        }
        public virtual void actShiftBegin(NEntity.Entity entity)
        {
            hprUse(entity, m_actShift);
        }
        public virtual void actShiftEnd(NEntity.Entity entity)
        {
            hprEnd(entity, m_actShift);

        }
        public virtual void actEBegin(NEntity.Entity entity)
        {
            hprUse(entity, m_actE);
        }
        public virtual void actEEnd(NEntity.Entity entity)
        {
            hprEnd(entity, m_actE);

        }
        public virtual void actQBegin(NEntity.Entity entity)
        {
            hprUse(entity, m_actQ);
        }
        public virtual void actQEnd(NEntity.Entity entity)
        {
            hprEnd(entity, m_actQ);

        }

        public Vector3 dirLook
        {
            get
            {
                return m_avatarManager.Forward;
            }
        }

        
       
       
        void hprFixedUpdate(NEntity.Entity entity, List<Action> actions, float timeElapsed)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].kFixedUpdate(entity,this, timeElapsed);
            }
        }
        void hprUpdate(NEntity.Entity entity, List<Action> actions, float timeElapsed)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].kUpdate(entity,this, timeElapsed);
            }
        }
        void hprUse(NEntity.Entity entity, List<Action> actions)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].use(entity,this);
            }

        }
        void hprEnd(NEntity.Entity entity, List<Action> actions)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].end(entity,this);
            }

        }



    }

}
