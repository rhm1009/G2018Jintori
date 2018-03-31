using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombarea : MonoBehaviour {

    private RMapServer MSvr;

    // Color Value
    public int CV;
    
	void Start () {
        MSvr = GameObject.Find("GamePanel/_MapManager").GetComponent<RMapServer>();
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cube")
        {
            string[] separatingChars = { "Floor[", "][", "]" };
            string[] XZ = other.gameObject.name.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            int z = int.Parse(XZ[0]), x = int.Parse(XZ[1]);

            BombAreaCheck(x, z, CV);

            //other.GetComponent<RCubeValue>().cValue = CV;
        }
    }

    void BombAreaCheck(int x_, int z_, int cVal_)
    {
        for (int z = -1; z < 2; z++)
        {
            for (int x = -1; x < 2; x++)
            {
                if(GameObject.Find("Floor[" + (z_ + z) + "][" + (x_ + x) + "]"))
                    MSvr.InputMapData(x_ + x, z_ + z, cVal_);
            }
        }
        
    }

}
