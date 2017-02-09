using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour {

    [SerializeField]
    private float
        m_speed = 10.0f,
        m_lookSensitivity = 100.0f;

    [SerializeField]
    private GameObject 
        m_localPlayerObject,
        PREFAB_PROJECTILE;
    [SerializeField]
    Transform m_bulletSpawn;
    private PlayerMotor motor;
	// Use this for initialization
	void Start () {
        motor = GetComponent<PlayerMotor>();
        

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
            return;
        }
        //Calculate movement velocity 
        float xMove = Input.GetAxisRaw("Horizontal"),
            zMove = Input.GetAxisRaw("Vertical");
        Vector3 velocity = (transform.right * xMove + transform.forward * zMove ).normalized* m_speed;
        //
        motor.move(velocity);
        motor.rotate(new Vector3(0, Input.GetAxisRaw("Mouse X"), 0) * m_lookSensitivity);
        motor.rotateCamera(new Vector3(Input.GetAxisRaw("Mouse Y"), 0, 0) * m_lookSensitivity);
        if (Input.GetMouseButtonDown(0))
        {
            attack1();
        }
        if (Input.GetMouseButtonDown(1))
        {
            attack2();
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
    
    public virtual void attack1()
    {
        CmdFire();

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
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            PREFAB_PROJECTILE,
            m_bulletSpawn.position,
            m_bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);
         
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}
