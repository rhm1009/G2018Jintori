using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RNetworkItem : NetworkBehaviour
{
    // 0 : default / 1 : red .... 4 : green
    [SyncVar(hook = "BBChange")]
    public int BombIndex = 0;

    public bool[] bomb = new bool[4];

    public bool[] wall = new bool[4];

    public GameObject ItemBomb;
    public GameObject ItemWall;



    private void OnEnable()
    {
        ClientScene.RegisterPrefab(ItemBomb);
        //ClientScene.RegisterPrefab(ItemWall);
    }

    private void Update()
    {
        //Debug.Log("RED : " + SyncListBomb[0] + " / BLUE : " + SyncListBomb[1]);
        Debug.Log("Bombindex=" + BombIndex);
        if (isServer)
        {
            if(BombIndex != 0)
            {
                CmdInitBomb(BombIndex - 1);
            }
        }
    }

    void BBChange(int value)
    {
        // BombIndex -> Before / value -> After
        Debug.Log("Bombindex=" + BombIndex + ", value="+value);
        //BombIndex = value;
        //SyncListBomb[BombIndex] = true;
        //CmdInitBomb(BombIndex);
    }

    // Bomb Value Change Function
    [Command]
    public void CmdBValueChange(int index)
    {
        BombIndex = index;
    }

    [Command]
    public void CmdInitBomb(int cVal_) // Get Color Value
    {
        GameObject player = null;
        if (cVal_ == 0) player = GameObject.Find("Character[RED]");
        else if (cVal_ == 1) player = GameObject.Find("Character[BLUE]");
        else if (cVal_ == 2) player = GameObject.Find("Character[YELLOW]");
        else if (cVal_ == 3) player = GameObject.Find("Character[GREEN]");

        Vector3 BPos = player.transform.TransformDirection(Vector3.forward);
        BPos += player.transform.position;
        GameObject Bomb = Instantiate(ItemBomb, BPos, Quaternion.Euler(0, 0, 0));

        //SyncListBomb[cVal_] = false;
        BombIndex = 0;
        
        NetworkServer.Spawn(Bomb);

        Bomb.transform.GetChild(0).GetComponent<Bombarea>().CV = cVal_ + 1;
    }

    [Command]
    public void CmdInitWall(int cVal_) // Get Color Value
    {
        GameObject player = null;
        if (cVal_ == 0) player = GameObject.Find("Character[RED]");
        else if (cVal_ == 1) player = GameObject.Find("Character[BLUE]");
        else if (cVal_ == 2) player = GameObject.Find("Character[YELLOW]");
        else if (cVal_ == 3) player = GameObject.Find("Character[GREEN]");

        Vector3 WPos = player.transform.TransformDirection(Vector3.forward);
        WPos += player.transform.position;
        GameObject Wall = Instantiate(ItemWall, WPos, Quaternion.Euler(0, 0, 0));
    }
}
