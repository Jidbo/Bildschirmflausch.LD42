using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    public float damping = 1;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.position = position;

        transform.LookAt(player.transform.position);
    }
}
