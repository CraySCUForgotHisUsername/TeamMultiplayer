
 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour {
    public Action 
        act_attack1, 
        act_attack2, 
        act_ability1,
        act_ability2,
        act_ability3;
    
    [SerializeField]
    private float
        m_speed = 10.0f,
        m_lookSensitivity = 100.0f;

    [SerializeField]
    public GameObject 
        m_localPlayerObject,
        PREFAB_PROJECTILE;
    public Transform m_head, m_bulletSpawn;
    public PlayerMotor m_motor;

    [SyncVar]
    Quaternion headRotation;
	// Use this for initialization
	void Start () {
        m_motor = GetComponent<PlayerMotor>();
        

    }
    public override void OnStartLocalPlayer()
    {
        gameObject.name = "LocalPlayer";
        m_localPlayerObject.SetActive(true);
    }
    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            m_head.rotation = headRotation;
            return;
        }
        //Calculate movement velocity 
        float xMove = Input.GetAxisRaw("Horizontal"),
            zMove = Input.GetAxisRaw("Vertical");
        Vector3 velocity = (transform.right * xMove + transform.forward * zMove ).normalized* m_speed;
        //
        m_motor.move(velocity);
        m_motor.rotate(new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * m_lookSensitivity);
        m_motor.rotateHead(new Vector3(Input.GetAxisRaw("Mouse Y"), 0, 0) * m_lookSensitivity);
        headRotation = m_head.rotation;


       // CmdUpdateHeadRotation(m_head.rotation);
        if (Input.GetMouseButtonDown(0))
        {
            attack1();
            CmdAttack1();
        }
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

        act_attack1.runServer(this);
    }


    [Command]
    public virtual void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
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
    
}

  