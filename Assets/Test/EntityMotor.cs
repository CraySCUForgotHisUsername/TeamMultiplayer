using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using System.Collections.Generic;



[RequireComponent(typeof(Rigidbody))]
public class EntityMotor : NetworkBehaviour
{
    const float AIR_SURFING_FORCE = 50.0f;
    const float AIR_SURFING_FORCE_LINEAR_MAX = 0.05f;
    const float JUMP_DELAY = 0.25f;
    const float MINI_JUMP_PUSH_POWER = 0.45f;
    const float JUMP_POWER_HORIZONTAL = 1.0f;

    public delegate float DEL_RETURN_FLOAT();
    public delegate float DEL_GET_SCHALAR(EntityMotor motor);
    public delegate void DEL_ME(EntityMotor motor);
    public delegate void DEL_ENTITY_ME_AVATAR(EntityMotor motor);
    public delegate void DEL_ENTITY_ME(Entity entity, EntityMotor motor);
    public delegate void DEL_MOVE(Entity entity, EntityMotor motor, Avatar avatar, float horizontal, float vertical);
    public delegate void DEL_JUMP(Entity entity, EntityMotor motor, Avatar avatar, float horizontal, float vertical);

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
    //public AvatarManager m_avatarManager;
    //Avatar m_avatar,
    //    m_avatarUsed,
    //    m_avatarFirstPerson,
    //    m_avatarThirdPerson;


    public List<NAction.Action>
        m_actPassive,
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

    public bool 
        isUpdateGravity = true,
        isUpdateMovement = true;
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

    public Vector3 VelocityRelativeDir {
        get
        {
            return m_moveDirection.normalized;
        }
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
        get
        {
            return !m_isInputDelayed;
        }
    }
    public bool IsGrounded
    {
        get
        {
            return m_isTouchingGround;
        }
        set
        {
            m_isTouchingGround = value;
            if (!value)
            {
                m_rigidbody.drag = 0;
                m_rigidbody.angularDrag = 0;

            }
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
    public void resetState() {
        m_isJumpAvailable = true;
        m_isInputDelayed = false;
    }


    bool updateIsGrounded(Transform body)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), -Vector3.up, out hit, 0.15f);
        m_upward = Vector3.up;
        if (hit.transform != null)
        {
            var rotation = Quaternion.Euler(0, body.rotation.eulerAngles.y, 0);
            m_upward = hit.normal;
            m_forward = (Quaternion.FromToRotation(body.up, hit.normal) * body.forward);
            m_right = (Quaternion.FromToRotation(body.up, hit.normal) * body.right);

        }
        else
        {
            m_upward = transform.up;
            m_forward = transform.forward;
            m_right = transform.right;
        }
        m_isTouchingGround = (hit.transform != null);


        m_rigidbody.drag = 20.0f;
        m_rigidbody.angularDrag = 20.0f;
        if (!IsGrounded)
        {
            m_rigidbody.drag = 0.0f;
            m_rigidbody.angularDrag = 0.0f;
            
        }



        return m_isTouchingGround;

    }
    public void updateGravity(Entity entity, float timeElapsed)
    {
        m_rigidbody.AddForce(Vector3.down * entity.Gravity * timeElapsed, ForceMode.Impulse);

    }
    //Run during fixedupdate
    public virtual void updateMovement( Entity entity,   float timeElapsed)
    {
        
        
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
            var velocityDiff = m_velocity - m_rigidbody.velocity;
            //velocityDiff.y =Mathf.Min( 0, velocityDiff.y);
            var force = velocityDiff;// * timeElapsed*15;
            // m_timeRunning += Time.fixedDeltaTime;
            //Debug.Log("MOVING " + m_velocity + " , " + m_rigidbody.velocity +" , " + force + "TIME " + timeElapsed);
            //Debug.Log(force);
            m_rigidbody.AddForce(force, ForceMode.Impulse);
            //m_rigidbody.MovePosition(m_rigidbody.position + m_velocity * timeElapsed);

        }
        //air control
        else if (!IsGrounded)
        {
            // m_timeRunning = 0;
            var bodyVelocity_XZ = m_rigidbody.velocity;
            bodyVelocity_XZ.y = 0;
            //float bodyVelocityMagnitude = bodyVelocity.magnitude;
            var bodyVelocityDirection = bodyVelocity_XZ.normalized;

            var desiredVelocityXZ = new Vector3(m_velocity.x, 0, m_velocity.z);
            var addThisVelocity = (m_velocity - bodyVelocity_XZ);
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

            //Debug.Log("AIR CONTROL " + (addThisVelocity * AIR_SURFING_FORCE_LINEAR_MAX));                                                           //Debug.Log(possibleAccelerationRange + " , " +force);
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
    public virtual void move(Entity entity, Avatar avatar,  float speed, float horizontal, float vertical)
    {
        m_moveDirection.x = horizontal;
        m_moveDirection.z = vertical;
        for (int i = 0; i < m_evntMoves.Count; i++)
        {
            m_evntMoves[i](entity, this, avatar, horizontal, vertical);
        }
        Vector3 direction = (m_right * horizontal + m_forward * vertical).normalized;//.normalized;
        m_velocity = direction * speed; ;
        if (vertical < 0)
            m_velocity *= 0.9f;
    }

    void setJumpAvailable(bool value)
    {
        m_isJumpAvailable = value;
        m_jumpTimeElapsed = 0;

    }

    public virtual void crawlBegin(Entity entity)
    {
        for (int i = 0; i < m_evntCrawlBegin.Count; i++)
        {
            m_evntCrawlBegin[i](entity, this);
        }
    }
    public virtual void crawlEnd(Entity entity)
    {
        for (int i = 0; i < m_evntCrawlEnd.Count; i++)
        {
            m_evntCrawlEnd[i](entity, this);
        }
    }
    public void rotate(Transform trnasformBody, Transform trnasforhHead, float rotateScalar, float horizontal, float vertical)
    {
        trnasformBody.Rotate(new Vector3(0, horizontal, 0) * m_lookSensitivity * rotateScalar);
        trnasforhHead.Rotate(new Vector3(-vertical, 0, 0) * m_lookSensitivity * rotateScalar);
        //m_avatarManager.rotoate(new Vector3(-vertical, 0, 0) * m_lookSensitivity * rotateScalar);
    }
    public void rotateHead(Vector3 velocity)
    {
    }

    public virtual void kFixedUpdate(Transform transform, Entity entity, Avatar avatar, float timeElapsed)
    {
        if (!m_isJumpAvailable)
        {
            m_jumpTimeElapsed += timeElapsed;
            if (m_jumpTimeElapsed > JUMP_DELAY)
            {
                m_isJumpAvailable = true;
                m_jumpTimeElapsed = 0;
            }

        }

        if(isUpdateGravity)updateGravity(entity, timeElapsed);
        //Debug.Log(m_right);
        updateIsGrounded(transform);
        if (isUpdateMovement)
        {
            updateMovement(entity, timeElapsed);
        }

        hprFixedUpdate(entity, avatar,m_actPassive, timeElapsed);
        hprFixedUpdate(entity, avatar,m_actLMB, timeElapsed);
        hprFixedUpdate(entity, avatar,m_actRMB, timeElapsed);
        hprFixedUpdate(entity, avatar,m_actJump, timeElapsed);
        hprFixedUpdate(entity, avatar,m_actR, timeElapsed);
        hprFixedUpdate(entity, avatar,m_actE, timeElapsed);
        hprFixedUpdate(entity, avatar,m_actF, timeElapsed);
        hprFixedUpdate(entity, avatar,m_actShift, timeElapsed);
    }
    // Update is called once per frame
    public virtual void kUpdate(Entity entity, Avatar avatar, float timeElapsed)
    {
        hprUpdate(entity,avatar,m_actPassive, timeElapsed);
        hprUpdate(entity,avatar,m_actLMB, timeElapsed);
        hprUpdate(entity,avatar,m_actRMB, timeElapsed);
        hprUpdate(entity,avatar,m_actJump, timeElapsed);
        hprUpdate(entity,avatar,m_actR, timeElapsed);
        hprUpdate(entity,avatar,m_actE, timeElapsed);
        hprUpdate(entity,avatar,m_actF, timeElapsed);
        hprUpdate(entity, avatar, m_actShift, timeElapsed);
        if (m_isInputDelayed)
        {
            m_timeInputDelay -= timeElapsed;
            if (m_timeInputDelay <= 0)
            {
                m_isInputDelayed = false;
                m_timeInputDelay = 0;
            }
            else
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
    public virtual void jumpBegin(Entity entity, Avatar avatar, float horizontal, float vertical)
    {
   
        hprUse(m_actJump,entity, avatar);
        for (int i = 0; i < m_evntJump.Count; i++)
        {
            m_evntJump[i](entity, this, avatar, horizontal, vertical);
        }

        if (!IsGrounded) return;
        if (m_isJumpAvailable)
        {
            //Debug.Log("JUMP "+Vector3.up * entity.Jump);
            Vector3 force = (Vector3.up * entity.Jump ) - new Vector3(0, Rigidbody.velocity.y,0);
            m_rigidbody.AddForce(force, ForceMode.Impulse);
            m_isTouchingGround = false;
            //Vector3 direction = (m_avatar.transform.right * horizontal + m_avatar.transform.forward * vertical).normalized;//.normalized;//.normalized;
            //m_rigidbody.AddForce(VelocityDirHorizontal * entity.Speed * JUMP_POWER_HORIZONTAL, ForceMode.Impulse);
            
            //Debug.Log("JUMP POWER " + VelocityDirHorizontal * entity.Speed * JUMP_POWER_HORIZONTAL);
            setJumpAvailable(false);
        }

    }
    public virtual void jumpEnd(Entity entity, Avatar avatar)
    {
        for (int i = 0; i < m_evntJumpStop.Count; i++)
        {
            m_evntJumpStop[i](entity, this);
        }
        hprEnd(entity, m_actJump, avatar);

    }
    public virtual void actLMBBegin(Entity entity, Avatar avatar)
    {
        hprUse(m_actLMB, entity, avatar);

    }
    public virtual void actLMBEnd(Entity entity, Avatar avatar)
    {

        hprEnd(entity, m_actLMB, avatar);
    }
    public virtual void actRMBBegin(Entity entity, Avatar avatar)
    {
        hprUse(m_actRMB,entity, avatar);

    }
    public virtual void actRMBEnd(Entity entity, Avatar avatar)
    {

        hprEnd(entity, m_actRMB, avatar);
    }
    /*
    public virtual void actLMBBegin(Entity entity)
    {
        hprUse(entity, m_actLMB);

    }
    public virtual void actLMBEnd(Entity entity)
    {
        hprEnd(entity, m_actLMB);

    }
    
    public virtual void actRBegin(Entity entity)
    {
        hprUse(entity, m_actR);

    }
    public virtual void actREnd(Entity entity)
    {
        hprEnd(entity, m_actR);

    }
    public virtual void actFBegin(Entity entity)
    {
        hprUse(entity, m_actF);
    }
    public virtual void actFEnd(Entity entity)
    {
        hprEnd(entity, m_actF);

    }
  
    
    public virtual void actEBegin(Entity entity)
    {
        hprUse(entity, m_actE);
    }
    public virtual void actEEnd(Entity entity)
    {
        hprEnd(entity, m_actE);

    }
    public virtual void actQBegin(Entity entity, Avatar avatar)
    {
        hprUse(entity, m_actQ),avatar;
    }
    public virtual void actQEnd(Entity entity)
    {
        hprEnd(entity, m_actQ);

    }
     * */
    public virtual void actShiftBegin(Entity entity, Avatar avatar)
    {
        hprUse(m_actShift,entity, avatar);
    }
    public virtual void actShiftEnd(Entity entity,Avatar avatar)
    {
        hprEnd(entity, m_actShift, avatar);

    }
    

    public Vector3 dirLook
    {
        get
        {
            return Vector3.forward;
        }
    }




    void hprFixedUpdate(Entity entity, Avatar avatar, List<NAction.Action> actions, float timeElapsed)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].kFixedUpdate(entity,this, avatar, timeElapsed);
        }
    }
    void hprUpdate(Entity entity, Avatar avatar, List<NAction.Action> actions, float timeElapsed)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].kUpdate(entity, this, avatar, timeElapsed);
        }
    }
    void hprUse( List<NAction.Action> actions, Entity entity, Avatar avatar)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].use(entity, this, avatar);
        }

    }
    void hprEnd(Entity entity, List<NAction.Action> actions, Avatar avatar)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].end(entity, this, avatar);
        }

    }



}