using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCameraScript : MonoBehaviour
{
    public Camera Cmr;

    private void Start()
    {
        //Cmr = Camera.main;
    }
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
            return;
        

        if (Cmr != null)
        {
            Cmr.transform.position = new Vector3(
                gameObject.transform.position.x,
                3.5f,
                gameObject.transform.position.z - 4
            );
        }

    }
}