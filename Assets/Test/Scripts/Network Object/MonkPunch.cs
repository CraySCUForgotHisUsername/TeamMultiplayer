using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace NetworkObject {

    public class MonkPunch : NetworkBehaviour
    {

        [SerializeField]
        Prefab
            m_prfAttack,
            m_prfShield;

        [ClientRpc]
        public void RpcInit(GameData.TEAM team)
        {

        }
        [Command]
        public void CmdInit(GameData.TEAM team)
        {

        }

        public void init(GameData.TEAM team)
        {
            PhysicsLayer.SET_ATTACK(m_prfAttack, team);
            PhysicsLayer.SET_SHIELD(m_prfShield, team);
        }
        private void Start()
        {
            if (isServer)
                this.enabled = true;

        }
        private void Update()
        {

        }
    }

}
