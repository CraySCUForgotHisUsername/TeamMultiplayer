using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NEntity.NScript
{

    public class LoadPrefab : Script
    {
        public Prefab PREFAB;
        public bool isParented = false;
        public bool isPhysicLayer = false;
        public PHYSICS_LAYER layer;
        public override bool init(EntityPlayer entity)
        {
            base.init(entity);
            var obj = GameObject.Instantiate<Prefab>(PREFAB);
            obj.transform.position = this.transform.position;
            obj.transform.rotation = this.transform.rotation;
            if (isParented)
            {
                obj.transform.parent = this.transform;
            }
            if (isPhysicLayer)
            {
                obj.setLayer(PhysicsLayer.GET_LAYER(layer));

            }
            return true;
        }

    }

}
