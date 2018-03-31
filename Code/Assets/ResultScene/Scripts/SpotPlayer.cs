using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlayer : MonoBehaviour {

    public GameObject judge;
    public int Winner;
	
	void Update () {
        Winner = judge.GetComponent<JudgeWinner>().iWinner;

        Spotlight(Winner);
    }

    void Spotlight(int cVal_)
    {
        if (cVal_ == 0)
        {
            transform.LookAt(GameObject.Find("Chara_RED").transform);
            if (GameObject.Find("SpotLight/PlayerSpot"))
                SetLayerRecursively(GameObject.Find("Chara_RED"), 8);
        }
        else if (cVal_ == 1)
        {
            transform.LookAt(GameObject.Find("Chara_BLUE").transform);
            if (GameObject.Find("SpotLight/PlayerSpot"))
                SetLayerRecursively(GameObject.Find("Chara_BLUE"), 8);
        }
        else if (cVal_ == 2)
        {
            transform.LookAt(GameObject.Find("Chara_YELLOW").transform);
            if (GameObject.Find("SpotLight/PlayerSpot"))
                SetLayerRecursively(GameObject.Find("Chara_YELLOW"), 8);
        }   
        else if (cVal_ == 3)
        {
            transform.LookAt(GameObject.Find("Chara_GREEN").transform);
            if (GameObject.Find("SpotLight/PlayerSpot"))
                SetLayerRecursively(GameObject.Find("Chara_GREEN"), 8);
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
