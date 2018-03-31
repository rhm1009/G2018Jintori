using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RNetworkMng : NetworkManager
{

    private RSceneMng SMng;

    public NetworkClient nClnt;


    public int iPort;
    public string sIPAdr;

    public Text Myip;

    // 0 Blue / 1 Yellow / 2 Green
    public bool[] bReady = new bool[3];
    public NetworkConnection[] NCon = new NetworkConnection[3];

    public bool bGameStart;
    public bool bGameOver;
    public bool bBackMenu;

    bool bHosted;

    public float fFadeTime;
    public GameObject DDCanvas; // Don't Destroy Canvas
    public GameObject FadeNetPrf;
    public GameObject FadePrf;
    GameObject FadeObj;

    // 0~2 : Join, 3~5 : Out
    private AudioSource[] Voice;

    // Network Manager Custom HUD

    private void Start()
    {
        Myip = GameObject.Find("MyIpText").GetComponent<Text>();
        DDCanvas = GameObject.Find("Canvas(DontDestroy)");

        SMng = GameObject.Find("SceneMng").GetComponent<RSceneMng>();
        iPort = 7777;
        //sIPAdr = "localhost";
        if (SMng.bHost)
        {
            //Create Host
            //Net_CreateHost();
        }
        else if (SMng.bJoin)
        {
            //Join room
            //Net_JoinRoom();
        }

        //UserInfo[0].Number = 0;

        bGameStart = false;
        bHosted = false;

        Voice = new AudioSource[6];
        Voice[0] = GameObject.Find("Sound/Join_GoodAfterNoon").GetComponent<AudioSource>();
        Voice[1] = GameObject.Find("Sound/Join_Sir").GetComponent<AudioSource>();
        Voice[2] = GameObject.Find("Sound/Join_ThanksForWait").GetComponent<AudioSource>();
        Voice[3] = GameObject.Find("Sound/Out_ByeBye").GetComponent<AudioSource>();
        Voice[4] = GameObject.Find("Sound/Out_SeeYou").GetComponent<AudioSource>();
        Voice[5] = GameObject.Find("Sound/Out_SeeYou2").GetComponent<AudioSource>();

        ClientScene.RegisterPrefab(FadeNetPrf);
    }

    private void Update()
    {
        //---------------------- Create Fade Image
        // Menu Scene -> Game Scene 
        if (SMng.bHost)//(isServer)
        {
            if (bGameStart && SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (!GameObject.Find("FadeIO_Net(Clone)"))
                {
                    //Network.Instantiate(FadeNetPrf, Vector3.zero, Quaternion.Euler(0, 0, 0), 0);
                    FadeObj = Instantiate(FadeNetPrf);
                    NetworkServer.Spawn(FadeObj);
                }
            }
            // Game Scene -> Result Scene
            if (bGameOver && SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (!GameObject.Find("FadeIO_Net(Clone)"))
                {
                    FadeObj = Instantiate(FadeNetPrf);
                    NetworkServer.Spawn(FadeObj);
                }
            }
        }
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = iPort;
    }

    void SetIPAddress()
    {
        NetworkManager.singleton.networkAddress = sIPAdr;
    }

    // ------------ Server -------------

    public void Net_CreateHost()
    {
        //Network.InitializeServer();
        NetworkServer.SetAllClientsNotReady();

        StopHost();
        SetPort();
        NetworkManager.singleton.StartHost();
        sIPAdr = Network.player.ipAddress;
        Debug.Log("[ Create IP Address : " + sIPAdr + " ]");
        Myip.text = "IP Address : " + sIPAdr;
        bReady = new bool[4];
        //User = 1;
    }

    public void Net_CloseHost()
    {
        NetworkManager.singleton.StopHost();
        Debug.Log("Host is Closing.");
        if (Myip) Myip.text = "";
        //User = 0;
    }
    public void ChangeScene_Menu()
    {
        if (bBackMenu)
        {
            if (bHosted)
            {
                ServerChangeScene("MenuScene");
            }
            else
            {
                SceneManager.LoadScene(0);
            }
            Destroy(GameObject.Find("Canvas(DontDestroy)"), 0.5f);
            //Destroy(gameObject, 0.2f);
            Invoke("Start", 0.8f);
        }
        bGameOver = false;
        bBackMenu = false;
    }
    public void ChangeScene_Game()
    {
        ServerChangeScene("GameScene");
        bGameStart = false;
    }
    public void ChangeScene_Result()
    {
        ServerChangeScene("ResultScene");
        // RGameScript Updated ... bGameOver = true;    
        //bGameOver = false;
    }


    // ------------ Client -------------

    public void Net_JoinRoom()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
        if (singleton.IsClientConnected())
        {
            Myip.text = sIPAdr + " Join Success";
            OnServerAddPlayer(singleton.client.connection, 0);
            ClientScene.Ready(singleton.client.connection);

            //nClnt = new NetworkClient();
            //nClnt.RegisterHandler(MsgType.Connect, OnPlayerConnected);
        }
        Debug.Log("Number of Connected Client : " + NetworkServer.connections.Count);
    }
    public void Net_ExitRoom()
    {
        NetworkManager.singleton.StopClient();
        Debug.Log("Exit Room");
        Myip.text = "";
    }

    //////////////////////////////////////////

    void OnServerConnect(NetworkConnection conn)
    {
        //if (conn.hostId >= 0)
        {
            Debug.Log("New");
        }
    }

    public void NetworkExit()
    {
        SMng.bNet = false;

               

        if (SMng.bHost)
        {
            SMng.bHost = false;
            bHosted = true;
            Net_CloseHost();
            //NetworkTransport.Shutdown();
        }
        else if(SMng.bJoin)
        {
            SMng.bJoin = false;
            Net_ExitRoom();
        }
    }


    /*
    private void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("New Player Connect!");
        PlayVoice(0);
    }
    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Player Disconnect...");
        PlayVoice(1);
    }
    */

    void PlayVoice(int index_)
    {
        // index_ = 0 ... -> Join
        // Join is 0~2
        if (index_ == 0)
        {
            int num = Random.Range(0, 2);
            Voice[num].Play();
        }

        // index_ = 1 ... -> Out
        // Out is 3~5
        else if (index_ == 1)
        {
            int num = Random.Range(3, 5);
            Voice[num].Play();
        }

        // If you want new voice, please input a code...
        else { }
    }
}
