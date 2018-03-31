using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadScore : MonoBehaviour {

    public bool LightOn;
    public GameObject SpotLight;

    public int[] Score;

    public GameObject SnMngObj;


    private void Start()
    {
        LightOn = false;

        Score = new int[4];

        SnMngObj = GameObject.Find("SceneMng");
        Score = SnMngObj.GetComponent<RSceneMng>().iScore;
    }

    private void Update()
    {
        if (SpotLight.activeSelf && !LightOn)
        {
            LightOn = true;
            SetScore();
        }
    }

    void SetScore()
    {
        //------------------ Image (Color)
        // red
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        // blue
        gameObject.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
        // yellow
        gameObject.transform.GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        // green
        gameObject.transform.GetChild(4).GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;

        //------------------ Text
        // red
        gameObject.transform.GetChild(1).GetChild(1).GetComponent<TextMesh>().text = Score[0].ToString();
        gameObject.transform.GetChild(1).GetChild(1).GetComponent<TextMesh>().color = Color.white;
        // blue
        gameObject.transform.GetChild(2).GetChild(1).GetComponent<TextMesh>().text = Score[1].ToString();
        gameObject.transform.GetChild(2).GetChild(1).GetComponent<TextMesh>().color = Color.white;
        // yellow
        gameObject.transform.GetChild(3).GetChild(1).GetComponent<TextMesh>().text = Score[2].ToString();
        gameObject.transform.GetChild(3).GetChild(1).GetComponent<TextMesh>().color = Color.white;
        // green
        gameObject.transform.GetChild(4).GetChild(1).GetComponent<TextMesh>().text = Score[3].ToString();
        gameObject.transform.GetChild(4).GetChild(1).GetComponent<TextMesh>().color = Color.white;
    }
}
