using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RMapServer : NetworkBehaviour {
    
    public int MapX, MapZ;

    public int[,] MData;

    //bool - Is Server
    public bool bIsSvr;

    [SyncVar]
    public int SyncX;
    [SyncVar]
    public int SyncZ;
    [SyncVar]
    public int SyncColor;
    
    void Start()
    {
        bIsSvr = false;

        SyncX = 0; SyncZ = 0; SyncColor = 0;

        // Paint Map from Server Data
        if (!isServer)
        {
            return;
        }
        bIsSvr = true;
    }
    
    void Update()
    {
        MData[SyncZ, SyncX] = SyncColor;
    }


    public void InputMapData(int x_, int z_, int value_)
    {
        if (isServer)   RpcInputMData(x_, z_, value_);
        else            CmdMapChange(x_, z_, value_);
    }

    [Command]
    public void CmdMapChange(int x_, int z_, int c_)
    {
        SyncX = x_;
        SyncZ = z_;
        SyncColor = c_;
    }

    [ClientRpc]
    void RpcInputMData(int x_, int z_, int c_)
    {
        // Get Cube Object at Player's Foot
        GameObject cube = GameObject.Find("Floor[" + z_ + "][" + x_ + "]");
        // Change Cube's Color Value
        cube.GetComponent<RCubeValue>().cValue = c_;
    }
}
