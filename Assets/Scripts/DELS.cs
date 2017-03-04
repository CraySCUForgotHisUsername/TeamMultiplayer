using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DEL {

    public delegate void VOID_ENTITY_MOTOR(NEntity.Entity entity, NMotor.Motor motor);

    public static void RAISE( List<VOID_ENTITY_MOTOR> events, NEntity.Entity entity, NMotor.Motor motor)
    {
        for(int i = 0; i< events.Count; i++)
        {
            events[i](entity, motor);
        }

    }
}
