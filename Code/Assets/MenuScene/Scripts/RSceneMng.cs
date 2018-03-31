using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSceneMng : MonoBehaviour
{

    public bool bNet; // Network On/Off(true/false)
    public bool bHost;
    public bool bJoin;

    public int[] iScore;
    bool bGame;


    void Start()
    {
        bNet = false;
        bHost = false;
        bJoin = false;

        iScore = new int[4];
        bGame = false;
    }

    private void Update()
    {
        gameObject.GetComponent<RNetworkMng>().enabled = bNet;
        gameObject.GetComponent<NetworkView>().enabled = bNet;
    }

    // init score's value at "num_"
    public void SetScore(int num_)
    {
        if (bGame) return;

        for (int i = 0; i < 4; i++)
            iScore[i] = num_;
        bGame = true;
    }
    public void SetScore()
    {
        //if (!bGame) return;

        iScore = GameObject.Find("Canvas/PLPanel").GetComponent<RNumOfGetArea>().GetArea;
        bGame = false;
    }
}