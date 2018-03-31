using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWall : MonoBehaviour {

    private Vector3 startMarker;
    private Vector3 endMarker;
    public float speed = 2.0F;
    private float startTime;
    private float journeyLength;
    void Start()
    {
        startMarker = transform.position - new Vector3(0, 2, 0);
        endMarker = transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        Destroy(gameObject, 5.0f);
    }
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
    }

}
