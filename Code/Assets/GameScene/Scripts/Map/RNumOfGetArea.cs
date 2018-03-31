using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RNumOfGetArea : MonoBehaviour {

    private MapManager MpMng;
    private RMapServer MSvr;

    // 0 = red / 1 = blue / 2 = yellow / 3 = green
    public int[] GetArea = new int[4];

    private void Start()
    {
        MpMng = GameObject.Find("GamePanel").transform.GetChild(0).GetComponent<MapManager>();
        MSvr = GameObject.Find("GamePanel").transform.GetChild(0).GetComponent<RMapServer>();
        InvokeRepeating("CheckPainted", 0f, 0.1f);
    }

	void Update () {

    }

    void CheckPainted()
    {
        GetArea = new int[] { 0,0,0,0 };

        for (int z = 0; z < MpMng.MapZ; z++)
        {
            for (int x = 0; x < MpMng.MapX; x++)
            {
                if (MpMng.Map[z, x] != 9)
                {   
                    GameObject obj = GameObject.Find("Floor[" + z + "][" + x + "]");

                    if (obj.GetComponent<Renderer>().material.color == Color.red)
                    {
                        GetArea[0]++;
                        if (MSvr.bIsSvr) MSvr.MData[z, x] = 1;
                    }
                    else if (obj.GetComponent<Renderer>().material.color == Color.blue)
                    {
                        GetArea[1]++;
                        if (MSvr.bIsSvr) MSvr.MData[z, x] = 2;
                    }
                    else if (obj.GetComponent<Renderer>().material.color == Color.yellow)
                    {
                        GetArea[2]++;
                        if (MSvr.bIsSvr) MSvr.MData[z, x] = 3;
                    }
                    else if (obj.GetComponent<Renderer>().material.color == Color.green)
                    {
                        GetArea[3]++;
                        if (MSvr.bIsSvr) MSvr.MData[z, x] = 4;
                    }
                }
            }
        }
        
        // Resetting Text

        // red
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = GetArea[0].ToString();
        // blue
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = GetArea[1].ToString();
        // yellow
        gameObject.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GetArea[2].ToString();
        // green
        gameObject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GetArea[3].ToString();
    }
}