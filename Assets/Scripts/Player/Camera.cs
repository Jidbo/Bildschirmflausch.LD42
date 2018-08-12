using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    public float damping = 1;
    Vector3 offset;

    void Start()
    {
        offset = player.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = player.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = player.transform.position - (rotation * offset);

        transform.LookAt(player.transform);
    }
}
