using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour {
    [SerializeField]
    Avatar  PREFAB_AVATAR_COLLISION,
            PREFAB_AVATAR_RED_FIRSTPERSON,
            PREFAB_AVATAR_RED_THIRDPERSON,
            PREFAB_AVATAR_BLUE_FIRSTPERSON,
            PREFAB_AVATAR_BLUE_THIRDPERSON;
    Avatar m_collison, m_model;
    private void Awake()
    {
        m_collison = Instantiate<Avatar>(PREFAB_AVATAR_COLLISION);
        m_collison.transform.SetParent(this.transform);
        m_collison.transform.localPosition = Vector3.zero;
        m_collison.transform.localRotation = Quaternion.identity;


         
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void setAvatar(GameData.TEAM team, bool isFirstPersonView)
    {
        Avatar avatar = null;
        Debug.Log(team);
        if (team == GameData.TEAM.RED)
        {

            if (isFirstPersonView)
            {
                avatar = Instantiate<Avatar>(PREFAB_AVATAR_RED_FIRSTPERSON);
            }
            else
            {
                avatar = Instantiate<Avatar>(PREFAB_AVATAR_RED_THIRDPERSON);
            }
        }
        else if(team == GameData.TEAM.BLUE)
        {
            if (isFirstPersonView)
            {
                avatar = Instantiate<Avatar>(PREFAB_AVATAR_BLUE_FIRSTPERSON);
            }
            else
            {
                avatar = Instantiate<Avatar>(PREFAB_AVATAR_BLUE_THIRDPERSON);
            }

        }
        if(avatar != null)
            addAvatar(avatar);
    }
    void addAvatar(Avatar avatar)
    {
        avatar.transform.SetParent(this.transform);
        avatar.transform.localPosition = Vector3.zero;
        avatar.transform.localRotation = Quaternion.identity;

        addToHead(avatar.m_head.transform);
        avatar.m_head.transform.localPosition = Vector3.zero;
        //avatar.m_head.transform.localRotation = Quaternion.identity;
        addToBody(avatar.m_body.transform);
        avatar.m_body.transform.localPosition = Vector3.zero;
        //avatar.m_body.transform.localRotation = Quaternion.identity;
        m_model = avatar;

    }
    public void addToHead(Transform transform)
    {
        transform.SetParent(m_collison.m_head.transform);
    }
    public void addToBody(Transform transform)
    {

        transform.SetParent(m_collison.m_body.transform);
    }
    public Avatar getAvatar()
    {
        return m_model;
    }
    public Avatar getAvatarCollision()
    {
        return m_collison;
    }
    public Quaternion getHeadRotation()
    {
        return m_collison.m_head.transform.rotation;
    }
    public void setHeadRotation(Quaternion rotation)
    {
        m_collison.m_head.transform.rotation = rotation;
    }
    public Vector3 Up
    {
        get
        {
            return m_collison.transform.up;
        }

    }
    public Vector3 Forward
    {
        get
        {
            return m_collison.m_head.transform.forward;
        }

    }
    public Quaternion HeadRotation
    {
        get
        {
            return m_collison.m_head.transform.rotation;
        }set
        {
            m_collison.m_head.transform.rotation = value;
        }

    }
    public void rotoate(Vector3 rotation)
    {

        m_collison.m_head.transform.Rotate(rotation);
    }
}
