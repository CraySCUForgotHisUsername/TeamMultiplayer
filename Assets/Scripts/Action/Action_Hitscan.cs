using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Action_Hitscan : Action {

    [SerializeField]
    int m_bounce = 0;
    public float 
        m_damage, m_maxTravelDistance;
    // Use this for initialization
    public override void runLocal(PlayerController playerController)
    {
        //var pointOfImpact = fire(playerController.m_motor.m_face.position, playerController.m_motor.m_face.forward, 10, 10);
        //NetworkEffect.INSTANCE.CmdBulletTrail(playerController.m_bulletSpawn.position, pointOfImpact);

        //    );
    }
    public override void runServer(PlayerController playerController)
    {
        //var pointOfImpact = fire(playerController.m_motor.m_head.position, playerController.m_motor.m_head.forward, 10, 10);
        //NetworkEffect.INSTANCE.CmdBulletTrail(playerController.m_bulletSpawn.position, pointOfImpact);

        //    );
    }
    public override void use(NMotor.Motor motor)
    {
        //Debug.Log("Fired");
        base.use(motor);
        
        fire(motor.m_playerInfo.team, motor.netId,  motor.getAvatar().m_head.transform.position, motor.getAvatar().m_head.transform.forward , m_damage, m_maxTravelDistance, m_bounce);

    }
    public void fire(
        GameData.TEAM team,
        NetworkInstanceId myMotor,
        Vector3 posBegin, Vector3 direction,
        float damage, float maxTravelDistance,int bounce, bool isBouncing = false)
    {
        bool isHitSomething = false;
        float travelDistance = 0;
        var ray = new Ray(posBegin, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
     
        if (hit.transform == null || hit.distance > maxTravelDistance)
        {
            travelDistance = maxTravelDistance;
            //hit the air
        }
        else
        {
            travelDistance = hit.distance;
            isHitSomething = true;
            // trail.transform.LookAt(hit.point);

        }
        if (!isBouncing)
            EffectManager.ME.getPlayerBulletTrail(team, myMotor, posBegin + direction * travelDistance);
        else
            EffectManager.ME.getPlayerBulletTrail(team, myMotor, posBegin, posBegin + direction * travelDistance);

        if (hit.transform != null)
        {
            var targetEntity = hit.transform.GetComponent<NEntity.Entity>();
            if (targetEntity != null && targetEntity.IsTakeDamage)
            {
                var targetMotor = hit.transform.GetComponent<NetworkIdentity>();
                //Debug.Log(ClientCommunication.ME.gameObject.name);
                ClientCommunication.ME.CmdTest();
                //ClientCommunication.ME.CmdDamageRaw(myMotor, targetMotor.netId, hit.point, damage / 2, damage / 2);
                ClientCommunication.ME.CmdDamage(myMotor, targetMotor.netId, hit.point, targetEntity.howMuchDamageWillBeTaken(damage / 2), damage / 2);
                
            }
            else
            {

                if (bounce > 0)
                {
                    var dirNew = Vector3.Reflect(direction, hit.normal);
                    // var posNew = hit.point;// hit.normal
                    fire(team, myMotor, hit.point, dirNew, damage, maxTravelDistance, bounce - 1, true);
                    //fire()

                }
            }

            //ClientCommunication.ME.CmdDamage_(myMotor, targetMotor.netId, hit.point, targetEntity.howMuchDamageWillBeTaken(damage / 2), damage / 2);
            //return;
        }
        //return this.transform.position + this.transform.forward * travelDistance;
        // trail.transform.position = transform.position;
    }


}
