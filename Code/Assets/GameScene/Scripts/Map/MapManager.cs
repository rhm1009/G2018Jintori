using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.Networking;


public class MapManager : NetworkBehaviour {

    public int MapX;
    public int MapZ;
    int index = 0;
    public int[,] Map;
    public string line;
    public string[] arr;
    StringReader reader;
    TextAsset asset = null;

    // Item zen Repeat timing
    public float Zen_Bomb = 5.0f;
    public float Zen_Wall = 4.0f;

    public GameObject _baseTile;
    public GameObject _wallTile;
    public GameObject _itemBomb;
    public GameObject _itemWall;

    private RNetworkMng NMng;
    private RMapServer MSrv;
    private RTimer Tmr;
    private PCamera Cmr;

    void Start()
    {
        NMng = GameObject.Find("SceneMng").GetComponent<RNetworkMng>();
        MSrv = GetComponent<RMapServer>();
        Tmr = GameObject.Find("Canvas/Timer").GetComponent<RTimer>();
        Cmr = Camera.main.GetComponent<PCamera>();

        NMng.bGameStart = true;

        // for camera position viv error
        Cmr.Invoke("FreezeCamera", 0.2f);
        // for set player position error
        Invoke("MeltMoving", 3.0f);

        if (MapLoad())
        {
            while (true)
            {
                line = reader.ReadLine();
                if (line == null) break;

                arr = line.Split('\t');
                if (index == 0)
                {
                    MapX = int.Parse(arr[0]);
                    MapZ = int.Parse(arr[1]);
                    Map = new int[MapZ, MapX];
                }
                else
                {
                    for (int x = 0; x < MapX; x++)
                    {
                        Map[index - 1, x] = int.Parse(arr[x]);
                    }
                }
                index++;
            }
        }

        MSrv.MData = Map;
        MSrv.MapX = MapX;
        MSrv.MapZ = MapZ;


        MapDraw();

        ClientScene.RegisterPrefab(_itemBomb);
        ClientScene.RegisterPrefab(_itemWall);

        // Item Init
        if (isServer)
        {
            InvokeRepeating("SetItemBomb", 5.0f, Zen_Bomb);
            InvokeRepeating("SetItemWall", 5.0f, Zen_Wall);
        }
    }

    void Update()
    {
        if (NMng.bGameStart)
        {
            SetCharPos();
        }
    }

    // You can Move!
    void MeltMoving()
    {
        Cmr.enabled = true;
        NMng.bGameStart = false;
        Tmr.bGame = true;
    }

    bool MapLoad()
    {
        string TextFilePath = "Data/MapData/MapData";
        asset = Resources.Load(TextFilePath) as TextAsset;
        reader = new StringReader(asset.text);
        return true;
    }

    void MapDraw()
    {
        for (int z = 0; z < MapZ; z++)
        {
            for (int x = 0; x < MapX; x++)
            {
                // this place is wall place
                if (Map[z, x] >= 9)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        GameObject objwall = Instantiate(_wallTile, new Vector3(-9.5f + x, i, -9.5f + z),
                        Quaternion.Euler(0, 0, 0),
                        GameObject.Find("GamePanel/Wall").transform);
                        objwall.name = "Wall[" + z + "][" + x + "]";
                    }
                }
                // no wall place
                else
                {
                    GameObject obj = Instantiate(_baseTile, new Vector3(-9.5f + x, -0.25f, -9.5f + z),
                        Quaternion.Euler(0, 0, 0),
                        GameObject.Find("GamePanel/Floor").transform);
                    obj.name = "Floor[" + z + "][" + x + "]";
                }
            }
        }
    }

    void SetCharPos()
    {
        for (int z = 0; z < MapZ; z++)
        {
            for (int x = 0; x < MapX; x++)
            {
                if (Map[z, x] == 1)
                {
                    Vector3 pos = new Vector3(-9.5f + x, 0.5f, -9.5f + z);
                    GameObject p = GameObject.Find("Character[RED]");
                    if (p != null)
                    {
                        p.transform.position = pos;
                        p.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                else if (Map[z, x] == 2)
                {
                    Vector3 pos = new Vector3(-9.5f + x, 0.5f, -9.5f + z);
                    GameObject p = GameObject.Find("Character[BLUE]");
                    if (p != null)
                    {
                        p.transform.position = pos;
                        p.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                }
                else if (Map[z, x] == 3)
                {
                    Vector3 pos = new Vector3(-9.5f + x, 0.5f, -9.5f + z);
                    GameObject p = GameObject.Find("Character[YELLOW]");
                    if (p != null)
                    {
                        p.transform.position = pos;
                        p.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                }
                else if (Map[z, x] == 4)
                {
                    Vector3 pos = new Vector3(-9.5f + x, 0.5f, -9.5f + z);
                    GameObject p = GameObject.Find("Character[GREEN]");
                    if (p != null)
                    {
                        p.transform.position = pos;
                        p.transform.rotation = Quaternion.Euler(0, -90, 0);
                    }
                }
            }
        }
    }
    void SetItemBomb()
    {
        int x, z;
        do
        {
            x = UnityEngine.Random.Range(0, MapX);
            z = UnityEngine.Random.Range(0, MapZ);

        } while (Map[z, x] > 9);

        GameObject Item = Instantiate(_itemBomb, new Vector3(-9.5f + x, 1.0f, -9.5f + z),
                        Quaternion.Euler(0, 0, 0));
        NetworkServer.Spawn(Item);
    }
    void SetItemWall()
    {
        int x, z;
        do
        {
            x = UnityEngine.Random.Range(0, MapX);
            z = UnityEngine.Random.Range(0, MapZ);

        } while (Map[z, x] > 9);

        GameObject Item = Instantiate(_itemWall, new Vector3(-9.5f + x, 1.0f, -9.5f + z),
                        Quaternion.Euler(0, 0, 0));
        NetworkServer.Spawn(Item);
    }
}