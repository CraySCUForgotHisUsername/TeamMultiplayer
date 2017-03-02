
 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PlayerController : NetworkBehaviour {


    
    [SerializeField]
    public GameObject m_eye;
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
    public void link(NMotor.Motor motor)
    {
        m_motor = motor;
        m_motor.setAvatar(hasAuthority);
        motor.addToHead(m_eye.transform);
        //m_eye.transform.parent = m_motor.m_avatar.m_head.transform;
        m_eye.transform.localPosition = Vector3.zero;
    }
    [ClientRpc]
    public void RpcLink(NetworkInstanceId id)
    {
        var obj = ClientScene.FindLocalObject(id);
        link( obj.GetComponent<NMotor.Motor>());
       // m_motor = motor;
       // m_eye.transform.parent = m_motor.m_head.transform;
       // m_eye.transform.localPosition = Vector3.zero;
    }

    [TargetRpc]
    public void TargetLink(NetworkConnection target, NetworkInstanceId id)
    {
        var obj = ClientScene.FindLocalObject(id);
        link(obj.GetComponent<NMotor.Motor>());
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
        m_motor.kFixedUpdate();

    }
    // Update is called once per frame
    void Update () {
        //    Debug.Log(hasAuthority);
        if (!hasAuthority)
        {
            //Debug.Log("I AM RESETTING!!!!!!!!!!!!! head rotation" + headRotation.eulerAngles);
            m_motor.setHeadRotation( headRotation);
            return;
        }
        m_motor.kUpdate();
        //Calculate movement velocity 
        float xMove = Input.GetAxisRaw("Horizontal"),
            zMove = Input.GetAxisRaw("Vertical");
        //Vector3 velocity = ( xMove,0,+ m_motor.transform.forward * zMove ).normalized;
        //
        m_motor.move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
       
        m_motor.rotate( Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //m_motor.rotateHead(new Vector3(Input.GetAxisRaw("Mouse Y"), 0, 0) );
        headRotation = m_motor.getHeadRotation();
        CmdSetHeadRotation(headRotation.eulerAngles.x, headRotation.eulerAngles.y, headRotation.eulerAngles.z);
        //Debug.Log("I AM refershing head rotation" + headRotation.eulerAngles);


        // CmdUpdateHeadRotation(m_head.rotation);

        //attack1();
        //CmdAttack1();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_motor.jump(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(m_motor.m_actJump != null) m_motor.m_actJump.use(m_motor);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            m_motor.jumpEnd();
            if (m_motor.m_actJump != null) m_motor.m_actJump.end(m_motor);

        }
        if (Input.GetMouseButtonDown(0))
        {
            m_motor.m_action1.use(m_motor);
        }
        else if (Input.GetMouseButton(0))
        {
            m_motor.m_action1.hold(m_motor);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_motor.m_action1.end(m_motor);
        }

        if (Input.GetMouseButtonDown(1))
        {
            m_motor.m_action2.use(m_motor);
        }
        else if (Input.GetMouseButton(1))
        {
            m_motor.m_action2.hold(m_motor);

        }
        else if (Input.GetMouseButtonUp(1))
        {
            m_motor.m_action2.end(m_motor);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("SHIFT");
            m_motor.m_action3.use(m_motor);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("SHIFT");
            m_motor.m_action4.use(m_motor);
        }
        

        /*



        if (Input.GetMouseButtonDown(1))
        {

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            reload();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ability1();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ability2();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ability3();
        }
         * */
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

