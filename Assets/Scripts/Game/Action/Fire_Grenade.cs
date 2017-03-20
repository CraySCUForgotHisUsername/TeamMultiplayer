using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class Fire_Grenade : Action
    {
        public override void useProcess(EntityPlayer entity, EntityMotor motor, Avatar avatar)
        {
            base.useProcess(entity, motor, avatar);
            Player.LOCAL_PLAYER.CmdFireGrenade(avatar.m_head.transform.position,
                avatar.m_head.transform.position + avatar.m_head.transform.forward);
        }
    }

}
