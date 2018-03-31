using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPos : MonoBehaviour {

    public GameObject PlayerPrf;

    public int isPlayer;
    public int iPlayers;
    GameObject[] player = new GameObject[4];
    
    private RNetworkMng NMng;

    float XPos;

    void Start () {
        isPlayer = 0;
        iPlayers = 0;
        
        NMng = GameObject.Find("SceneMng").GetComponent<RNetworkMng>();
        
        if (GameObject.Find("Character[RED]"))
        {
            iPlayers++;
            player[0] = Instantiate(PlayerPrf);
            player[0].name = "Chara_RED";
            player[0].transform.GetChild(1).
                GetComponent<Renderer>().material.color = Color.red;
        }
        if (GameObject.Find("Character[BLUE]"))
        {
            iPlayers++;
            player[1] = GameObject.Find("Character[BLUE]");
            player[1] = Instantiate(PlayerPrf);
            player[1].name = "Chara_BLUE";
            player[1].transform.GetChild(1).
                GetComponent<Renderer>().material.color = Color.blue;
        }
        if (GameObject.Find("Character[YELLOW]"))
        {
            iPlayers++;
            player[2] = GameObject.Find("Character[YELLOW]");
            player[2] = Instantiate(PlayerPrf);
            player[2].name = "Chara_YELLOW";
            player[2].transform.GetChild(1).
                GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (GameObject.Find("Character[GREEN]"))
        {
            iPlayers++;
            player[3] = GameObject.Find("Character[GREEN]");
            player[3] = Instantiate(PlayerPrf);
            player[3].name = "Chara_GREEN";
            player[3].transform.GetChild(1).
                GetComponent<Renderer>().material.color = Color.green;
        }

        NMng.NetworkExit();

        XPos = 0;
    }
	
	void Update () {
        // Default Rotate
        Quaternion defRot = Quaternion.Euler(0, 0, 0);

        XPos = 0;
        for (int i = 0; i < iPlayers; i++)
        {
            XPos += (i - 1) * 0.5f;
            if (iPlayers == 2) XPos = 1.5f;

            player[i].transform.position = new Vector3(XPos - (i * 2), 0, 5.5f);
            player[i].transform.rotation = defRot;
        }

        /*
		if (iPlayers == 1)
        {
            player[0] = Instantiate(PlayerPrf);
            player[0].transform.position = new Vector3(0, 0, 5.5f);
            player[0].transform.rotation = defRot;
            player[0].transform.GetChild(1).GetComponent<Material>().color = Color.red;
            //player[0].AddComponent<>
        }
        else if (iPlayers == 2)
        {
            player[0].transform.position = new Vector3(1.5f, 0, 5.5f);
            player[0].transform.rotation = defRot;
            player[0].transform.GetChild(1).GetComponent<Material>().color = Color.red;

            player[1].transform.position = new Vector3(-1.5f, 0, 5.5f);
            player[1].transform.rotation = defRot;
        }
        else if (iPlayers == 3)
        {
            player[0].transform.position = new Vector3(2f, 0, 5.5f);
            player[0].transform.rotation = defRot;
            player[0].transform.GetChild(1).GetComponent<Material>().color = Color.red;

            player[1].transform.position = new Vector3(0, 0, 5.5f);
            player[1].transform.rotation = defRot;

            player[2].transform.position = new Vector3(-2f, 0, 5.5f);
            player[2].transform.rotation = defRot;
        }
        else if (iPlayers == 4)
        {
            player[0].transform.position = new Vector3(3f, 0, 5.5f);
            player[0].transform.rotation = defRot;
            player[0].transform.GetChild(1).GetComponent<Material>().color = Color.red;

            player[1].transform.position = new Vector3(1f, 0, 5.5f);
            player[1].transform.rotation = defRot;

            player[2].transform.position = new Vector3(-1f, 0, 5.5f);
            player[2].transform.rotation = defRot;

            player[3].transform.position = new Vector3(-3f, 0, 5.5f);
            player[3].transform.rotation = defRot;
        }
        */
	}
}
