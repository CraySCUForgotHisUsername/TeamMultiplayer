using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NEntity.NScript
{

    public class LoadNetworkPrefab : Script
    {
        public NetworkPrefabLoader networkloader;
        public PREFAB_ID id;
        public GameData.TEAM team;
        public bool isParent;
        public override bool init(EntityPlayer entity)
        {
            base.init(entity);
            if(isParent)networkloader.RpcLoadPrefab_ParentMe(id, team);
            else networkloader.RpcLoadPrefab_INDEPENDENT(id, team);
            return true;
        }

    }

}
