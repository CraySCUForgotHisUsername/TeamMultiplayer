using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class Rocket_LMB : Action
    {
        public override void useProcess(EntityPlayer entity, EntityMotor motor, Avatar avatar)
        {
            base.useProcess(entity, motor, avatar);
            Player.LOCAL_PLAYER.CmdFireRocket(avatar.m_head.transform.position,
                avatar.m_head.transform.position + avatar.m_head.transform.forward);
        }
    }

}
