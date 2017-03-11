using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPrefabLoader : NetworkBehaviour
{   
    [ClientRpc]
    public void RpcLoadPrefab(PREFAB_ID id, GameData.TEAM team)
    {
        var t = PrefabBank.SPAWN(id, team);
        t.transform.parent = this.transform;
        t.transform.position = this.transform.position;
        t.transform.rotation = this.transform.rotation;
    }

}