﻿using System.Collections;
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

    public static EntityPlayer LOCAL_PLAYER_ENTITY;
    public delegate float DEL_MODIFIER();
    public delegate void DEL_ME(EntityPlayer me);
    const float DAMAGE_AVOID_DISTANCE = 1.0f;
    const float DAMAGE_AVOID_REDUCTION = 0.5f;

    public List<DEL_ME> m_lazyEvents = new List<DEL_ME>();

    [SyncVar]
    [SerializeField]
    public float
        health = 100.0f,
        healthMax = 100.0f;
    

    [SyncVar]
    [SerializeField]
    float
        m_speed = 10.0f,
        m_speedRotation = 1.0f,
        m_gravityPower = 20;


    [SyncVar]
    [SerializeField]
    float   modGravity = 0.0f,
            modDefense = 0.0f, //Reduced damage or increased damage taken and such
            modSpeed = 0.0f;   //Increased speed or decreased speed 
    
    public List<DEL_MODIFIER> scalarDefense = new List<DEL_MODIFIER>();
    public List<DEL_MODIFIER> scalarSpeed = new List<DEL_MODIFIER>();
   

    public float Health
    {
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
    public float Gravity
    {
        get
        {
            return m_gravityPower;
        }
    }
    public float Defense
    {
        get
        {
            return 1.0f;
        }
    }
    public float Time
    {
        get
        {
            return 1.0f;
        }
    }


    public float getModSpeed()
    {
        return 1.0f;
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
    }
    
    public float howMuchDamageWillBeTaken(float amount)
    {
        return amount;
    }
    public bool takeDamage(float amount)
    {
        health -= amount;
        if (!isServer)
            CmdClientTakeDamage(amount);
        if (health < 1)
        {
            health = 0;

        }
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

}