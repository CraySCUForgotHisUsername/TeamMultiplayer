using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NAction {

    public class Projectile : Action
    {
        [SerializeField]
        NEntity.Projectile PREFAB_PROJECTILE;
        
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public override void useProcess(NEntity.Entity entity, NMotor.Motor motor)
        {
            base.use(entity,motor);
            Avatar avatar = motor.m_avatarManager.getAvatar();
            NEntity.Projectile proj =  GameObject.Instantiate(PREFAB_PROJECTILE).GetComponent<NEntity.Projectile>();
            proj.transform.position = avatar.m_weapon.transform.position;
            proj.transform.rotation = avatar.m_weapon.transform.rotation;
            Physics.IgnoreCollision(proj.m_colliderProjectile.GetComponent<Collider>(), avatar.m_headCollider);
            Physics.IgnoreCollision(proj.m_colliderProjectile.GetComponent<Collider>(), avatar.m_bodyColldier);
        }
    }

}
