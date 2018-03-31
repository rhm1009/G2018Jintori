using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChecker : MonoBehaviour {

    public RMapServer MSvr;

    RGameScript GameSc;
        
	void Update () {
        // Initalizing in GameScene
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene") &&
            !MSvr)
        {
            if(GameObject.Find("GamePanel/_MapManager"))
                MSvr = GameObject.Find("GamePanel/_MapManager").GetComponent<RMapServer>();

            GameSc = GameObject.Find("GamePanel").GetComponent<RGameScript>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameSc && GameSc.gs != RGameScript.GameState.Play)
            return;

        if(collision.gameObject.tag=="cube")
        {
            if (gameObject.name == "Character[RED]")
            {
                //collision.gameObject.GetComponent<Renderer>().material.color = Color.red;

                string[] separatingChars = { "Floor[", "][" , "]"};
                string[] XZ = collision.gameObject.name.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                //Debug.Log("Number of this Floor ... [ Z : " + XZ[0] + " ], [ X : " + XZ[1] + " ]");
                int z = int.Parse(XZ[0]), x = int.Parse(XZ[1]);

                MSvr.InputMapData(x, z, 1);
            }
            else if (gameObject.name == "Character[BLUE]")
            {
                //collision.gameObject.GetComponent<Renderer>().material.color = Color.blue;

                string[] separatingChars = { "Floor[", "][", "]" };
                string[] XZ = collision.gameObject.name.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                //Debug.Log("Number of this Floor ... [ Z : " + XZ[0] + " ], [ X : " + XZ[1] + " ]");
                int z = int.Parse(XZ[0]), x = int.Parse(XZ[1]);

                MSvr.InputMapData(x, z, 2);
            }
            else if (gameObject.name == "Character[YELLOW]")
            {
                //collision.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

                string[] separatingChars = { "Floor[", "][", "]" };
                string[] XZ = collision.gameObject.name.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                //Debug.Log("Number of this Floor ... [ Z : " + XZ[0] + " ], [ X : " + XZ[1] + " ]");
                int z = int.Parse(XZ[0]), x = int.Parse(XZ[1]);

                MSvr.InputMapData(x, z, 3);
            }
            else if (gameObject.name == "Character[GREEN]")
            {
                //collision.gameObject.GetComponent<Renderer>().material.color = Color.green;

                string[] separatingChars = { "Floor[", "][", "]" };
                string[] XZ = collision.gameObject.name.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                //Debug.Log("Number of this Floor ... [ Z : " + XZ[0] + " ], [ X : " + XZ[1] + " ]");
                int z = int.Parse(XZ[0]), x = int.Parse(XZ[1]);

                MSvr.InputMapData(x, z, 4);
            }

            // this client is server
            if(MSvr.bIsSvr)
            {

            }

        }
    }
}
