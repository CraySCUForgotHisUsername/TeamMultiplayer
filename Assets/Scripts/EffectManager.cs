using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EffectManager : NetworkBehaviour
{
    public static EffectManager ME;
    public Trail
        PREFAB_TRAIL_ALLY,
        PREFAB_TRAIL_ENEMY;
    private void Awake()
    {
        ME = this;

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    void getBulletTrail(bool isAlly, Vector3 from, Vector3 to)
    {
        var trail = GameObject.Instantiate<Trail>((isAlly) ? PREFAB_TRAIL_ALLY : PREFAB_TRAIL_ENEMY);
        var playerObjMotorAvatar = ClientScene.FindLocalObject(from).GetComponent<NMotor.Motor>().getAvatar();

        trail.init(from, to);
    }
     * */
    void getBulletTrail(bool isAlly, NetworkInstanceId playerMotorId,Vector3 from, Vector3 to)
    {
        var trail = GameObject.Instantiate<Trail>((isAlly) ? PREFAB_TRAIL_ALLY : PREFAB_TRAIL_ENEMY);
        //var playerObjMotorAvatar = ClientScene.FindLocalObject(playerMotorId).GetComponent<NMotor.Motor>().getAvatar();

        trail.init(from, to);
    }
    void getBulletTrail(bool isAlly, NetworkInstanceId playerMotorId, Vector3 to)
    {
        //var trail = GameObject.Instantiate<Trail>( (isAlly)? PREFAB_TRAIL_ALLY :PREFAB_TRAIL_ENEMY);
        //var playerObjMotorAvatar = ClientScene.FindLocalObject(playerMotorId).GetComponent<NMotor.Motor>().m_avatarManager.getAvatar();
        //trail.init(playerObjMotorAvatar.m_weapon.transform.position, to);
    }
    [TargetRpc]
    public void TargetGetBulletTrail_FromTo(NetworkConnection target, bool isAlly, NetworkInstanceId playerMotorId, Vector3 from, Vector3 to)
    {
        getBulletTrail(isAlly, playerMotorId, from,to);

    }
    [TargetRpc]
    public void TargetGetBulletTrail(NetworkConnection target, bool isAlly, NetworkInstanceId playerMotorId, Vector3 to)
    {
        getBulletTrail(isAlly, playerMotorId, to);

    }
    public void getPlayerBulletTrail(GameData.TEAM team, NetworkInstanceId playerMotorId, Vector3 from, Vector3 to)
    {
        //Trail prefab;
        //switch (team) {
        //    default:
        //    case GameData.TEAM.SPECTATOR:
        //        prefab = PREFAB_TRAIL_GREY;
        //        break;
        //    case GameData.TEAM.RED:
        //        prefab = PREFAB_TRAIL_RED;
        //        break;
        //    case GameData.TEAM.BLUE:
        //        prefab = PREFAB_TRAIL_BLUE;
        //        break;
        //}
        getBulletTrail(true, playerMotorId, from, to);
        //var prefab = (team == GameData.TEAM.RED) ? PREFAB_TRAIL_RED : PREFAB_TRAIL_BLUE;
        //var trail = GameObject.Instantiate<Trail>(PREFAB_TRAIL_ALLY);
        //var playerObjMotorAvatar = ClientScene.FindLocalObject(playerMotorId).GetComponent<PlayerMotor>().getAvatar();
        //
        //trail.init(playerObjMotorAvatar.m_weapon.transform.position, to);
        //ClientCommunication.ME.CmdEffectPlayerBulletTrail_FromTo(playerMotorId,from, to);
    }
    public void getPlayerBulletTrail(GameData.TEAM team, NetworkInstanceId playerMotorId, Vector3 to)
    {
        //Trail prefab;
        //switch (team) {
        //    default:
        //    case GameData.TEAM.SPECTATOR:
        //        prefab = PREFAB_TRAIL_GREY;
        //        break;
        //    case GameData.TEAM.RED:
        //        prefab = PREFAB_TRAIL_RED;
        //        break;
        //    case GameData.TEAM.BLUE:
        //        prefab = PREFAB_TRAIL_BLUE;
        //        break;
        //}
        getBulletTrail(true, playerMotorId, to);
        //var prefab = (team == GameData.TEAM.RED) ? PREFAB_TRAIL_RED : PREFAB_TRAIL_BLUE;
        //var trail = GameObject.Instantiate<Trail>(PREFAB_TRAIL_ALLY);
        //var playerObjMotorAvatar = ClientScene.FindLocalObject(playerMotorId).GetComponent<PlayerMotor>().getAvatar();
        //
        //trail.init(playerObjMotorAvatar.m_weapon.transform.position, to);
        //ClientCommunication.ME.CmdEffectPlayerBulletTrail( playerMotorId,  to);
    }
    
}
