using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NAction
{

    public class Melee : Action
    {
        [SerializeField]
        HitboxApply PREFAB_MELEE_HITBOX;

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
            var avatar = motor.m_avatarManager.getAvatar();
            var avatarCollision = motor.m_avatarManager.getAvatarCollision();
            var m_meleeColliderObj = GameObject.Instantiate<HitboxApply >(PREFAB_MELEE_HITBOX);
            for(int i = 0; i < m_meleeColliderObj.m_colliders.Count; i++)
            {
                Physics.IgnoreCollision(m_meleeColliderObj.m_colliders[i], avatarCollision.m_bodyColldier);
                Physics.IgnoreCollision(m_meleeColliderObj.m_colliders[i], avatarCollision.m_headCollider);

            }
            m_meleeColliderObj.gameObject.SetActive(true);
            m_meleeColliderObj.transform.position = avatar.m_head.transform.position;
            m_meleeColliderObj.transform.rotation = avatar.m_head.transform.rotation;
            //base.use(motor);
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Melee action class enter" + other);
        }
    }

}
