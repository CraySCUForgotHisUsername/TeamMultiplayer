using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace GameData
{
    public enum TEAM { SPECTATOR, RED, BLUE };
    public enum HERO { A, B ,C,D};
    public class PlayerInfo {
        public TEAM team;
        public HERO hero;
        public NetworkConnection connection;
        public PlayerController controller;
        public PlayerMotor motor;


    }

}
