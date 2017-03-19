using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//receive the player input then do stuff from server 
public class PlayerInputManager : MonoBehaviour {
    public static PlayerInputManager ME;
    public NetworkPrefabLoader NETWORK_PREFAB;

    public RocketScript PREFAB_ROCKET_SCRIPT;
    public GrenadeScript PREFAB_GRENADE_SCRIPT;
    public MonkPunchScript SCRIPT_MONKPUNCH;
    //public MonkScript PREFAB_MONK_SCRIPT;
    // Use this for initialization
    private void Awake()
    {
        ME = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ROCKET(GameData.TEAM team, Vector3 position, Vector3 to)
    {
        NetworkPrefabLoader networkObj = Instantiate<NetworkPrefabLoader>(NETWORK_PREFAB);
        Entity entity = networkObj.GetComponent<Entity>();
        Rigidbody body = networkObj.GetComponent<Rigidbody>();
        RocketScript script = Instantiate<RocketScript>(PREFAB_ROCKET_SCRIPT);
        script.init(networkObj, team, entity, body, position, to);

        NetworkServer.Spawn(networkObj.gameObject);
        //networkObj.RpcLoadPrefab_ParentMe(PREFAB_ID.ROCKET, team);

    }
    public void GRENADE(GameData.TEAM team, Vector3 position, Vector3 to)
    {
        NetworkPrefabLoader networkObj = Instantiate<NetworkPrefabLoader>(NETWORK_PREFAB);
        networkObj.transform.position = position;
        networkObj.transform.LookAt(to);
        NetworkServer.Spawn(networkObj.gameObject);

        Entity entity = networkObj.GetComponent<Entity>();
        Rigidbody body = networkObj.GetComponent<Rigidbody>();
        GrenadeScript script = Instantiate<GrenadeScript>(PREFAB_GRENADE_SCRIPT);
        script.transform.parent = networkObj.transform;
        script.transform.position = networkObj.transform.position;
        script.transform.rotation = networkObj.transform.rotation;
        script.init(networkObj, team, entity, body, position, to);

        //networkObj.RpcLoadPrefab_ParentMe(PREFAB_ID.ROCKET, team);

    }
    public void MONK_PUNCH(GameData.TEAM team, Vector3 position, Vector3 to)
    {
        NetworkPrefabLoader networkObj = Instantiate<NetworkPrefabLoader>(NETWORK_PREFAB);
        networkObj.transform.position = position;
        networkObj.transform.LookAt(to);
        NetworkServer.Spawn(networkObj.gameObject);

        Entity entity = networkObj.GetComponent<Entity>();
        Rigidbody body = networkObj.GetComponent<Rigidbody>();
        var script = Instantiate<MonkPunchScript>(SCRIPT_MONKPUNCH);
        script.transform.parent = networkObj.transform;
        script.transform.position = networkObj.transform.position;
        script.transform.rotation = networkObj.transform.rotation;
        script.init(networkObj, team, entity, body, position, to);

        //networkObj.RpcLoadPrefab_ParentMe(PREFAB_ID.ROCKET, team);

    }
}
