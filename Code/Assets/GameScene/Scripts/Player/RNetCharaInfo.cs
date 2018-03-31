using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RNetCharaInfo : NetworkBehaviour {

    //Character Color Value [0:Red 1:Blue 2:Yellow 3:Green]
    public int CharaCV;

    [SyncVar]
    private Vector3 NetUserPos;

    [ClientCallback]
    private void Update()
    {
        if(isLocalPlayer)
        {
            TransmitPos();
        }
        else
        {
            LerpPos();
        }
    }

    [Client]
    void TransmitPos()
    {
        CmdProvidePosToSvr(transform.position);
    }

    [Command]
    void CmdProvidePosToSvr(Vector3 pos)
    {
        NetUserPos = pos;
    }
    void LerpPos()
    {
        //Debug.Log(transform.name + " Lerp");
        transform.position = Vector3.Lerp(transform.position, NetUserPos, 0.25f);
    }
}
