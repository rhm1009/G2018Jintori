using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBombDamage : MonoBehaviour {

    private PMove MoveScr;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MoveScr = other.GetComponent<PMove>();
            if (!MoveScr.bDamage && !MoveScr.bSuper)
            {
                MoveScr.Anim.SetTrigger("Damaged");
                MoveScr.bDamage = true;
                MoveScr.Invoke("WakeUp", 4.0f);

                AudioSource Ado_Dmg;
                Ado_Dmg = other.transform.GetChild(2).GetChild(0).GetComponent<AudioSource>();
                Ado_Dmg.Play();
            }
        }
    }
}
