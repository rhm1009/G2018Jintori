using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RMenuButton : MonoBehaviour {

    public GameObject Panel;

    private RSceneMng SMng;
    private RNetworkMng NMng;
    private RSettingActive SetAct;

    // Sound (Effect, Voice)
    private AudioSource Click;

    private AudioSource Voice_Create;
    private AudioSource Voice_Start;

    private void Start()
    {
        SMng = GameObject.Find("SceneMng").GetComponent<RSceneMng>();
        NMng = GameObject.Find("SceneMng").GetComponent<RNetworkMng>();
        Click = GameObject.Find("Sound/Click").GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SetAct = GameObject.Find("Canvas").GetComponent<RSettingActive>();

            Voice_Create = GameObject.Find("Sound/Create").GetComponent<AudioSource>();
            Voice_Start = GameObject.Find("Sound/GameStart").GetComponent<AudioSource>();
        }
    }

    public void OnClick()
    {
        Click.Play();

        // Basic Code
        if (gameObject.name == "ButtonName")
        {
            // Edit Source...
        }


        //////////////////////////////////////////
        // - Main Menu
        if (gameObject.name == "GameStart")
        {
            SMng.bNet = true;
            SetAct.Btn[4] = Instantiate(Panel, transform.parent);
            SetAct.Btn[4].name = "NetworkPanel";
            // Panel Position Setting
            SetAct.Btn[4].GetComponent<RectTransform>().offsetMin = new Vector2(40, 14);
            SetAct.Btn[4].GetComponent<RectTransform>().offsetMax = new Vector2(-40, -14);
        }
        // How To Play
        if (gameObject.name == "HTPlay")
        {
            SetAct.bHTPlay = true;
        }
        if (gameObject.name == "Mul_Stop")
        {
            if (SMng.bHost)
            {
                SMng.bHost = false;
                NMng.Net_CloseHost();
            }
            if (SMng.bJoin)
            {
                SMng.bJoin = false;
                NMng.Net_ExitRoom();
            }
            Panel.SetActive(false);
            SetAct.Btn[2].SetActive(false);
            SetAct.Btn[3].SetActive(false);
            SetAct.Btn[4].SetActive(true);
        }
        if (gameObject.name == "Mul_Start")
        {
            // Host
            if (SMng.bHost)
            {
                // All User is Readying
                if(true)
                {
                    Voice_Start.Play();
                    // Send Game Start Message
                    NMng.bGameStart = true;
                    // Setting Fade Time
                    NMng.fFadeTime = 3.0f;

                    NMng.Invoke("ChangeScene_Game", NMng.fFadeTime);
                    //NMng.OnClientSceneChanged()
                   // UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
                   
                }
            }
            // Client
            if (SMng.bJoin)
            {

            }
        }
        // - - Network Menu
        if (gameObject.name == "NP_Exit")
        {
            SMng.bNet = false;
        }
        if (gameObject.name == "NP_HostBtn" &&
            !SMng.bHost && !SMng.bJoin)
        {
            {
                Debug.Log("Create Host!");

                Voice_Create.Play();

                SMng.bHost = true;
                SetAct.Btn[2].SetActive(true);
                SetAct.Btn[2].transform.GetChild(0).GetComponent<Text>().text
                    = "Close Host";

                SetAct.Btn[3].SetActive(true);
                SetAct.Btn[3].transform.GetChild(0).GetComponent<Text>().text
                    = "Game Start\n(Needs All Users Ready)";

                SetAct.Btn[4].SetActive(false);
                
                NMng.Net_CreateHost();

                Panel = GameObject.Find("Canvas").transform.Find("PLPanel").gameObject;
                Panel.SetActive(true);
            }
        }
        if (gameObject.name == "NP_JoinBtn" &&
            !SMng.bHost && !SMng.bJoin)
        {
            string RmName = GameObject.Find("NP_Menu/InputField/Text").
                        GetComponent<Text>().text;

            if (RmName == "") RmName = "localhost";

            NMng.sIPAdr = RmName;

            Debug.Log("Join Room! IP Address : " + NMng.sIPAdr);

            Voice_Create.Play();

            SMng.bJoin = true;
            SetAct.Btn[2].SetActive(true);
            SetAct.Btn[2].transform.GetChild(0).GetComponent<Text>().text
                = "Stop Matching\n(Disconnect)";

            SetAct.Btn[3].SetActive(true);
            SetAct.Btn[3].transform.GetChild(0).GetComponent<Text>().text
                = "Ready";

            SetAct.Btn[4].SetActive(false);

            NMng.Net_JoinRoom();

            Panel = GameObject.Find("Canvas").transform.Find("PLPanel").gameObject;
            Panel.SetActive(true);

        }
        // - - - Join Menu


        // - - How To Play
        if (gameObject.name == "HP_Exit")
        {
            SetAct.bHTPlay = false;
        }

        /////////////////////////////////////////////////////////////////////////
        // - Result Scene
        if (gameObject.name == "GoToMenu")
        {
            // Send Game Start Message
            NMng.bBackMenu = true;
            // Setting Fade Time
            NMng.fFadeTime = 3.0f;
            if (!GameObject.Find("FadeIO(Clone)"))
            {
                GameObject FadeObj = Instantiate(NMng.FadePrf);
                FadeObj.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }

            NMng.Invoke("ChangeScene_Menu", NMng.fFadeTime);
        }
    }
}
