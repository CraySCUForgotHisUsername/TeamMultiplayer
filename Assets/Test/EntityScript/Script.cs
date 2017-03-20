using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NEntity.NScript
{

    public class Script : MonoBehaviour
    {
        public virtual bool init(EntityPlayer entity)
        {
            return true;
        }
        public virtual void kFixedUpdate(EntityPlayer entity,float timeElapsed)
        {

        }
        public virtual void kUpdate(EntityPlayer entity, float timeElapsed)
        {

        }
        public virtual bool isCompleted(EntityPlayer entity)
        {
            return true;
        }
        public virtual bool confirmComplete(EntityPlayer entity)
        {
            return true;
        }
    }

}
