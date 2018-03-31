using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RChar_Network : MonoBehaviour {

    public GameObject[] chara = new GameObject[4];
	
	void Update () {

        // Find Player, And Setting Color

        GameObject obj = null;

        if (GameObject.Find("Character(Clone)"))
        {
            obj = GameObject.Find("Character(Clone)");
        }

        // Color Set
        if (!GameObject.Find("Character[RED]") && obj != null)
        {
            obj.name = "Character[RED]";
            obj.GetComponent<RNetCharaInfo>().CharaCV = 1;
            chara[0] = obj;
        }
        else if (!GameObject.Find("Character[BLUE]") && obj != null)
        {
            obj.name = "Character[BLUE]";
            obj.GetComponent<RNetCharaInfo>().CharaCV = 2;
            chara[1] = obj;
        }
        else if (!GameObject.Find("Character[YELLOW]") && obj != null)
        {
            obj.name = "Character[YELLOW]";
            obj.GetComponent<RNetCharaInfo>().CharaCV = 3;
            chara[2] = obj;
        }
        else if (!GameObject.Find("Character[GREEN]") && obj != null)
        {
            obj.name = "Character[GREEN]";
            obj.GetComponent<RNetCharaInfo>().CharaCV = 4;
            chara[3] = obj;
        }
        // Color Set
        if (chara[0] != null &&
            chara[0].transform.GetChild(1).GetComponent<Renderer>().material.color != Color.red)
            chara[0].transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
        if (chara[1] != null &&
            chara[1].transform.GetChild(1).GetComponent<Renderer>().material.color != Color.blue)
            chara[1].transform.GetChild(1).GetComponent<Renderer>().material.color = Color.blue;
        if (chara[2] != null &&
            chara[2].transform.GetChild(1).GetComponent<Renderer>().material.color != Color.yellow)
            chara[2].transform.GetChild(1).GetComponent<Renderer>().material.color = Color.yellow;
        if (chara[3] != null &&
            chara[3].transform.GetChild(1).GetComponent<Renderer>().material.color != Color.green)
            chara[3].transform.GetChild(1).GetComponent<Renderer>().material.color = Color.green;

    }
}
