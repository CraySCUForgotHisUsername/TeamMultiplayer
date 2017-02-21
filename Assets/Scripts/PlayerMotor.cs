using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : NetworkBehaviour {
    public Avatar 
        PREFAB_AVATAR,
        PREFAB_FIRSTPERSON, PREFAB_THIRDPERSON;
    public GameData.PlayerInfo m_playerInfo = new GameData.PlayerInfo();
    [SerializeField]
    Avatar m_avatar,
        m_avatarUsed,
        m_avatarFirstPerson,
        m_avatarThirdPerson;

    //public Transform m_head,m_weapon;
    public Action
        m_action1,
        m_action2,
        m_action3,
        m_action4,
        m_action5;
    [SerializeField]
    private float
        m_speed = 10.0f,
        m_lookSensitivity = 100.0f;

    private Vector3
        m_velocity = Vector3.zero,
        m_rotation = Vector3.zero,
        m_rotationFace = Vector3.zero;


    Rigidbody m_rigidbody;
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
    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();

	}
    void FixedUpdate()
    {
        updateMovement();
    }
    //Run during fixedupdate
    void updateMovement()
    {
        if (m_velocity != Vector3.zero)
            m_rigidbody.MovePosition(m_rigidbody.position + m_velocity * Time.fixedDeltaTime);
        if (m_rotation != Vector3.zero)
            m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(m_rotation * Time.fixedDeltaTime));
        //if (m_rotationFace != Vector3.zero)
        //    m_head.transform.Rotate(-m_rotationFace * Time.fixedDeltaTime);
    }
    [ClientRpc]
    public void RpcSetPlayerTeam(int team)
    {
        this.m_playerInfo.team = (GameData.TEAM)team;
    }
    public void move(float horizontal, float vertical)
    {
        Vector3 direction = (m_avatar.transform.right* horizontal+ m_avatar.transform.forward * vertical).normalized;//.normalized;
        m_velocity = direction * m_speed; ;
    }
    public void rotate(float horizontal, float vertical)
    {
        transform.Rotate(new Vector3(0, horizontal, 0) * m_lookSensitivity);
        m_avatar.m_head.transform.Rotate(new Vector3(-vertical, 0, 0)* m_lookSensitivity);
    }
    public void rotateHead(Vector3 velocity)
    {
    }

    // Update is called once per frame
    public void kUpdate ()
    {
        m_action1.kUpdate(this, Time.deltaTime);
        //m_action2.kUpdate(this, Time.deltaTime);
        //m_action1.update(this, Time.deltaTime);
        //m_action1.update(this, Time.deltaTime);
        //m_action1.update(this, Time.deltaTime);
        //m_action1.update(this, Time.deltaTime);
    }
    public Vector3 dirLook {
        get
        {
            return m_avatar.m_head.transform.forward;
        }
    }
  
    public void addToHead(Transform transform)
    {
        transform.SetParent( m_avatar.m_head.transform);
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
    public Quaternion getHeadRotation()
    {
        return m_avatar.m_head.transform.rotation;
    }
    public void setHeadRotation(Quaternion rotation)
    {
        m_avatar.m_head.transform.rotation = rotation;
    }
}
