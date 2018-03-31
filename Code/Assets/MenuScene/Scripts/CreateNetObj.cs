using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNetObj : MonoBehaviour {

    public GameObject NetObjPrf;

    void Update() {
        if (!GameObject.Find(NetObjPrf.name))
        {
            GameObject NetObj = Instantiate(NetObjPrf);
            NetObj.name = NetObjPrf.name;
        }	
	}
}
