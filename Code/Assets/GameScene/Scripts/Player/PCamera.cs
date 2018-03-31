using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCamera : MonoBehaviour
{
    public Transform Target;

    public float dist = 10f;
    public float height = 3f;
    public float dir = 1.5f;
    public float dampTrace = 20f;


    void Start()
    {
        GameObject player;

        player = GameObject.Find("Character[RED]");
        if (player.GetComponent<PMove>().isLocalPlayer)
        {
            Target = player.transform;
            return;
        }

        player = GameObject.Find("Character[BLUE]");
        if (player.GetComponent<PMove>().isLocalPlayer)
        {
            Target = player.transform;
            return;
        }

        player = GameObject.Find("Character[YELLOW]");
        if (player.GetComponent<PMove>().isLocalPlayer)
        {
            Target = player.transform;
            return;
        }

        player = GameObject.Find("Character[GREEN]");
        if (player.GetComponent<PMove>().isLocalPlayer)
        {
            Target = player.transform;
            return;
        }


    }
    
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
    Target.position - (Target.forward * dist) + (Vector3.up * height), Time.smoothDeltaTime * dampTrace);
        Vector3 v_dir = new Vector3(0, dir, 0);
        transform.LookAt(Target.position + v_dir);
        //Debug.Log(transform.position);
    }
    
    void FreezeCamera()
    {
        this.enabled = false;
    }
}
