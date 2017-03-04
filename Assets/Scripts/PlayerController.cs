
 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PlayerController : NetworkBehaviour {


    [SerializeField]
    GameData.TEAM m_team;
    GameData.HERO m_hero;
    [SerializeField]
    public GameObject m_eye;
    public NEntity.Entity m_entity;
    public NMotor.Motor m_motor;

    [SyncVar]
    public Quaternion headRotation = new Quaternion();
	// Use this for initialization
	void Start () {

    }
    public override void OnStartLocalPlayer()
    {
        //onStart();
    }
    public override void OnStartAuthority()
    {
        onStart();
    }

    public override void OnStartClient()
    {
       // onStart();
    }
    public override void OnStartServer()
    {
       // onStart();
    }
    void onStart()
    {
        Debug.Log("onStart " + isLocalPlayer);
        gameObject.name = "OtherPlayer";
        if (!hasAuthority) return;
        Debug.Log("OnStartLocalPlayer");
        gameObject.name = "LocalPlayer";
        m_eye.SetActive(true);
       // m_motor.setAvatar(isLocalPlayer);

    }
    public void link(NEntity.Entity entity, NMotor.Motor motor)
    {
        m_entity = entity;
        m_motor = motor;
        m_motor.m_avatarManager.setAvatar(entity, m_team, hasAuthority);
        motor.m_avatarManager.addToHead(m_eye.transform);
        m_eye.transform.localPosition = Vector3.zero;

        if (!hasAuthority) return;
        NUI.Game.ME.link(m_team, m_hero, entity);
    }
    [ClientRpc]
    public void RpcLink(NetworkInstanceId id)
    {
        var obj = ClientScene.FindLocalObject(id);
        link(obj.GetComponent<NEntity.Entity>(), obj.GetComponent<NMotor.Motor>());
       // m_motor = motor;
       // m_eye.transform.parent = m_motor.m_head.transform;
       // m_eye.transform.localPosition = Vector3.zero;
    }


    [ClientRpc]
    public void RpcLinkInformation(GameData.TEAM myTeam, GameData.HERO myHero)
    {
        m_team = myTeam;
        m_hero = myHero;
    }
    [TargetRpc]
    public void TargetLink(NetworkConnection target, GameData.TEAM team, GameData.HERO hero, NetworkInstanceId id)
    {
        var obj = ClientScene.FindLocalObject(id);
        var entity = obj.GetComponent<NEntity.Entity>();
        var motor = obj.GetComponent<NMotor.Motor>();
        m_team = team;
        m_hero = hero;
        link(entity,motor);
    }
    [Command]
    public void CmdSetHeadRotation(float x, float y, float z)
    {
        this.headRotation = Quaternion.Euler(x, y, z);
    }
    private void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }
        m_motor.kFixedUpdate(m_entity, Time.fixedDeltaTime);

    }


    // Update is called once per frame
    void Update () {
        //    Debug.Log(hasAuthority);
        if (!hasAuthority)
        {

            m_motor.m_avatarManager.HeadRotation =  headRotation;
            return;
        }
        updateMotorInput();
        m_entity.kUpdate(Time.deltaTime);
        m_motor.kUpdate(m_entity, Time.deltaTime);
    }
    public virtual void updateMotorInput()
    {
        
        m_motor.move(m_entity, m_entity.Speed,Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_motor.rotate(m_entity.RotationSpeed, Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        headRotation = m_motor.m_avatarManager.HeadRotation;
        CmdSetHeadRotation(headRotation.eulerAngles.x, headRotation.eulerAngles.y, headRotation.eulerAngles.z);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_motor.jumpBegin(m_entity, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            m_motor.jumpEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_motor.crawlBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            m_motor.crawlEnd(m_entity);
        }

        if (!m_motor.IsReadyForInput) return;
        if (Input.GetMouseButton(0)){
            m_motor.actLMBBegin(m_entity);
        }
        else if (Input.GetMouseButtonUp(0)) {
            m_motor.actRMBEnd(m_entity);
        }
        if (Input.GetMouseButton(1)){
            m_motor.actRMBBegin(m_entity);
        }
        else if (Input.GetMouseButtonUp(1)){
            m_motor.actRMBEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_motor.actRBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            m_motor.actREnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_motor.actShiftBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_motor.actShiftEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_motor.actEBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            m_motor.actEEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            m_motor.actFBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            m_motor.actFEnd(m_entity);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_motor.actQBegin(m_entity);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            m_motor.actQEnd(m_entity);
        }
    }
    [Command]
    void CmdUpdateHeadRotation(Quaternion rotation)
    {

    }
    
    [Command]
    public virtual void CmdAttack1()
    {

        //act_attack1.runServer(this);
    }
    [Command]
    public virtual void CmdFire()
    {
    }
    
}

