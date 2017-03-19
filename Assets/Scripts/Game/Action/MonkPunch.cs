using System.Collections;
using System.Collections.Generic;
using NMotor;
using UnityEngine;

namespace NAction
{

    public class MonkPunch : Action
    {
        public override void useProcess(Entity entity, EntityMotor motor, Avatar avatar)
        {
            base.useProcess(entity, motor, avatar);
            Player.LOCAL_PLAYER.CmdMonkPunch(avatar.m_head.transform.position,
                avatar.m_head.transform.position + avatar.m_head.transform.forward);
        }
    }

}
