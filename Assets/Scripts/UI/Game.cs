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
    }

}
