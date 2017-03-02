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
    void Start()
    {
        bttnTeamSpectator.onClick.AddListener(h_teamSpectator);
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
        ClientCommunication.ME.CmdSelectTeam(GameData.TEAM.RED);
        screenSelectTeam.SetActive(false);
        screenSelectHero.SetActive(true);
    }
    void h_teamB()
    {
        ClientCommunication.ME.CmdSelectTeam(GameData.TEAM.BLUE);
        screenSelectTeam.SetActive(false);
        screenSelectHero.SetActive(true);
        
    }
    void h_heroA()
    {
        ClientCommunication.ME.CmdSelectHero(GameData.HERO.A);
        screenSelectHero.SetActive(false);

    }
    void h_heroB()
    {
        ClientCommunication.ME.CmdSelectHero(GameData.HERO.B);
        screenSelectHero.SetActive(false);

    }
    void h_heroC()
    {
        ClientCommunication.ME.CmdSelectHero(GameData.HERO.C);
        screenSelectHero.SetActive(false);

    }
    void h_heroD()
    {
        ClientCommunication.ME.CmdSelectHero(GameData.HERO.D);
        screenSelectHero.SetActive(false);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
