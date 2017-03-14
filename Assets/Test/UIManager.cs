using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager ME;
    public delegate void DEL_TEAM(GameData.TEAM team);
    public delegate void DEL_HERO(GameData.TYPE hero);
    static public List<DEL_TEAM> EVENT_TEAM = new List<DEL_TEAM>();
    static public List<DEL_HERO> EVENT_HERO = new List<DEL_HERO>();
    static public bool IS_NEW_INPUT = false;
    static public GameData.TEAM TEAM_SELECTED;
    static public GameData.TYPE HERO_SELECTED;

    [SerializeField]
    GameObject m_UITeam, m_UIHero;
    [SerializeField]
    Button bttnTeamSpectator, bttnTeamRed, bttnTeamBlue, 
        bttnHeroA, bttnHeroB, bttnHeroC, bttnHeroD, bttnHeroE, bttnHeroF;
    [SerializeField]
    Text textResource,textHealth;
    // Use this for initialization




    bool isCursurLocked = false;
    private void Awake()
    {
        
    }
    void Start()
    {
        //bttnTeamSpectator.onClick.AddListener(h_teamSpectator);
        bttnTeamRed.onClick.AddListener(h_teamRed);
        bttnTeamBlue.onClick.AddListener(h_teamBlue);
        bttnHeroA.onClick.AddListener(h_heroA);
        bttnHeroB.onClick.AddListener(h_heroB);
        bttnHeroC.onClick.AddListener(h_heroC);
        bttnHeroD.onClick.AddListener(h_heroD);
        bttnHeroE.onClick.AddListener(h_heroE);
        bttnHeroF.onClick.AddListener(h_heroF);
        Initiate();
        //bttnHeroD.onClick.AddListener(h_heroD);
    }
    
    void h_teamSpectator()
    {

    }
    void h_teamRed()
    {
        hdrNewTeam(GameData.TEAM.RED);
    }
    void h_teamBlue()
    {
        hdrNewTeam(GameData.TEAM.BLUE);
    }
    void h_heroA()
    {
        hdrNewHero(GameData.TYPE.A);
    }
    void h_heroB()
    {
        hdrNewHero(GameData.TYPE.B);
    }
    void h_heroC()
    {
        hdrNewHero(GameData.TYPE.C);
    }
    void h_heroD()
    {
        hdrNewHero(GameData.TYPE.D);
    }
    void h_heroE()
    {
        hdrNewHero(GameData.TYPE.E);
    }
    void h_heroF()
    {
        hdrNewHero(GameData.TYPE.F);
    }
    void h_heroG()
    {
        hdrNewHero(GameData.TYPE.G);
    }
    void hdrNewTeam(GameData.TEAM team)
    {
        IS_NEW_INPUT = true;
        TEAM_SELECTED = team;
        m_UITeam.SetActive(false);
        m_UIHero.SetActive(true);
    }
    void hdrNewHero(GameData.TYPE type)
    {
        IS_NEW_INPUT = true;
        HERO_SELECTED = type;
        m_UITeam.SetActive(false);
        m_UIHero.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Initiate()
    {
        IS_NEW_INPUT = false;
        Cursor.lockState = CursorLockMode.None;
        m_UITeam.SetActive(true);
        m_UIHero.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
            if (Input.GetKeyDown(KeyCode.H))
        {
            Initiate();
        }
        if (Entity.LOCAL_PLAYER_ENTITY != null)
        {
            textResource.text = "Resource : " + Entity.LOCAL_PLAYER_ENTITY.m_resourceNow + " / " + Entity.LOCAL_PLAYER_ENTITY.m_resourceMax;
            textHealth.text = "Health : " + Entity.LOCAL_PLAYER_ENTITY.health + " / " + Entity.LOCAL_PLAYER_ENTITY.healthMax;
        }
        
        if (UIManager.IS_NEW_INPUT && Player.LOCAL_PLAYER != null)
        {
            UIManager.IS_NEW_INPUT = false;
            Player.LOCAL_PLAYER.CmdChangeTeam(UIManager.TEAM_SELECTED);
            Player.LOCAL_PLAYER.CmdChangeHero(UIManager.HERO_SELECTED);
        }
    }
}
