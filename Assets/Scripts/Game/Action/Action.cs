using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Action : MonoBehaviour  {

    public virtual void init(NMotor.Motor motor)
    {

    }
    // Use this for initialization
    public virtual void runLocal(PlayerController playerController)
    {

    }
    public virtual void runServer(PlayerController playerController)
    {

    }
    public virtual void use(NEntity.Entity entity, NMotor.Motor motor)
    {

    }
    public virtual void hold(NEntity.Entity entity, NMotor.Motor motor)
    {

    }
    public virtual void end(NEntity.Entity entity, NMotor.Motor motor)
    {

    }
    
    public virtual void kUpdate(NEntity.Entity entity, NMotor.Motor motor, float timeElapsed)
    {

    }
    public virtual void kFixedUpdate(NEntity.Entity entity, NMotor.Motor motor, float timeElapsed)
    {

    }
}
