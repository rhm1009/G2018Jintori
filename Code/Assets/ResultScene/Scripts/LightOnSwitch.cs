using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnSwitch : MonoBehaviour {

    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;

    public AudioSource LOnSound;
    public AudioSource ClapSound;

    void Start ()
    {
        InvokeRepeating("Switch", 3.0f, 1.5f);
    }

	void Switch()
    {
        if (!Light1.active)
            Light1.active = true;
        else if (!Light2.active)
            Light2.active = true;
        else if (!Light3.active)
            Light3.active = true;
        else {
            CancelInvoke();
            ClapSound.Play();
            return;
        }
        LOnSound.Play();
    }
}
