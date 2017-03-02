using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NMotor {

    public class Rocket : Motor
    {
        public override void jump(float horizontal, float vertical)
        {
            base.jump(horizontal, vertical);
        }
        public override void jumpEnd()
        {
            base.jumpEnd();
        }
        public override void kFixedUpdate(float timeElapsed)
        {
            base.kFixedUpdate(timeElapsed);
        }
        public override void kUpdate()
        {
            base.kUpdate();
        }
    }

}
