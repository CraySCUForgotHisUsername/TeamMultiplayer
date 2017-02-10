using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Action_Hitscan : Action {

    // Use this for initialization
    public override void runLocal(PlayerController playerController)
    {
        //var pointOfImpact = fire(playerController.m_motor.m_face.position, playerController.m_motor.m_face.forward, 10, 10);
        //NetworkEffect.INSTANCE.CmdBulletTrail(playerController.m_bulletSpawn.position, pointOfImpact);

        //    );
    }
    public override void runServer(PlayerController playerController)
    {
        var pointOfImpact = fire(playerController.m_motor.m_head.position, playerController.m_motor.m_head.forward, 10, 10);
        NetworkEffect.INSTANCE.CmdBulletTrail(playerController.m_bulletSpawn.position, pointOfImpact);

        //    );
    }



}
