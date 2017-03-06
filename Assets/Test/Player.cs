using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public GameObject m_mainCamera;
   
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        m_mainCamera.SetActive(true);
    }
    void Update()
    {
        if (!isLocalPlayer) return;
    }
}
