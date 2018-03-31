using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RTimer : NetworkBehaviour {

    // active game? ... no active -> false
    public bool bGame;

    public Text TimeText;

    [SyncVar]
    public float fTimeCnt;

    string timeStr;


    void Start()
    {
        bGame = false;
        
        // Set Limit Time
        fTimeCnt = 60f * 1.50f + 0.999f;

        
    }

    void Update()
    {
        //GetComponent<RectTransform>().position.Set(0, -35, 0);
        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -35, 0);
        timeStr = ((int)fTimeCnt / 60).ToString("00") + ":" + ((int)fTimeCnt % 60).ToString("00");
        TimeText.text = "制限時間\n" + timeStr;//Network.time;// timeStr;

        if (bGame)
        {
            if (fTimeCnt < 1) GOFunc();

            if (!isServer) return;
            RpcLTCheck();
        }
    }

    //Limit Time Check
    [ClientRpc]
    void RpcLTCheck()
    {
        fTimeCnt -= Time.deltaTime;
    }

    // Game Over Function
    void GOFunc()
    {
        AudioSource TmOver = GameObject.Find("Sound/GameOver").GetComponent<AudioSource>();
        TmOver.Play();

        bGame = false;

        if(isServer)
            GameObject.Find("SceneMng").GetComponent<RNetworkMng>().Invoke("ChangeScene_Result", 3.0f);
    }
}
