using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RAction : NetworkBehaviour
{

    private Animator Anim;
    private RCameraScript Cmr;
    public Rigidbody rb;

    public float speed;

    // bool value for Animation
    public bool Moving;
    public bool Attack;
    public bool Damaged;

    // prevent repeat Attack Animation
    int iAtkKey;

    // ArrowKey Pressing Time (Walk->Run)
    public float fPressTime;

    // Attack Collider Prefab
    public GameObject AtkColPf;

    void Start()
    {
        Anim = transform.GetChild(0).GetComponent<Animator>();
        Cmr = transform.GetComponent<RCameraScript>();
        rb = GetComponent<Rigidbody>();
        speed = 10;
        fPressTime = 0;
        //iPrsKey = 0;
        Moving = false; Attack = false; Damaged = false;
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        Cmr.Cmr = Camera.main;

        /*
        AnimFunc();

        // Moving
        if (!Attack)
        {
            // Move Key ... Arrow(Up, Down, Left, Right)
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Moving = true;
                //transform.rotation = Quaternion.Euler(0, 0, 0);

                // Get Multi Move Key... Down Speed
                bool DiagMove = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
                rb.AddForce(new Vector3(0, 0, (DiagMove) ? 0.7f : 1f) * speed);

                fPressTime += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Moving = true;
                //transform.rotation = Quaternion.Euler(0, 180, 0);

                bool DiagMove = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
                rb.AddForce(new Vector3(0, 0, (DiagMove) ? -0.7f : -1f) * speed);

                fPressTime += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //*
                Moving = true;
                ///transform.rotation = Quaternion.Euler(0, -90, 0);

                bool DiagMove = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow));
                rb.AddForce(new Vector3((DiagMove) ? -0.7f : -1f, 0, 0) * speed);
                //

                fPressTime += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //*
                Moving = true;
                //transform.rotation = Quaternion.Euler(0, 90, 0);

                bool DiagMove = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow));
                rb.AddForce(new Vector3((DiagMove) ? 0.7f : 1f, 0, 0) * speed);
                /

                fPressTime += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(new Vector3(0, -1, 0));
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(new Vector3(0, 1, 0));
            }
            // Attack Key ... A
            if (Input.GetKeyDown(KeyCode.A) && iAtkKey == 0)
            {

                /*
                Attack = true;
                
                GameObject AtkCol = Instantiate(AtkColPf, transform);

                // in Instantiate -> position // this -> local position
                AtkCol.transform.localPosition = Vector3.zero;
                AtkCol.transform.localRotation = Quaternion.Euler(Vector3.zero);
                //
            }
        }
        if (!Input.GetKey(KeyCode.UpArrow) &&
            !Input.GetKey(KeyCode.DownArrow) &&
            !Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.RightArrow))
        {
            Moving = false;
            fPressTime = 0;
            //iPrsKey = 0;
        }
        */

    }

    private void AnimFunc()
    {
        if (Moving)
        {
            Anim.SetBool("Walk", true);
            Anim.SetBool("Run", true);
        }
        else
        {
            Anim.SetBool("Walk", false);
            Anim.SetBool("Run", false);
        }
        if (Attack)
        {
            if (iAtkKey == 0)
            {
                Anim.SetTrigger("Damaged");
            }
            iAtkKey++;

            if (iAtkKey >= 30)
            {
                if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged@loop"))
                {
                    Attack = false;
                    iAtkKey = 0;
                }

                if (GameObject.Find("AttackCol(Clone)"))
                {
                    Destroy(GameObject.Find("AttackCol(Clone)"));
                }
            }
        }
        if (Damaged)
        {
            Anim.SetTrigger("Damaged");
        }
    }
}