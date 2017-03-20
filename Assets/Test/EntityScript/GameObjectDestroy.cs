using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace NEntity.NScript
{

    public class GameObjectDestroy : Script
    {
        public GameObject obj;
        public bool isNetworkedObject = false;
        public override bool init(EntityPlayer entity)
        {
            base.init(entity);
            if(isNetworkedObject)
                NetworkServer.Destroy(obj);
            else
                GameObject.Destroy(obj);

            return true;
        }
    }

}
