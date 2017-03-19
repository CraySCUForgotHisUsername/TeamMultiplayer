using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Entity : NetworkBehaviour
{
    [SerializeField]
    public GameData.TEAM m_team;
    [SerializeField]
    public GameData.TYPE m_type;

    public static Entity LOCAL_PLAYER_ENTITY;
    public delegate float DEL_MODIFIER();
    public delegate void DEL_ME(Entity me);
    const float DAMAGE_AVOID_DISTANCE = 1.0f;
    const float DAMAGE_AVOID_REDUCTION = 0.5f;

    public List<DEL_ME> m_lazyEvents = new List<DEL_ME>();

    [SyncVar]
    [SerializeField]
    public float
        health = 100.0f,
        healthMax = 100.0f;
    float m_healthBefore = 0;

    [SerializeField]
    public int m_ammo, m_ammoMax;
    [SerializeField]
    public float m_resourceNow, m_resourceMax;
    [SerializeField]
    public float m_resourceRegen;

    [SyncVar]
    [SerializeField]
    float
        m_speed = 10.0f,
        m_speedRotation = 1.0f,
        m_jumpPower = 10,
        m_gravityPower = 20;


    [SyncVar]
    [SerializeField]
    float   modGravity = 0.0f,
            modAir = 0.0f,
            modOffense = 0.0f, //Bonus damage or reduced damage and such
            modDefense = 0.0f, //Reduced damage or increased damage taken and such
            modSpeed = 0.0f;   //Increased speed or decreased speed 

    public bool m_isRegenResource = true;

    public List<DEL_MODIFIER> scalarOffense = new List<DEL_MODIFIER>();
    public List<DEL_MODIFIER> scalarDefense = new List<DEL_MODIFIER>();
    public List<DEL_MODIFIER> scalarSpeed = new List<DEL_MODIFIER>();
    public List<DEL_MODIFIER> scalarJump = new List<DEL_MODIFIER>();
    public float AirControl
    {
        get
        {
            return 1 + modAir;
        }
    }

    public float Health {
        get { return health; }
        set
        {
            health = healthMax;
        }
    }
    public float Speed
    {
        get
        {
            float scalar = 1 + Mathf.Max(-1, scalarSpeed.Select(s => s()).Sum());
            //Debug.Log(scalar);
            return m_speed * scalar;
        }

    }
    public float RotationSpeed
    {
        get
        {
            return m_speedRotation;
        }
    }
    public float Jump
    {
        get
        {
            float scalar = 1 + Mathf.Max(-1, scalarJump.Select(s => s()).Sum());
            //Debug.Log(scalar);
            return m_jumpPower * scalar;
        }

    }
    public float Gravity
    {
        get
        {
            return m_gravityPower;
        }
    }
    public float Offense {
        get
        {
            return 1.0f;
        }
    }
    public float Defense {
        get
        {
            return 1.0f;
        }
    }
     public float Time {
        get
        {
            return 1.0f;
        }
    }


    
    void raiseLazyEvent()
    {
        for (int i = 0; i < m_lazyEvents.Count; i++)
            m_lazyEvents[i](this);
    }
    bool isTakingDamage = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_healthBefore != health)
        {
            m_healthBefore = health;
        }
        raiseLazyEvent();
        //Debug.Log("isserVEr " + isServer);
        
    }
    public void kUpdate(float timeElapsed)
    {
        if(m_isRegenResource)
            m_resourceNow = Mathf.Min(m_resourceMax, m_resourceNow + m_resourceRegen * timeElapsed);
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

        if (!isServer)
            CmdClientTakeDamage(amount);

        if (health < 1)
        {
            health = 0;

        }
        raiseLazyEvent();
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
        if (!isServer)
            CmdClientTakeDamage(amount);
        if (health < 1)
        {
            health = 0;

        }
        raiseLazyEvent();
        return true;
    }
    [Command]
    public void CmdClientSetHealth(float health)
    {
        this.health = health;
    }
    [Command]
    void CmdClientTakeDamage(float amountOfDamageTaken)
    {
        this.health -= amountOfDamageTaken;
    }
    [TargetRpc]
    public void TargetTakeDamage(NetworkConnection target, Vector3 impactPoint, float damage)
    {
        //Hello I am told I am taking damage?
        Debug.Log("Hello I am told I am taking damage " + this.gameObject.name);
        if ((this.transform.position - impactPoint).magnitude > DAMAGE_AVOID_DISTANCE)
        {
            //Ok I think I kinda avoided it.
            //I deserve to take less damage from it.
            takeDamage(damage * DAMAGE_AVOID_REDUCTION);
        }
        else
        {
            takeDamage(damage);

        }
    }

    public float getModSpeed()
    {
        return 1.0f;
    }
    public int useAmmoTest(int amount, bool isStream)
    {
        if (!isStream && m_ammo < amount)
        {
            return 0;
        }
        int ammoUsed = Mathf.Min(amount, m_ammo);
        return ammoUsed;

    }
    public int useAmmo(int amount, bool isStream)
    {
        int ammoUsed = useAmmoTest(amount, isStream);
        //Debug.Log("AMMOUS ED " + amount + ", " + ammoUsed);
        m_ammo -= ammoUsed;
        raiseLazyEvent();
        return ammoUsed;

    }
    public float useResourceTest(float amount, bool isStream)
    {
        if (!isStream && m_resourceNow < amount)
        {
            return 0;
        }
        float resourceUsed = Mathf.Min(amount, m_resourceNow);
        return resourceUsed;
    }
    public float useResource(float amount, bool isStream)
    {
        float resourceUsed = useResourceTest(amount, isStream);
        m_resourceNow -= resourceUsed;
        raiseLazyEvent();
        return resourceUsed;
    }
}