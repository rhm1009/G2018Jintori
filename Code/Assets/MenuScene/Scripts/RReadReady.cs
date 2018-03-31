using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RReadReady : MonoBehaviour {

    public int NumOfPlyr;

    GameObject[] Player;

	void Start () {
        NumOfPlyr = 0;
        Player = new GameObject[4];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!Player[0] && GameObject.Find("Character[RED]"))
        {
            Player[0] = GameObject.Find("Character[RED]");
            NumOfPlyr++;
        }
        if (!Player[1] && GameObject.Find("Character[BLUE]"))
        {
            Player[1] = GameObject.Find("Character[BLUE]");
            NumOfPlyr++;
        }
        if (!Player[2] && GameObject.Find("Character[YELLOW]"))
        {
            Player[2] = GameObject.Find("Character[YELLOW]");
            NumOfPlyr++;
        }
        if (!Player[3] && GameObject.Find("Character[GREEN]"))
        {
            Player[3] = GameObject.Find("Character[GREEN]");
            NumOfPlyr++;
        }
    }
}
