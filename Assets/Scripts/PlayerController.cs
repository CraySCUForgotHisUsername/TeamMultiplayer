
 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PlayerController : NetworkBehaviour {
    public Action 
        act_attack1, 
        act_attack2, 
        act_ability1,
        act_ability2,
        act_ability3;
    
    [SerializeField]
    public GameObject m_eye;
    public PlayerMotor m_motor;

    [SyncVar]
    Quaternion headRotation;
	// Use this for initialization
	void Start () {

    }
    public override void OnStartLocalPlayer()
    {
    }
    public override void OnStartAuthority()
    {
        onStart();
    }
    void onStart()
    {
        gameObject.name = "OtherPlayer";
        if (!isLocalPlayer) return;
        Debug.Log("OnStartLocalPlayer");
        gameObject.name = "LocalPlayer";
        m_eye.SetActive(true);

    }
    public void link(PlayerMotor motor)
    {
        m_motor = motor;
        m_motor.setAvatar(isLocalPlayer);
        motor.addToHead(m_eye.transform);
        //m_eye.transform.parent = m_motor.m_avatar.m_head.transform;
        m_eye.transform.localPosition = Vector3.zero;
    }
    [ClientRpc]
    public void RpcLink(NetworkInstanceId id)
    {
        var obj = ClientScene.FindLocalObject(id);
        link( obj.GetComponent<PlayerMotor>());
       // m_motor = motor;
       // m_eye.transform.parent = m_motor.m_head.transform;
       // m_eye.transform.localPosition = Vector3.zero;
    }
    // Update is called once per frame
    void Update () {
        //    Debug.Log(hasAuthority);
        if (!isLocalPlayer)
        {
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


        // CmdUpdateHeadRotation(m_head.rotation);

        //attack1();
        //CmdAttack1();
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
    
    public virtual void attack1()
    {
        act_attack1.runLocal(this);
    }
  
    public virtual void attack2()
    {

    }
    public virtual void reload()
    {

    }
    public virtual void ability1()
    {

    }
    public virtual void ability2()
    {

    }
    public virtual void ability3()
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
/*
 */
/*
 *         // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            PREFAB_PROJECTILE,
            m_bulletSpawn.position,
            m_bulletSpawn.rotation);

        // Add velocity to the bullet

        bullet.GetComponent<Rigidbody>().velocity = ((this.transform.position + m_motor.m_head.forward * 1000) - m_bulletSpawn.transform.position).normalized * 100;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
 */
