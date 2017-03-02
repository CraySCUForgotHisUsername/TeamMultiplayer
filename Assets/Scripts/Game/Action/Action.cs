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
    public virtual void use(NMotor.Motor motor)
    {

    }
    public virtual void hold(NMotor.Motor motor)
    {

    }
    public virtual void end(NMotor.Motor motor)
    {

    }
    
    public virtual void kFixedUpdate(NMotor.Motor motor, float timeElapsed)
    {

    }
    public virtual void kUpdate(NMotor.Motor motor, float timeElapsed)
    {

    }
}
