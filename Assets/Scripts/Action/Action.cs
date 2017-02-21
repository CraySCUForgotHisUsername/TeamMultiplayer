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
    public virtual void use(PlayerMotor motor)
    {

    }
    public virtual void hold(PlayerMotor motor)
    {

    }
    public virtual void end(PlayerMotor motor)
    {

    }
    public Vector3 fire(Vector3 position, Vector3 direction, float maxTravelDistance, int damage)
    {
        float travelDistance = 0;
        var ray = new Ray(position, direction);
        RaycastHit hit;
        Health targetHealth;

        Physics.Raycast(ray, out hit);
        if (hit.transform == null || hit.distance > maxTravelDistance)
        {
            travelDistance = maxTravelDistance;
            //hit the air
        }
        else
        {
            targetHealth = hit.transform.GetComponent<Health>();
            Debug.Log(hit.transform.gameObject.name);
            if(targetHealth!=null)
                targetHealth.takeDamage((int)damage);
            travelDistance = hit.distance;
            // trail.transform.LookAt(hit.point);

        }

        return position + direction * travelDistance;
        // trail.transform.position = transform.position;
    }
    public void kUpdate( PlayerMotor motor, float timeElapsed)
    {

    }
}
