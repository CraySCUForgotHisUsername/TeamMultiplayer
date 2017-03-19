using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;


public partial class Player : NetworkBehaviour
{
    [Command]
    public void CmdFireGrenade(Vector3 position, Vector3 to)
    {
        PlayerInputManager.ME.GRENADE(this.m_entity.m_team, position, to);
    }
    [Command]
    public void CmdMonkPunch(Vector3 position, Vector3 to)
    {
        PlayerInputManager.ME.MONK_PUNCH(this.m_entity.m_team, position, to);
    }
}