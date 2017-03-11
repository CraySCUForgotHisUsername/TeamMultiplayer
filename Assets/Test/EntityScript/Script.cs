using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NEntity.NScript
{

    public class Script : MonoBehaviour
    {
        public virtual bool init(Entity entity)
        {
            return true;
        }
        public virtual void kFixedUpdate(Entity entity,float timeElapsed)
        {

        }
        public virtual void kUpdate(Entity entity, float timeElapsed)
        {

        }
        public virtual bool isCompleted(Entity entity)
        {
            return true;
        }
        public virtual bool confirmComplete(Entity entity)
        {
            return true;
        }
    }

}
