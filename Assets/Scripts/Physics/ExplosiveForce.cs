using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExplosiveForce : TriggerEnter
{
    [SerializeField]
    float force, m_damageMin, m_damage,radius, uplift;
    public override void onObjectEnter(Rigidbody body, EntityPlayer entity, Vector3 impactPoint)
    {
        base.onObjectEnter(body, entity, impactPoint);
        float damage =  Mathf.Max(m_damageMin, m_damage*( radius - (this.transform.position - impactPoint).magnitude) / radius );
        if (entity != null)
        {
            var player = entity.GetComponent<Player>();
            if (player != null)
            {
                player.RpcAddExplosionForce(force, this.transform.position - player.transform.position, radius, uplift, ForceMode.Impulse);
                if(entity.takeDamage(damage / 2))
                {
                    entity.TargetTakeDamage(entity.connectionToClient, entity.transform.position, damage / 2);

                }
                return;
            }else
            {
                entity.takeDamage(damage);
            }
            
            //if (player.hasAuthority) return;
           
        }

        if (body != null)
        {
            var networkBody = body.GetComponent<NetworkRigidbody>();
            if (networkBody != null )
            {
                networkBody.RpcAddExplosionForce(force, this.transform.position - networkBody.transform.position, radius, uplift, ForceMode.Impulse);
                return;

            }
        }

        //body.AddExplosionForce(force,this.transform.position,radius,uplift, ForceMode.Impulse);
        
        
        
    }
}