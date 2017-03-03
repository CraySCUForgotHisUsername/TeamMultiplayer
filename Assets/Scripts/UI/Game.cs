using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NUI {

    public class Game : MonoBehaviour
    {
        public static Game ME;

        [SerializeField]
        UnityEngine.UI.Text
            m_healthNow, m_healthMax,
            m_ammoNow, m_ammoMax,
            m_resourceNow, m_resourceMax,
            m_skillACooldown,
            m_skillBCooldown;
        // Use this for initialization
        void Awake()
        {
            ME = this;
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void cheapUpdate(NEntity.Entity entity)
        {
            m_healthNow.text = "" + entity.Health;
            m_healthMax.text = "" + entity.healthMax;
            m_ammoNow.text = "" + entity.m_ammo;
            m_ammoMax.text = "" + entity.m_ammoMax;
            m_resourceNow.text = "" + entity.m_resourceNow;
            m_resourceMax.text = "" + entity.m_resourceMax;

        }
        //Depending on the team and hero, different loadout should be loaded
        public void link(GameData.TEAM team, GameData.HERO hero, NEntity.Entity entity)
        {
            cheapUpdate(entity);
            entity.m_lazyEvents.Add(cheapUpdate);
            Debug.Log("LINK TO UI");
        }
    }

}
