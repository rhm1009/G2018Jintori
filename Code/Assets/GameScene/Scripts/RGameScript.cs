using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGameScript : MonoBehaviour {

    public enum GameState
    {
        Wait,
        Play,
        Over
    }
    public GameState gs;

    public GameObject SnMngObj;

    private RTimer Timer;
    private RNetworkMng NMng;
    private RSceneMng SMng;

    void Start () {
        gs = GameState.Wait;
        if (!SnMngObj)
        {
            SnMngObj = GameObject.Find("SceneMng");
            NMng = SnMngObj.GetComponent<RNetworkMng>();
            SMng = SnMngObj.GetComponent<RSceneMng>();
        }
        if(GameObject.Find("Canvas/Timer"))
            Timer = GameObject.Find("Canvas/Timer").GetComponent<RTimer>();
        
        

        //NMng = GameObject.Find("SceneMng").GetComponent<RNetworkMng>();
    }
	
	void Update () {
        if (!Timer) { Start(); return; }

        Debug.Log(gs);

        if (gs == GameState.Wait && Timer.bGame)
            gs = GameState.Play;
        if (gs == GameState.Play && !Timer.bGame)
            gs = GameState.Over;


        /////////////////////////
		if (gs == GameState.Wait)
        {
            GS_Wait();
        }
        else if (gs == GameState.Play)
        {
            GS_Play();
        }
        else if (gs == GameState.Over)
        {
            GS_Over();
        }
	}

    // Game State : Wait
    void GS_Wait()
    {
        SMng.SetScore(0);
    }
    // Game State : Wait
    void GS_Play()
    {
        
    }
    // Game State : Wait
    void GS_Over()
    {
        NMng.bGameOver = true;
        SMng.SetScore();
    }
}
