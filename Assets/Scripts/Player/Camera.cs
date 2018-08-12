using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    public GameObject anchor1;
    public GameObject anchor2;
    public float damping = 1.0f;
    Vector3 offset;

    void Start()
    {
        offset = player.transform.position - transform.position;
    }

    void LateUpdate()
    {

        float px = player.transform.position.x - offset.x;
        float py = player.transform.position.z - offset.z;
        float x1 = anchor1.transform.position.x;
        float y1 = anchor1.transform.position.z;
        float x2 = anchor2.transform.position.x;
        float y2 = anchor2.transform.position.z;
        float dx = x2 - x1;
        float dy = y2 - y1;
        if ((dx == 0) && (dy == 0)){
            transform.position = new Vector3(x1, transform.position.y, y1);
        }
        else {
            float t = (dx * (px - x1) + dy * (py - y1)) / (dx * dx + dy * dy);
            t = Mathf.Clamp01(t);
            transform.position = new Vector3(x1 + t * dx, anchor1.transform.position.y, y1 + t * dy);
        }
        transform.LookAt(player.transform);
    }
}
