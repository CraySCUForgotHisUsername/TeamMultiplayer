using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    
    [SerializeField]
    GameObject screenSelectTeam, screenSelectHero;
    [SerializeField]
    Button bttnTeamSpectator, bttnTeamA, bttnTeamB, bttnHeroA, bttnHeroB, bttnHeroC, bttnHeroD;
    // Use this for initialization

    public bool             newInput = true;
    public GameData.TEAM    m_teamSelected;
    public GameData.TYPE    m_heroSelected;
    void Start()
    {
        //bttnTeamSpectator.onClick.AddListener(h_teamSpectator);
        bttnTeamA.onClick.AddListener(h_teamA);
        bttnTeamB.onClick.AddListener(h_teamB);
        bttnHeroA.onClick.AddListener(h_heroA);
        bttnHeroB.onClick.AddListener(h_heroB);
        bttnHeroC.onClick.AddListener(h_heroC);
        bttnHeroD.onClick.AddListener(h_heroD);
    }

    void h_teamSpectator()
    {

    }
    void h_teamA()
    {
        newInput = true;
        m_teamSelected = GameData.TEAM.RED;
    }
    void h_teamB()
    {
        newInput = true;
        m_teamSelected = GameData.TEAM.BLUE;
    }
    void h_heroA()
    {
        newInput = true;
        m_heroSelected = GameData.TYPE.A;
    }
    void h_heroB()
    {
        newInput = true;
        m_heroSelected = GameData.TYPE.B;
    }
    void h_heroC()
    {
        newInput = true;
        m_heroSelected = GameData.TYPE.C;
    }
    void h_heroD()
    {

    }
    // Update is called once per frame
    void Update () {
		
	}
}
