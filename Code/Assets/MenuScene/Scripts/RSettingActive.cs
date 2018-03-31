using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSettingActive : MonoBehaviour {
    public GameObject[] Btn = new GameObject[6];

    private RSceneMng SMng;

    public bool bHTPlay;

    // Use this for initialization
    void Start ()
    {
        SMng = GameObject.Find("SceneMng").GetComponent<RSceneMng>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!SMng.bNet && !bHTPlay)
        {
            // Single Button
            if (!Btn[0].activeInHierarchy)
            {
                Btn[0].SetActive(true);
            }
            // Multi Button
            if (!Btn[1].activeInHierarchy)
            {
                Btn[1].SetActive(true);
            }
            if (GameObject.Find("NetworkPanel"))
            {
                Destroy(GameObject.Find("NetworkPanel"));
            }
            if(Btn[5].activeInHierarchy)
            {
                Btn[5].SetActive(false);
            }
        }
        if (SMng.bNet || bHTPlay)
        {
            // Single Button
            if (Btn[0].activeInHierarchy)
            {
                Btn[0].SetActive(false);
            }
            // Multi Button
            if (Btn[1].activeInHierarchy)
            {
                Btn[1].SetActive(false);
            }
        }

        if (SMng.bHost || SMng.bJoin)
        {
            // Disconnect Button
            if (!Btn[2].activeInHierarchy)
            {
                Btn[2].SetActive(true);
            }
        }

        if(bHTPlay)
        {
            Btn[5].SetActive(true);
        }
    }
}
