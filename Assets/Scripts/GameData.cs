using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace GameData
{
    public enum TEAM { WORLD, RED, BLUE };
    public enum TYPE { A, B ,C , D , E, F,G};
   
    public class PlayerInfo {
        public TEAM team;
        public TYPE type;
        public NetworkConnection connection;
        public PlayerController controller;
        //public Motor motor;


    }

}
