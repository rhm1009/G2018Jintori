using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCubeValue : MonoBehaviour {

    public int cValue;

	// Use this for initialization
	void Start () {
        cValue = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (cValue == 1)
            GetComponent<Renderer>().material.color = Color.red;
        else if (cValue == 2)
            GetComponent<Renderer>().material.color = Color.blue;
        else if (cValue == 3)
            GetComponent<Renderer>().material.color = Color.yellow;
        else if (cValue == 4)
            GetComponent<Renderer>().material.color = Color.green;
        else 
            GetComponent<Renderer>().material.color = Color.gray;

    }
}
