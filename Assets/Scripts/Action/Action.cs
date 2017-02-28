using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Action : MonoBehaviour  {

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
    /*
    public Vector3 fire(Vector3 position, Vector3 direction, float maxTravelDistance, int damage)
    {
        float travelDistance = 0;
        var ray = new Ray(position, direction);
        RaycastHit hit;
        NEntity.Entity targetHealth;

        Physics.Raycast(ray, out hit);
        if (hit.transform == null || hit.distance > maxTravelDistance)
        {
            travelDistance = maxTravelDistance;
            //hit the air
        }
        else
        {
            targetHealth = hit.transform.GetComponent<NEntity.Entity>();
            Debug.Log(hit.transform.gameObject.name);
            if(targetHealth!=null)
                targetHealth.takeDamage((int)damage);
            travelDistance = hit.distance;
            // trail.transform.LookAt(hit.point);

        }

        return position + direction * travelDistance;
        // trail.transform.position = transform.position;
    }
     * */

    public virtual void kFixedUpdate(NMotor.Motor motor, float timeElapsed)
    {

    }
    public virtual void kUpdate(NMotor.Motor motor, float timeElapsed)
    {

    }
}
