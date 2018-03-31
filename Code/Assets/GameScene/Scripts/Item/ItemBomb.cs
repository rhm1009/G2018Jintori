using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : MonoBehaviour {

    private Collider ColorChange;
    private Collider Damage;
    private AudioSource Ado_Bomb;

    public float ExplosionTime;

    private void Awake()
    {
        ColorChange = gameObject.transform.GetChild(0).GetComponent<Collider>();
        ColorChange.enabled = false;
        Damage = gameObject.transform.GetChild(1).GetComponent<Collider>();
        Damage.enabled = false;
        Ado_Bomb = GameObject.Find("Sound/Bomb").GetComponent<AudioSource>();

        ExplosionTime = 1.3f;
    }
    void Start ()
    {
        Invoke("Effect", ExplosionTime);
    }

    void Effect()
    {
        ColorChange.enabled = true;
        Damage.enabled = true;
        Ado_Bomb.Play();
        Destroy(gameObject, 0.1f);
    }
}
