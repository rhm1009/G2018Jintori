using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class RNetReady : NetworkBehaviour {

    public bool Ready = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // viewing connecting info
        if (GameObject.Find("Character[RED]"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }else { gameObject.transform.GetChild(0).gameObject.SetActive(false); }
        if (GameObject.Find("Character[BLUE]"))
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else { gameObject.transform.GetChild(1).gameObject.SetActive(false); }
        if (GameObject.Find("Character[YELLOW]"))
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
        else { gameObject.transform.GetChild(2).gameObject.SetActive(false); }
        if (GameObject.Find("Character[GREEN]"))
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else { gameObject.transform.GetChild(3).gameObject.SetActive(false); }
    }

    public void GetReady()
    {
        if(!isServer)
        {
            return;
        }

        Ready = true;
    }
}
