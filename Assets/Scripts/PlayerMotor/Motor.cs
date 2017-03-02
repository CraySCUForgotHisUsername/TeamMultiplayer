using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;


namespace NMotor {

    [RequireComponent(typeof(NEntity.Entity))]
    [RequireComponent(typeof(Rigidbody))]
    public class Motor : NetworkBehaviour
    {
        public delegate void DEL_ME(Motor motor);
        public delegate void DEL_MOVE(Motor motor, float horizontal, float vertical);
        public List<DEL_MOVE> m_evntMoves = new List<DEL_MOVE>();
        public List<DEL_MOVE> m_evntJump = new List<DEL_MOVE>();
        public List<DEL_ME> m_evntCrawl = new List<DEL_ME>();
        public List<DEL_ME> m_evntJumpStop = new List<DEL_ME>();
        public List<DEL_ME> m_evntCrawlStop = new List<DEL_ME>();

        public Avatar
            PREFAB_AVATAR,
            PREFAB_FIRSTPERSON, PREFAB_THIRDPERSON;
        public NEntity.Entity m_entity;
        public GameData.PlayerInfo m_playerInfo = new GameData.PlayerInfo();
        [SerializeField]
        Avatar m_avatar,
            m_avatarUsed,
            m_avatarFirstPerson,
            m_avatarThirdPerson;

        //public Transform m_head,m_weapon;
        public Action
            m_actJump;
        public Action
            m_action1,
            m_action2,
            m_action3,
            m_action4,
            m_action5;
        [SerializeField]
        private float
            m_gravity = 1.0f,
            m_speed = 10.0f,
            m_jumpPower = 300.0f,
            m_airControl = 1.0f,
            m_lookSensitivity = 100.0f;

        private Vector3
            m_moveDirection = Vector3.zero,
            m_velocity = Vector3.zero,
            m_velocityOld = Vector3.zero,
            m_rotation = Vector3.zero,
            m_rotationFace = Vector3.zero;

        public bool isUpdateMovement = true;
        Rigidbody m_rigidbody;


        Vector3 m_forward = Vector3.zero,
            m_right = Vector3.zero,
            m_upward = Vector3.up;
        bool wasGrounded = true;
        float m_timeRunning = 0;
        
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
        // Use this for initialization
        private void Awake()
        {
            m_avatar = Instantiate<Avatar>(PREFAB_AVATAR);
            m_avatar.transform.SetParent(this.transform);
            m_avatar.transform.localPosition = Vector3.zero;
            m_avatar.transform.localRotation = Quaternion.identity;



            m_avatarFirstPerson = Instantiate<Avatar>(PREFAB_FIRSTPERSON);
            m_avatarThirdPerson = Instantiate<Avatar>(PREFAB_THIRDPERSON);
            m_avatarFirstPerson.gameObject.SetActive(false);
            m_avatarThirdPerson.gameObject.SetActive(false);

            m_avatarFirstPerson.transform.SetParent(this.transform);
            m_avatarThirdPerson.transform.SetParent(this.transform);

            m_avatarFirstPerson.transform.localPosition = Vector3.zero;
            m_avatarThirdPerson.transform.localPosition = Vector3.zero;
            m_avatarFirstPerson.transform.localRotation = Quaternion.identity;
            m_avatarThirdPerson.transform.localRotation = Quaternion.identity;
        }
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_entity    = GetComponent<NEntity.Entity>();

        }
        void FixedUpdate()
        {
            m_velocity = (m_forward * m_moveDirection.z + m_right * m_moveDirection.x ).normalized * m_speed;
            //Debug.Log(m_right);
            if (isUpdateMovement)updateMovement(Time.fixedDeltaTime);
            //m_rigidbody.AddForce( new Vector3(0,-100 *Time.fixedDeltaTime,0) );
        }
        bool updateIsGrounded()
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), - Vector3.up,out  hit, 0.2f);
            m_upward = Vector3.up;
            if (hit.transform != null)
            {
                var rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
                m_upward = hit.normal;
                m_forward = (Quaternion.FromToRotation(this.transform.up, hit.normal) * this.transform.forward);
                m_right = (Quaternion.FromToRotation(this.transform.up, hit.normal) * this.transform.right);
                //m_right = (Quaternion.LookRotation(hit.normal) * this.tran.up);

            }
            return hit.transform != null;
            //return Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), hit - Vector3.up, 0.15f);
            /*
                Physics.Raycast(transform.position + new Vector3(0.2f, 0.01f, 0), -Vector3.up, 0.02f) ||

                Physics.Raycast(transform.position + new Vector3(-0.2f, 0.01f, 0), -Vector3.up, 0.02f) ||
                Physics.Raycast(transform.position + new Vector3(0, 0.01f, 0.2f), -Vector3.up, 0.02f) ||
                Physics.Raycast(transform.position + new Vector3(0, 0.01f, -0.2f), -Vector3.up, 0.02f);
             **/
        }
        //Run during fixedupdate
        public virtual void updateMovement(float timeElapsed)
        {
            bool isGrounded = updateIsGrounded();
            m_rigidbody.AddForce(-m_upward * m_gravity*800.0f *timeElapsed);
            m_rigidbody.drag = 10.0f;
            m_rigidbody.angularDrag = 10.0f;
            if (!isGrounded)
            {
                m_rigidbody.drag = 0.0f;
                m_rigidbody.angularDrag = 0.0f;
                //Debug.Log(isGrounded);
                if (wasGrounded)
                {
                    //Vector3 dir = m_velocity.normalized
                    Debug.Log("OK JUMP!" + this.m_velocity * 100);

                    //float ratio =Mathf.Max(0.1f+ Mathf.Min(1, m_timeRunning / 0.5f));
                    m_rigidbody.AddForce(this.m_velocity * 100 );

                }
            }
            else
            {

            }

            wasGrounded = isGrounded;
            if (isGrounded)
            {
                //m_rigidbody.velocity *= 0.0f;
                //m_rigidbody.velocity -= m_rigidbody.velocity * 0.99f * Mathf.Max(timeElapsed*3, 1.0f);// Vector3.zero;
                //if (m_rigidbody.velocity.magnitude < 0.1) m_rigidbody.velocity = Vector3.zero;
                m_airControl += 1.0f * Time.fixedDeltaTime;
            }
            m_airControl = Mathf.Max(0, Mathf.Min(m_airControl, 1.0f));



            m_velocityOld = m_velocity;

            if (m_rotation != Vector3.zero)
                m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(m_rotation * Time.fixedDeltaTime));
            if (m_velocity == Vector3.zero)
            {
               // m_timeRunning = 0;
                return;
            }
            //land control
            
            if (isGrounded )
            {
               // m_timeRunning += Time.fixedDeltaTime;
                //Debug.Log("MOVING " + Time.time);
                m_rigidbody.MovePosition(m_rigidbody.position + m_velocity * Time.fixedDeltaTime);
                //   m_rigidbody.AddForce(m_velocity * Time.fixedDeltaTime * m_airControl*100.0f);

            }
            //air control
            if (!isGrounded )
            {
               // m_timeRunning = 0;
                var bodyVelocity_XZ = m_rigidbody.velocity;
                bodyVelocity_XZ.y = 0;
                //float bodyVelocityMagnitude = bodyVelocity.magnitude;
                var bodyVelocityDirection = bodyVelocity_XZ.normalized;
              
                var desiredVelocityDirection = m_velocity.normalized;
                //float ratioDifference = 1-Vector3.Dot(bodyVelocityDirection, desiredVelocityDirection);

                float possibleAccelerationRange = 1 -  Mathf.Max(-1, Mathf.Min(1, Vector3.Dot(bodyVelocity_XZ,  desiredVelocityDirection) )) ;

                //Debug.Log();
                //possibleAccelerationRange = 1.0f;
                //var directionCorrect = desiredVelocityDirection - bodyVelocityDirection;
                //directionCorrect.Normalize();

                Vector3 force = m_velocity * possibleAccelerationRange *  200.0f * timeElapsed;
                
                m_rigidbody.AddForce(force * m_airControl);
            }

            //if (m_rotationFace != Vector3.zero)
            //    m_head.transform.Rotate(-m_rotationFace * Time.fixedDeltaTime);
        }
        [ClientRpc]
        public void RpcSetPlayerTeam(int team)
        {
            this.m_playerInfo.team = (GameData.TEAM)team;
        }
        public virtual void move(float horizontal, float vertical)
        {
            m_moveDirection.x = horizontal;
            m_moveDirection.z = vertical;
            for (int i = 0; i < m_evntMoves.Count; i++)
            {
                m_evntMoves[i](this,horizontal, vertical);
            }
            Vector3 direction = (m_avatar.transform.right * horizontal + m_avatar.transform.forward * vertical).normalized;//.normalized;
            m_velocity = direction * m_speed; ;
            if (vertical > 0)
                m_velocity *= 0.9f;
        }
        public virtual void jump(float horizontal, float vertical)
        {
            for (int i = 0; i < m_evntJump.Count; i++)
            {
                m_evntJump[i](this, horizontal, vertical);
            }

            if (!updateIsGrounded()) return;

            Vector3 direction = ( m_avatar.transform.right * horizontal + m_avatar.transform.forward * vertical).normalized;//.normalized;//.normalized;
            m_rigidbody.AddForce(m_avatar.transform.up * m_jumpPower);
            //m_rigidbody.AddForce(direction * m_speed * 40.0f);

        }
        public virtual void jumpEnd()
        {
            for (int i = 0; i < m_evntJumpStop.Count; i++)
            {
                m_evntJumpStop[i](this);
            }

        }
        public virtual void crawl()
        {
            for (int i = 0; i < m_evntCrawl.Count; i++)
            {
                m_evntCrawl[i](this);
            }


        }
        public void rotate(float horizontal, float vertical)
        {
            transform.Rotate(new Vector3(0, horizontal, 0) * m_lookSensitivity);
            m_avatar.m_head.transform.Rotate(new Vector3(-vertical, 0, 0) * m_lookSensitivity);
        }
        public void rotateHead(Vector3 velocity)
        {
        }
        public virtual void kFixedUpdate()
        {

            if (m_actJump != null)
                m_actJump.kFixedUpdate(this, Time.fixedDeltaTime);
            if (m_action1 != null)
                m_action1.kFixedUpdate(this, Time.fixedDeltaTime);
            if (m_action2 != null)
                m_action2.kFixedUpdate(this, Time.fixedDeltaTime);
            if (m_action3 != null)
                m_action3.kFixedUpdate(this, Time.fixedDeltaTime);
            if (m_action4 != null)
                m_action4.kFixedUpdate(this, Time.fixedDeltaTime);
            if (m_action5 != null)
                m_action5.kFixedUpdate(this, Time.fixedDeltaTime);
        }
        // Update is called once per frame
        public virtual void kUpdate()
        {
            if (m_actJump != null)
                m_actJump.kUpdate(this, Time.deltaTime);
            if (m_action1 != null)
                m_action1.kUpdate(this, Time.deltaTime);
            if (m_action2 != null)
                m_action2.kUpdate(this, Time.deltaTime);
            if (m_action3 != null)
                m_action3.kUpdate(this, Time.deltaTime);
            if (m_action4 != null)
                m_action4.kUpdate(this, Time.deltaTime);
            //m_action2.kUpdate(this, Time.deltaTime);
            //m_action1.update(this, Time.deltaTime);
            //m_action1.update(this, Time.deltaTime);
            //m_action1.update(this, Time.deltaTime);
            //m_action1.update(this, Time.deltaTime);
        }
        public Vector3 dirLook
        {
            get
            {
                return m_avatar.m_head.transform.forward;
            }
        }

        public void addToHead(Transform transform)
        {
            transform.SetParent(m_avatar.m_head.transform);
        }
        public void addToBody(Transform transform)
        {

            transform.SetParent(m_avatar.m_body.transform);
        }
        void addAvatar(Avatar avatar)
        {
            avatar.gameObject.SetActive(true);
            addToHead(avatar.m_head.transform);
            avatar.m_head.transform.localPosition = Vector3.zero;
            //avatar.m_head.transform.localRotation = Quaternion.identity;
            addToBody(avatar.m_body.transform);
            avatar.m_body.transform.localPosition = Vector3.zero;
            //avatar.m_body.transform.localRotation = Quaternion.identity;
            m_avatarUsed = avatar;

        }
        public void setAvatar(bool isLocal)
        {
            if (isLocal)
            {
                addAvatar(m_avatarFirstPerson);
            }
            else
            {
                addAvatar(m_avatarThirdPerson);
            }
        }
        public Avatar getAvatar()
        {
            return m_avatarUsed;
        }
        public Avatar getCollisionAvatar()
        {
            return m_avatar;
        }
        public Quaternion getHeadRotation()
        {
            return m_avatar.m_head.transform.rotation;
        }
        public void setHeadRotation(Quaternion rotation)
        {
            m_avatar.m_head.transform.rotation = rotation;
        }
       



    }

}
