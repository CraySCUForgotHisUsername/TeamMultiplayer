using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NUI {

    public class Game : MonoBehaviour
    {
        public static Game ME;

        [SerializeField]
        UnityEngine.UI.Text
            m_healthCurrent, m_healthMax,
            m_ammoCurrent, m_ammoMax,
            m_resourceCurrent, m_resourceMax,
            m_skillACooldown,m_skillBCooldown;
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
