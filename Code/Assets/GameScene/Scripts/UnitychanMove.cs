using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanMove : MonoBehaviour {

    public Animator anim;
    public Rigidbody rbody;

    private float inputH;
    private float inputV;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
