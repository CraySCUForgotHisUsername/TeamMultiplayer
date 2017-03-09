using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager ME;
    public delegate void DEL_TEAM(GameData.TEAM team);
    public delegate void DEL_HERO(GameData.HERO hero);
    static public List<DEL_TEAM> EVENT_TEAM = new List<DEL_TEAM>();
    static public List<DEL_HERO> EVENT_HERO = new List<DEL_HERO>();

    [SerializeField]
    Button bttnTeamSpectator, bttnTeamRed, bttnTeamBlue, 
        bttnHeroA, bttnHeroB, bttnHeroC, bttnHeroD, bttnHeroE, bttnHeroF;
    [SerializeField]
    Text textResource;
    // Use this for initialization

    static public bool IS_NEW_INPUT = false;
    static public GameData.TEAM TEAM_SELECTED;
    static public GameData.HERO HERO_SELECTED;
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
        //bttnHeroD.onClick.AddListener(h_heroD);
    }
    
    void h_teamSpectator()
    {

    }
    void h_teamRed()
    {
        IS_NEW_INPUT = true;
        TEAM_SELECTED = GameData.TEAM.RED;
    }
    void h_teamBlue()
    {
        IS_NEW_INPUT = true;
        TEAM_SELECTED = GameData.TEAM.BLUE;
    }
    void h_heroA()
    {
        IS_NEW_INPUT = true;
        HERO_SELECTED = GameData.HERO.A;
    }
    void h_heroB()
    {
        IS_NEW_INPUT = true;
        HERO_SELECTED = GameData.HERO.B;
    }
    void h_heroC()
    {
        IS_NEW_INPUT = true;
        HERO_SELECTED = GameData.HERO.C;
    }
    void h_heroD()
    {
        IS_NEW_INPUT = true;
        HERO_SELECTED = GameData.HERO.D;
    }
    void h_heroE()
    {
        IS_NEW_INPUT = true;
        HERO_SELECTED = GameData.HERO.E;
    }
    void h_heroF()
    {
        IS_NEW_INPUT = true;
        HERO_SELECTED = GameData.HERO.F;
    }
    void h_heroG()
    {
        IS_NEW_INPUT = true;
        HERO_SELECTED = GameData.HERO.G;
    }
    // Update is called once per frame
    void Update()
    {
        if(Entity.LOCAL_PLAYER_ENTITY != null)
        {
            textResource.text = "" + Entity.LOCAL_PLAYER_ENTITY.m_resourceNow + " / " + Entity.LOCAL_PLAYER_ENTITY.m_resourceMax;
        }
    }
}
